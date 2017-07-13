IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'getMediaComments') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE getMediaComments AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE getMediaComments ( 
	@media int) 
AS
BEGIN
	SELECT Comment.CommentID, Comment.UserID, Comment.Content, Comment.PlacedOn, SiteUser.Username FROM Comment INNER JOIN SiteUser ON SiteUser.UserID = Comment.UserID WHERE MediaID = @media ORDER BY PlacedOn DESC;
END
' 
END