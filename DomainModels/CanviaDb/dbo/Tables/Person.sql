CREATE TABLE [dbo].[Person] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50) NULL,
    [Lastname]    NVARCHAR (50) NULL,
    [Birthdate]   DATETIME      NULL,
    [Deathdate]   DATETIME      NULL,
    [Sex]         BIT           NULL,
    [IsAvailable] BIT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);



