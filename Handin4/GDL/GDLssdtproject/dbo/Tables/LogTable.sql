CREATE TABLE [dbo].[LogTable]
(
	[Operation] NVARCHAR (50)  NULL,
    [LogEntry]  NVARCHAR (100) NULL,
    [LogEntry2] NVARCHAR (100) NULL,
    [ID]        BIGINT         IDENTITY (1, 1) NOT NULL

)
