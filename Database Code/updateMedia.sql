IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'updateMedia') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE updateMedia AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE updateMedia ( 
	@mediaID int,
	@artistID int,
	@youtubeURL varchar(max),
	@name varchar(max),
	@description text) 
AS
BEGIN
	IF((SELECT COUNT(*) FROM ArtistExhibition WHERE ArtistID = @artist AND ExhibitionID = @exhibition) < 1)
		BEGIN
			INSERT INTO ArtistExhibition(ArtistID, ExhibitionID) VALUES (@artist,@exhibition);
		END
	UPDATE Media SET ArtistID = @artistID, YoutubeURL = @youtubeURL, Name = @name, ArtDescription = @description WHERE MediaID = @mediaID;
END
' 
END