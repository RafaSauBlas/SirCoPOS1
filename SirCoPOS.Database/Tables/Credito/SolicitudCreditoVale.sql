CREATE TABLE [Credito].[SolicitudCreditoVale]
(
	[Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(), 
    [vale] VARCHAR(14) NOT NULL, 
    [electronica] BIT NOT NULL, 
    [monto] MONEY NOT NULL, 
    [date] DATETIME NOT NULL, 
    [idusuario] INT NOT NULL, 
    [fechaRevision] DATETIME NULL,
    [idusuarioAprobacion] INT NULL, 
    [fechaAprobacion] DATETIME NULL, 
    [Approved] BIT NULL,     
    [montoAprobacion] MONEY NULL, 
    [electronicaAprobacion] BIT NULL,     
    [creditoAprobacion] MONEY NULL, 
    CONSTRAINT [PK_SolicitudCreditoVale] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_SolicitudCreditoVale_Vale] FOREIGN KEY ([vale]) REFERENCES [Credito].[ValeCliente]([vale]) 
)
