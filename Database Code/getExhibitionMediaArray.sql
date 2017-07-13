IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'getExhibitionMediaArray') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE getExhibitionMediaArray AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE getExhibitionMediaArray ( 
	@exhibitionID int) 
AS
BEGIN
	SELECT Artist.Name AS ArtistName, Media.MediaFilename, Media.Name AS MediaName, Media.ArtDescription, Media.MediaType, Media.ArtistID, Media.MediaID FROM Media INNER JOIN Artist ON Media.ArtistID = Artist.ArtistID WHERE ExhibitionID = @exhibitionID;
END
' 
END