IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'getSalt') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE getSalt AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE getSalt ( 
	@username varchar(20) 
	)
AS
BEGIN
	SELECT Salt FROM SiteUser WHERE Username = @username;
END
' 
END