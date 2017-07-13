IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'getExhibitionInfo') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE getExhibitionInfo AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE getExhibitionInfo ( 
	@exhibitionID int) 
AS
BEGIN
	SELECT CuratedBy, ExhibitionName, ExhibitionDescription, ExhibitionType, DateEntered FROM Exhibition WHERE ExhibitionID = @exhibitionID;
END
' 
END