IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'getCoverImage') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE getCoverImage AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE getCoverImage ( 
	@exhibition int) 
AS
BEGIN
	SELECT TOP 1 MediaFilename FROM Media WHERE ExhibitionID=@exhibition;
END
' 
END