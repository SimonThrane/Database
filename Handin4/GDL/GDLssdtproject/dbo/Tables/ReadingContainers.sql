CREATE TABLE [dbo].[ReadingContainers] (
    [ReadingContainerId] INT      IDENTITY (1, 1) NOT NULL,
    [version]            INT      NOT NULL,
    [timestamp]          DATETIME NOT NULL,
    CONSTRAINT [PK_dbo.ReadingContainers] PRIMARY KEY CLUSTERED ([ReadingContainerId] ASC)
);

