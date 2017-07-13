IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'getUserID') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE getUserID AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE getUserID ( 
	@username varchar(max) 
	)
AS
BEGIN
	SELECT UserID FROM SiteUser WHERE Username = @username;
END
' 
END