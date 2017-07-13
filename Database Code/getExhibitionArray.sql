IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'getExhibitionArray') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE getExhibitionArray AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE getExhibitionArray ( 
	@artistID int
	) 
AS
BEGIN
	SELECT ExhibitionID, ExhibitionName, ExhibitionDescription, DateEntered FROM Exhibition WHERE CuratedBy = @artistID ORDER BY DateEntered;
END
' 
END