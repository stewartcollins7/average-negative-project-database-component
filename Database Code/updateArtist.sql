IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'updateArtist') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE updateArtist AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE updateArtist ( 
	@artistID int,
    @name varchar(100),
    @location varchar(50),
	@bio text
	) 
AS
BEGIN
	UPDATE Artist 
	SET Name = @name, Location = @location, ArtistBio = @bio WHERE ArtistID = @artistID;
	RETURN 0;
END
' 
END