IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'insertNewExhibition') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE insertNewExhibition AS SELECT 1
'
END

GO

BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE insertNewExhibition ( 
	@name varchar(max),
	@curatedBy INT,
	@description text,
	@type char) 
AS
BEGIN
	INSERT INTO Exhibition (ExhibitionDescription, ExhibitionName, CuratedBy, ExhibitionType) VALUES (@description, @name, @curatedBy, @type);
	SELECT SCOPE_IDENTITY();
END
' 
END