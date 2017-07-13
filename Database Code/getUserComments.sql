IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'getUserComments') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE getUserComments AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE getUserComments ( 
	@user int) 
AS
BEGIN
	SELECT Comment.CommentID, Comment.MediaID, Comment.Content, Comment.PlacedOn, SiteUser.Username FROM Comment INNER JOIN SiteUser ON SiteUser.UserID = Comment.UserID WHERE Comment.UserID = @user ORDER BY PlacedOn DESC;
END
' 
END