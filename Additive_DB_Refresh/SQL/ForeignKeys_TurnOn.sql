DECLARE @stmt NVARCHAR(1000);


IF (OBJECT_ID('refresh.ForeignKeys_AdditiveProcess') IS NOT NULL)
BEGIN
    DECLARE stmt CURSOR FORWARD_ONLY FOR
    SELECT fk.TurnOn
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

    DROP TABLE refresh.ForeignKeys_AdditiveProcess;

END;