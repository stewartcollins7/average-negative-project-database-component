IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'getExhibitionIDs') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE getExhibitionIDs AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE getExhibitionIDs ( 
	@artistID int
	) 
AS
BEGIN
	SELECT ExhibitionID FROM Exhibition WHERE CuratedBy = @artistID ORDER BY DateEntered;
END
' 
END