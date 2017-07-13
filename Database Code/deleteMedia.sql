IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'deleteMedia') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE deleteMedia AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE deleteMedia ( 
	@media int) 
AS
BEGIN
	DELETE FROM Media WHERE MediaID = @media;
END
' 
END