IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'deleteExhibition') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE deleteExhibition AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE deleteExhibition ( 
	@exhibition int) 
AS
BEGIN
	DELETE FROM ArtistExhibition WHERE ExhibitionID = @exhibition;
	DELETE FROM Exhibition WHERE ExhibitionID = @exhibition;
END
' 
END