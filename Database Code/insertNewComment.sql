IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'insertNewComment') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE insertNewComment AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE insertNewComment ( 
	@user int,
	@media int,
	@content text) 
AS
BEGIN
	INSERT INTO Comment (UserID, MediaID, Content) VALUES (@user,@media,@content);
	SELECT SCOPE_IDENTITY();
END
' 
END