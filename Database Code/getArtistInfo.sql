IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'getArtistInfo') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE getArtistInfo AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE getArtistInfo ( 
	@artist int) 
AS
BEGIN
	SELECT CreatedByUserID, Name, Location, ArtistBio FROM Artist WHERE ArtistID = @artist;
END
' 
END