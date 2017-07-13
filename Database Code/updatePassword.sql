IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'updatePassword') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE updatePassword AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE updatePassword ( 
    @userID int , 
    @password varchar(max)) 
AS
BEGIN
	UPDATE SiteUser SET UserPassword=@password WHERE UserID = @userID;
END
' 
END