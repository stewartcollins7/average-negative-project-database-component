IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'getArtistMediaIDs') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE getArtistMediaIDs AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE getArtistMediaIDs ( 
	@artist int) 
AS
BEGIN
	SELECT MediaID FROM Media WHERE ArtistID=@artist;
END
' 
END