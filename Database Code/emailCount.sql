IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'emailCount') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE emailCount AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE emailCount ( 
    @email varchar(100)  
    )
AS
BEGIN
    -- Checks whether username already registered
	SELECT COUNT(*) FROM SiteUser WHERE Email = @email
END
' 
END