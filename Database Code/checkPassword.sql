IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'checkPassword') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE checkPassword AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE checkPassword ( 
	@username varchar(50), 
	@password varchar(128)
	)
AS
BEGIN
	SELECT Count(*) FROM SiteUser WHERE Username = @username AND UserPassword = @password;
END
' 
END