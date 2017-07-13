IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'usernameCount') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE usernameCount AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE usernameCount ( 
    @username varchar(20)  
    )
AS
BEGIN
    -- Checks whether username already registered
	SELECT COUNT(*) FROM SiteUser WHERE Username = @username
END
' 
END