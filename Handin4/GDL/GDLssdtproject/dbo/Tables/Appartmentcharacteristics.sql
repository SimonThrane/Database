CREATE TABLE [dbo].[Appartmentcharacteristics] (
    [AppartmentId]                                      INT  IDENTITY (1, 1) NOT NULL,
    [No]                                                INT  NOT NULL,
    [Size]                                              REAL NOT NULL,
    [Floor]                                             INT  NOT NULL,
    [CharacteristicContainer_CharacteristicContainerId] INT  NULL,
    CONSTRAINT [PK_dbo.Appartmentcharacteristics] PRIMARY KEY CLUSTERED ([AppartmentId] ASC),
    CONSTRAINT [FK_dbo.Appartmentcharacteristics_dbo.CharacteristicContainers_CharacteristicContainer_CharacteristicContainerId] FOREIGN KEY ([CharacteristicContainer_CharacteristicContainerId]) REFERENCES [dbo].[CharacteristicContainers] ([CharacteristicContainerId])
);


GO
CREATE NONCLUSTERED INDEX [IX_CharacteristicContainer_CharacteristicContainerId]
    ON [dbo].[Appartmentcharacteristics]([CharacteristicContainer_CharacteristicContainerId] ASC);

