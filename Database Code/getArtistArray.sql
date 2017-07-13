IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'getArtistArray') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE getArtistArray AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE getArtistArray ( 
	@user int) 
AS
BEGIN
	SELECT ArtistID, CreatedByUserID, Name, Location, ArtistBio FROM Artist WHERE CreatedByUserID = @user ORDER BY ArtistID ASC;
END
' 
END