CREATE TABLE [dbo].[CharacteristicContainers] (
    [CharacteristicContainerId] INT      IDENTITY (1, 1) NOT NULL,
    [timestamp]                 DATETIME NOT NULL,
    [version]                   INT      NOT NULL,
    CONSTRAINT [PK_dbo.CharacteristicContainers] PRIMARY KEY CLUSTERED ([CharacteristicContainerId] ASC)
);

