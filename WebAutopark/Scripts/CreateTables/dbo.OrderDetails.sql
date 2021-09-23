CREATE TABLE [dbo].[OrderDetails] (
    [OrderDetailId] INT IDENTITY (1, 1) NOT NULL,
    [OrderId]       INT NOT NULL,
    [DetailId]      INT NOT NULL,
    [DetailAmount]  INT NOT NULL,
    CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED ([OrderDetailId] ASC),
    CONSTRAINT [FK_OrderDetails_Details] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Details] ([DetailId]),
    CONSTRAINT [FK_OrderDetails_Orders] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders] ([OrderId])
);