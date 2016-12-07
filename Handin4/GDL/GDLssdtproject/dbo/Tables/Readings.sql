CREATE TABLE [dbo].[Readings] (
    [ReadingId]                           INT      IDENTITY (1, 1) NOT NULL,
    [sensorId]                            INT      NOT NULL,
    [appartmentId]                        INT      NOT NULL,
    [value]                               REAL     NOT NULL,
    [timestamp]                           DATETIME NOT NULL,
    [ReadingContainer_ReadingContainerId] INT      NULL,
    CONSTRAINT [PK_dbo.Readings] PRIMARY KEY CLUSTERED ([ReadingId] ASC),
    CONSTRAINT [FK_dbo.Readings_dbo.ReadingContainers_ReadingContainer_ReadingContainerId] FOREIGN KEY ([ReadingContainer_ReadingContainerId]) REFERENCES [dbo].[ReadingContainers] ([ReadingContainerId])
);


GO



CREATE NONCLUSTERED INDEX [IX_ReadingContainer_ReadingContainerId]
    ON [dbo].[Readings]([ReadingContainer_ReadingContainerId] ASC);

GO

CREATE TRIGGER [dbo].[Trigger_Readings]
    ON [dbo].[Readings]
    FOR UPDATE
    AS
    BEGIN
        DECLARE @LogString VARCHAR(100)
		DECLARE @LogString1 VARCHAR(100)
		SELECT @LogString= (SELECT 'ReadingId: ' + cast(ReadingId as varchar) +  ' SensorId: '+ cast(sensorId as varchar) +' AppartmentId: '+ cast(appartmentId as varchar) +' Value: '+ cast(value as varchar) +' Timestamp: ' + cast(timestamp as varchar) From deleted)
		SELECT @LogString1= (SELECT 'ReadingId: ' + cast(ReadingId as varchar) +  ' SensorId: '+ cast(sensorId as varchar) +' AppartmentId: '+ cast(appartmentId as varchar) +' Value: '+ cast(value as varchar) +' Timestamp: ' + cast(timestamp as varchar) From inserted)
		Insert into dbo.LogTable(Operation, LogEntry, LogEntry2) VALUES ('Updated',@LogString,@LogString1);
    END
GO

CREATE TRIGGER [dbo].[Trigger_Readings_1]
    ON [dbo].[Readings]
    FOR DELETE
    AS
    BEGIN
        DECLARE @LogString VARCHAR(100)
		SELECT @LogString= (SELECT 'ReadingId: ' + cast(ReadingId as varchar) +  ' SensorId: '+ cast(sensorId as varchar) +' AppartmentId: '+ cast(appartmentId as varchar) +' Value: '+ cast(value as varchar) +' Timestamp: ' + cast(timestamp as varchar) From deleted)
		Insert into dbo.LogTable(Operation, LogEntry) VALUES ('Deleted',@LogString);
    END
GO

CREATE TRIGGER [dbo].[Trigger_Readings_2]
    ON [dbo].[Readings]
    FOR INSERT
    AS
    BEGIN
		DECLARE @LogString1 VARCHAR(100)
		SELECT @LogString1= (SELECT 'ReadingId: ' + cast(ReadingId as varchar) +  ' SensorId: '+ cast(sensorId as varchar) +' AppartmentId: '+ cast(appartmentId as varchar) +' Value: '+ cast(value as varchar) +' Timestamp: ' + cast(timestamp as varchar) From inserted)
		Insert into dbo.LogTable(Operation, LogEntry2) VALUES ('Inserted',@LogString1);
    END