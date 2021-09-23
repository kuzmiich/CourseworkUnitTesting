CREATE TABLE [dbo].[Details] (
    [DetailId] INT           NOT NULL IDENTITY,
    [Name]     NVARCHAR (30) NOT NULL,
    CONSTRAINT [PK_Details] PRIMARY KEY CLUSTERED ([DetailId] ASC)
);

