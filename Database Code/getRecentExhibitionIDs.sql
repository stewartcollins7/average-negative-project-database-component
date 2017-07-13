IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'getRecentExhibitionIDs') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE getRecentExhibitionIDs AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE getRecentExhibitionIDs  
AS
BEGIN
	SELECT TOP 9 ExhibitionID FROM Exhibition ORDER BY ExhibitionID DESC;
END
' 
END