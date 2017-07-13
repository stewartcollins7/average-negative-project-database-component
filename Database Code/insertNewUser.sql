IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'insertNewUser') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE insertNewUser AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE insertNewUser ( 
    @userName varchar(max) , 
    @email varchar(max),
    @isArtist bit,
	@userPassword varchar(max),
	@salt varchar(max)
	)
AS
BEGIN
    -- Checks whether username or email already registered
	IF ((SELECT COUNT(*) FROM SiteUser WHERE Username = @userName OR Email = @email) < 1)
		BEGIN
			INSERT INTO SiteUser (Username, Email, IsArtist, UserPassword, Salt) VALUES (@userName, @email, @isArtist, @userPassword, @salt);
			-- Returns 0 if inserted succesfully
			RETURN 0;
		END
	ELSE
		BEGIN
			IF ((SELECT COUNT(*) FROM SiteUser WHERE Username = @userName) > 0)
				BEGIN
				-- Returns 1 if Username already registered
				RETURN 1;
				END
			ELSE
				BEGIN
				-- Returns 2 if Email already registered
				RETURN 2;
				END
		END
END
' 
END