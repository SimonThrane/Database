CREATE PROCEDURE [dbo].[InsertData]
	@Readings [ReadingsTable] readonly
AS
BEGIN
	INSERT INTO dbo.Readings ([sensorId],[appartmentId], [value],[timestamp])
	SELECT  [sensorId],[appartmentId], [value],[timestamp] FROM @Readings

	END
GO
