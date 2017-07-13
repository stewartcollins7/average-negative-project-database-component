IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'deleteArtist') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE deleteArtist AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE deleteArtist ( 
	@artist int) 
AS
BEGIN
	DELETE FROM ArtistExhibition WHERE ArtistID = @artist;
	DELETE FROM Artist WHERE ArtistID = @artist;
END
' 
END