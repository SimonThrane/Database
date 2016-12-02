CREATE FUNCTION [dbo].[StructuredData]
(
	@param1 int
)
RETURNS TABLE AS RETURN
(
	SELECT Readings.value, Sensorcharacteristics.unit, Readings.timestamp, Readings.appartmentId FROM Readings INNER JOIN Sensorcharacteristics
	ON Sensorcharacteristics.sensorId = Readings.sensorId WHERE appartmentId = @param1
)
