CREATE VIEW [dbo].[metadata]
	AS SELECT [Sensorcharacteristics].calibrationCoeff, [Sensorcharacteristics].calibrationDate, [Sensorcharacteristics].[description], 
	[Sensorcharacteristics].calibrationEquation, Appartmentcharacteristics.AppartmentId, Appartmentcharacteristics.Floor, Appartmentcharacteristics.Size
	FROM [dbo].Sensorcharacteristics inner join Appartmentcharacteristics 
	on (Appartmentcharacteristics.CharacteristicContainer_CharacteristicContainerId = 
	Sensorcharacteristics.CharacteristicContainer_CharacteristicContainerId)