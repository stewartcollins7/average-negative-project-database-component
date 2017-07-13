IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'getMediaInfo') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE getMediaInfo AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE getMediaInfo ( 
	@media int) 
AS
BEGIN
	SELECT ExhibitionID, ArtistID, YoutubeURL, MediaFilename, Name, ArtDescription FROM Media WHERE MediaID=@media;
END
' 
END