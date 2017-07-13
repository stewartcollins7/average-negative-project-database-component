IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'getArtistIDs') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE getArtistIDs AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE getArtistIDs ( 
	@user int) 
AS
BEGIN
	SELECT ArtistID FROM Artist WHERE CreatedByUserID = @user ORDER BY ArtistID ASC;
END
' 
END