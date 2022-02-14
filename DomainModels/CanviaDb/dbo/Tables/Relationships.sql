CREATE TABLE [dbo].[Relationships] (
    [Id]             INT IDENTITY (1, 1) NOT NULL,
    [SourcePersonId] INT NULL,
    [TargetPersonId] INT NULL,
    [Relation]       INT NULL,
    CONSTRAINT [PK_Relationships] PRIMARY KEY CLUSTERED ([Id] ASC)
);

