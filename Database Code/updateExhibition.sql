IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'updateExhibition') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE updateExhibition AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE updateExhibition ( 
	@exhibitionID int,
	@name varchar(max),
	@type char,
	@description text) 
AS
BEGIN
	UPDATE Exhibition SET ExhibitionDescription = @description, ExhibitionName = @name, ExhibitionType = @type WHERE ExhibitionID = @exhibitionID;
	RETURN 0;
END
' 
END