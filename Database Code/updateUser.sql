IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'updateUser') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE updateUser AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE updateUser ( 
    @userID int , 
    @email varchar(max),
	@username varchar(max)
	) 
AS
BEGIN
	UPDATE SiteUser SET Email=@email, Username=@username WHERE UserID = @userID;
END
' 
END