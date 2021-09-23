CREATE TABLE [dbo].[VehicleTypes] (
    [VehicleTypeId]  INT          NOT NULL IDENTITY,
    [TypeName]       NVARCHAR (30) NOT NULL,
    [TaxCoefficient] FLOAT (53)   NOT NULL,
    CONSTRAINT [PK_VehicleTypes] PRIMARY KEY CLUSTERED ([VehicleTypeId] ASC)
);

