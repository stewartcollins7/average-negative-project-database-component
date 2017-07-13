IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'editComment') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE editComment AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE editComment ( 
	@commentID int,
	@content text) 
AS
BEGIN
	UPDATE Comment SET Content = @content, PlacedOn = GETDATE() WHERE CommentID = @commentID;
END
' 
END