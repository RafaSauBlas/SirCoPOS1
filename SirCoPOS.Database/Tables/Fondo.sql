CREATE TABLE [dbo].[Fondo]
(
	[Id] INT NOT NULL IDENTITY, 
    [ResponsableId] INT NOT NULL, 
    [Disponible] MONEY NOT NULL, 
    [Tipo] TINYINT NOT NULL, 
    [AuditorAperturaId] INT NOT NULL,
    [FechaApertura] DATETIME NOT NULL,
    [AuditorCierreId] INT NULL,
    [FechaCierre] DATETIME NULL,
    [CajaSucursal] VARCHAR(2) NULL,
    [CajaNumero] TINYINT NULL,  
    [Activo] AS (case when [FechaCierre] IS NULL then (1) else (0) end) PERSISTED NOT NULL, 
    CONSTRAINT [PK_Fondo] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Fondo_Caja] FOREIGN KEY ([CajaSucursal], [CajaNumero]) REFERENCES [Caja]([Sucursal], [Numero])    
)

GO

CREATE UNIQUE INDEX [IX_Fondo_Caja] ON [dbo].[Fondo] ([CajaSucursal], [CajaNumero], [Activo]) WHERE [FechaCierre] IS NULL
