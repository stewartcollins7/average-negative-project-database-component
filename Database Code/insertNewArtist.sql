IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'insertNewArtist') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE insertNewArtist AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE insertNewArtist ( 
    @userID INT,
	@name varchar(max) , 
    @location varchar(max),
	@bio text
	) 
AS
BEGIN
	INSERT INTO Artist (CreatedByUserID, Name, Location, ArtistBio) VALUES (@userID, @name, @location, @bio);
	SELECT SCOPE_IDENTITY();
END
' 
END