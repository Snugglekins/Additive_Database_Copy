IF (OBJECT_ID('refresh.ForeignKeys_AdditiveProcess') IS NULL)
BEGIN
    
    SELECT CAST('ALTER TABLE [' + SCHEMA_NAME(fk.schema_id) + '].[' + OBJECT_NAME(fk.parent_object_id) + ']'
                + ' WITH CHECK CHECK CONSTRAINT [' + fk.name AS NVARCHAR(1000)) + ']'   AS TurnOn
         , CAST('ALTER TABLE [' + SCHEMA_NAME(fk.schema_id) + '].[' + OBJECT_NAME(fk.parent_object_id) + ']'
                + ' NOCHECK CONSTRAINT [' + fk.name AS NVARCHAR(1000)) + ']' AS TurnOff
    INTO refresh.ForeignKeys_AdditiveProcess
    FROM sys.foreign_keys AS fk
    WHERE fk.is_disabled = 0;
END;

DECLARE @stmt NVARCHAR(1000);

DECLARE stmt CURSOR FORWARD_ONLY FOR
SELECT fk.TurnOff
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