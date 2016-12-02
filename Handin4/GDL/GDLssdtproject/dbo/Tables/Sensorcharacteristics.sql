CREATE TABLE [dbo].[Sensorcharacteristics] (
    [sensorId]                                          INT            IDENTITY (1, 1) NOT NULL,
    [calibrationCoeff]                                  NVARCHAR (MAX) NULL,
    [description]                                       NVARCHAR (MAX) NULL,
    [calibrationDate]                                   DATETIME       NOT NULL,
    [externalRef]                                       NVARCHAR (MAX) NULL,
    [unit]                                              NVARCHAR (MAX) NULL,
    [calibrationEquation]                               NVARCHAR (MAX) NULL,
    [CharacteristicContainer_CharacteristicContainerId] INT            NULL,
    CONSTRAINT [PK_dbo.Sensorcharacteristics] PRIMARY KEY CLUSTERED ([sensorId] ASC),
    CONSTRAINT [FK_dbo.Sensorcharacteristics_dbo.CharacteristicContainers_CharacteristicContainer_CharacteristicContainerId] FOREIGN KEY ([CharacteristicContainer_CharacteristicContainerId]) REFERENCES [dbo].[CharacteristicContainers] ([CharacteristicContainerId])
);


GO
CREATE NONCLUSTERED INDEX [IX_CharacteristicContainer_CharacteristicContainerId]
    ON [dbo].[Sensorcharacteristics]([CharacteristicContainer_CharacteristicContainerId] ASC);

