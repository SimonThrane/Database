CREATE TYPE [dbo].[ReadingsTable] AS TABLE
(
	[ReadingId] INT, [sensorId] INT,[appartmentId] INT, [value] FLOAT,[timestamp] DateTime
)
