IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'getAllExhibitionIDs') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE getAllExhibitionIDs AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE getAllExhibitionIDs  
AS
BEGIN
	SELECT ExhibitionID FROM Exhibition ORDER BY DateEntered;
END
' 
END