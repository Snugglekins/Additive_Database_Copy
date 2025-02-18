IF (OBJECT_ID('refresh.Triggers_AdditiveProcess') IS NULL)
BEGIN
    SELECT 'DISABLE TRIGGER [' + SCHEMA_NAME(o.schema_id) + '].[' + t.name + '] ON [' + SCHEMA_NAME(o.schema_id)
           + '].[' + o.name + ']' AS DisableTrigger
         , 'ENABLE TRIGGER [' + SCHEMA_NAME(o.schema_id) + '].[' + t.name + '] ON [' + SCHEMA_NAME(o.schema_id) + '].['
           + o.name + ']'         AS EnableTrigger
    INTO refresh.Triggers_AdditiveProcess
    FROM sys.triggers          AS t
        INNER JOIN sys.objects AS o
            ON o.object_id = t.parent_id
    WHERE t.is_disabled = 0;
END;

DECLARE @stmt NVARCHAR(1000);

DECLARE stmt CURSOR FORWARD_ONLY FOR
SELECT fk.DisableTrigger
FROM refresh.Triggers_AdditiveProcess AS fk;

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