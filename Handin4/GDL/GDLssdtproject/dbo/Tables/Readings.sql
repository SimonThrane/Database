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

