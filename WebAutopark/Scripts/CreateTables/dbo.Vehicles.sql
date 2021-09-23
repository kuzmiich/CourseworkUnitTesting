CREATE TABLE [dbo].[Vehicles] (
    [VehicleId]          INT           NOT NULL IDENTITY,
    [VehicleTypeId]      INT           NOT NULL,
    [ModelName]          NVARCHAR (30) NOT NULL,
    [RegistrationNumber] NVARCHAR (20) NULL,
    [ManufractureYear]   INT           NOT NULL,
    [Weight]             INT           NOT NULL,
    [Mileage]            INT           NOT NULL,
    [Color]              INT           NOT NULL,
    [TankCapacity]       FLOAT (53)    NOT NULL,
    [EngineConsumption]  FLOAT (53)    NOT NULL,
    [TankCapacity]       FLOAT (53)    NOT NULL,
    CONSTRAINT [PK_Vehicles] PRIMARY KEY CLUSTERED ([VehicleId] ASC),
    CONSTRAINT [FK_Vehicles_VehicleTypes] FOREIGN KEY ([VehicleTypeId]) REFERENCES [dbo].[VehicleTypes] ([VehicleTypeId])
);

