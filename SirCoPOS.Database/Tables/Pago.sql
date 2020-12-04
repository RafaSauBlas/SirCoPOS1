CREATE TABLE [dbo].[Pago]
(
	[Id] INT NOT NULL IDENTITY, 
    [ReceptorId] INT NOT NULL, 
    [EmisorId] INT NOT NULL, 
    [Importe] MONEY NOT NULL, 
    [Fecha] DATETIME NOT NULL, 
    [PeriodoId] INT NOT NULL, 
    CONSTRAINT [PK_Pago] PRIMARY KEY ([Id]), 
    CONSTRAINT [AK_Pago_ReceptorPeriodo] UNIQUE ([ReceptorId],[PeriodoId]) 
)
