IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'getExhibitionArtistIDs') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE getExhibitionArtistIDs AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE getExhibitionArtistIDs ( 
	@exhibition int) 
AS
BEGIN
	SELECT ArtistID FROM ArtistExhibition WHERE ExhibitionID = @exhibition;
END
' 
END