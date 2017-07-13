IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'getMediaIDs') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE getMediaIDs AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE getMediaIDs ( 
	@exhibition int) 
AS
BEGIN
	SELECT MediaID FROM Media WHERE ExhibitionID=@exhibition;
END
' 
END