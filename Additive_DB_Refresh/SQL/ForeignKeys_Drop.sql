IF (OBJECT_ID('refresh.ForeignKeys_AdditiveProcess') IS NULL)
BEGIN
    CREATE TABLE refresh.ForeignKeys_AdditiveProcess -- feel free to use a permanent table
    (
      object_id BIGINT NOT null,
      fk_name NVARCHAR(1000),
      drop_script NVARCHAR(MAX),
      create_script NVARCHAR(MAX)
    );
  
    INSERT INTO refresh.ForeignKeys_AdditiveProcess
    (
        object_id
      , fk_name
      , drop_script
    )

    -- drop is easy, just build a simple concatenated list from sys.foreign_keys:
    SELECT fk.object_id,
                QUOTENAME(cs.name)+'.'+QUOTENAME(fk.name),
    'IF OBJECT_ID('''+QUOTENAME(cs.name)+'.'+QUOTENAME(fk.name)+''') IS NOT NULL '+
    'ALTER TABLE ' + QUOTENAME(cs.name) + '.' + QUOTENAME(ct.name) 
        + ' DROP CONSTRAINT ' + QUOTENAME(fk.name) + ';' AS drop_script
    FROM sys.foreign_keys AS fk
    INNER JOIN sys.tables AS ct
      ON fk.parent_object_id = ct.[object_id]
    INNER JOIN sys.schemas AS cs 
      ON ct.[schema_id] = cs.[schema_id];

    -- create is a little more complex. We need to generate the list of 
    -- columns on both sides of the constraint, even though in most cases
    -- there is only one column.
    ;WITH createscripts AS (
	   SELECT
       fk.object_id AS object_id
       , 'IF OBJECT_ID('''+QUOTENAME(cs.name)+'.'+QUOTENAME(fk.name)+''') IS NULL '+
       'ALTER TABLE ' 
       + QUOTENAME(cs.name) + '.' + QUOTENAME(ct.name) 
	   + CASE fk.is_not_trusted WHEN 1 THEN ' WITH NOCHECK ' ELSE '' END 
       + ' ADD CONSTRAINT ' + QUOTENAME(fk.name) 
       + ' FOREIGN KEY (' + STUFF((SELECT ',' + QUOTENAME(c.name) 
       -- get all the columns in the constraint table
        FROM sys.columns AS c 
        INNER JOIN sys.foreign_key_columns AS fkc 
        ON fkc.parent_column_id = c.column_id
        AND fkc.parent_object_id = c.[object_id]
        WHERE fkc.constraint_object_id = fk.[object_id]
        ORDER BY fkc.constraint_column_id 
        FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 1, N'')
      + ') REFERENCES ' + QUOTENAME(rs.name) + '.' + QUOTENAME(rt.name)
      + '(' + STUFF((SELECT ',' + QUOTENAME(c.name)
       -- get all the referenced columns
        FROM sys.columns AS c 
        INNER JOIN sys.foreign_key_columns AS fkc 
        ON fkc.referenced_column_id = c.column_id
        AND fkc.referenced_object_id = c.[object_id]
        WHERE fkc.constraint_object_id = fk.[object_id]
        ORDER BY fkc.constraint_column_id 
        FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 1, N'') + ')' 
		+ CASE WHEN fk.update_referential_action_desc NOT LIKE 'NO_ACTION' THEN ' ON UPDATE ' + fk.update_referential_action_desc COLLATE SQL_Latin1_General_CP1_CI_AS ELSE '' END 
		+ CASE WHEN fk.delete_referential_action_desc NOT LIKE 'NO_ACTION' THEN ' ON DELETE ' + fk.delete_referential_action_desc COLLATE SQL_Latin1_General_CP1_CI_AS ELSE '' END 
		AS create_script
    FROM sys.foreign_keys AS fk
    INNER JOIN sys.tables AS rt -- referenced table
      ON fk.referenced_object_id = rt.[object_id]
    INNER JOIN sys.schemas AS rs 
      ON rt.[schema_id] = rs.[schema_id]
    INNER JOIN sys.tables AS ct -- constraint table
      ON fk.parent_object_id = ct.[object_id]
    INNER JOIN sys.schemas AS cs 
      ON ct.[schema_id] = cs.[schema_id]
    WHERE rt.is_ms_shipped = 0 AND ct.is_ms_shipped = 0
   )


    UPDATE fk
    SET create_script = cs.create_script
    FROM refresh.ForeignKeys_AdditiveProcess AS fk
    INNER JOIN createscripts cs
        ON cs.object_id = fk.object_id

END



DECLARE @stmt NVARCHAR(1000);

DECLARE stmt CURSOR FORWARD_ONLY FOR
SELECT fk.drop_script
FROM refresh.ForeignKeys_AdditiveProcess AS fk;

OPEN stmt;

FETCH NEXT FROM stmt
INTO @stmt;

WHILE @@FETCH_STATUS = 0
BEGIN
    EXEC sys.sp_executesql @stmt;
    FETCH NEXT FROM stmt
    INTO @stmt;
END;
CLOSE stmt;
DEALLOCATE stmt;