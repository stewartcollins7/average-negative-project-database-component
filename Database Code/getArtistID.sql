IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'getArtistID') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE getArtistID AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE getArtistID ( 
	@userID int) 
AS
BEGIN
	SELECT TOP 1 ArtistID FROM Artist WHERE CreatedByUserID = @userID ORDER BY ArtistID;
END
' 
END