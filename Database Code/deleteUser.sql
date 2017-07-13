IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'deleteUser') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE deleteUser AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE deleteUser ( 
	@user int) 
AS
BEGIN
	DELETE FROM SiteUser WHERE UserID = @user;
END
' 
END