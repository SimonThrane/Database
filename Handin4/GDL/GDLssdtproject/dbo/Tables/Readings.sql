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

CREATE TRIGGER [dbo].[Trigger4]

ON [dbo].[Readings]
FOR INSERT
AS
DECLARE @LogString VARCHAR(100)
DECLARE @LogString1 VARCHAR(100)
SELECT @LogString= (SELECT ReadingId +  ' '+ sensorId +' '+ appartmentId +' '+ value +' ' + timestamp From deleted)
SELECT @LogString1= (SELECT ReadingId +  ' '+ sensorId +' '+ appartmentId +' '+ value +' ' + timestamp From inserted)
Insert into dbo.LogTable(Operation, LogEntry, LogEntry2) VALUES ('Updated',@LogString,@LogString1);

END

CREATE NONCLUSTERED INDEX [IX_ReadingContainer_ReadingContainerId]
    ON [dbo].[Readings]([ReadingContainer_ReadingContainerId] ASC);
