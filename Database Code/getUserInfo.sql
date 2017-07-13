IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'getUserInfo') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE getUserInfo AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE getUserInfo ( 
	@userID int 
	)
AS
BEGIN
	SELECT Username, Email FROM SiteUser WHERE UserID = @userID;
END
' 
END