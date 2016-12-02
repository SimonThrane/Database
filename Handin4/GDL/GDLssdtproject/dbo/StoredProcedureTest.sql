EXEC dbo.ReadSensors 99;
GO

SELECT * From dbo.StructuredData(99) where unit = 'kWh';