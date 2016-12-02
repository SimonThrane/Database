CREATE PROCEDURE [dbo].[ReadSensors]
	@param1 int
AS
	SELECT Readings.value, Sensorcharacteristics.unit, Readings.timestamp, Readings.appartmentId FROM Readings INNER JOIN Sensorcharacteristics
	ON Sensorcharacteristics.sensorId = Readings.sensorId WHERE appartmentId = @param1
RETURN