IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'deleteComment') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE deleteComment AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE deleteComment ( 
	@comment int) 
AS
BEGIN
	DELETE FROM Comment WHERE CommentID = @comment;
END
' 
END