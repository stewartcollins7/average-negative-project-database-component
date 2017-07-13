IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'getIsArtist') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE getIsArtist AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE getIsArtist ( 
	@userID int) 
AS
BEGIN
	SELECT COUNT(*) FROM SiteUser WHERE UserID = @userID AND isArtist=1;
END
' 
END