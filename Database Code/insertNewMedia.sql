IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'insertNewMedia') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE insertNewMedia AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE insertNewMedia ( 
	@exhibition int,
	@artist int,
	@youtubeURL varchar(max),
	@filename varchar(max),
	@name varchar(max),
	@description text) 
AS
BEGIN
	IF((SELECT COUNT(*) FROM ArtistExhibition WHERE ArtistID = @artist AND ExhibitionID = @exhibition) < 1)
		BEGIN
			INSERT INTO ArtistExhibition(ArtistID, ExhibitionID) VALUES (@artist,@exhibition);
		END
	INSERT INTO Media (ExhibitionID, ArtistID, YoutubeURL, MediaFilename, Name, ArtDescription) VALUES (@exhibition,@artist,@youtubeURL,@filename,@name,@description);
	SELECT SCOPE_IDENTITY();
END
' 
END