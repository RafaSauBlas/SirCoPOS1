CREATE TABLE [dbo].[FondoArqueoFormaPago]
(
	[FondoArqueoId] INT NOT NULL,
    [FormaPago] INT NOT NULL, 
    [Unidades] INT NOT NULL,
    [ReportadoUnidades] INT NOT NULL, 
    [Monto] MONEY NOT NULL,
    [ReportadoMonto] MONEY NOT NULL, 
    [FaltanteUnidades] AS ([Unidades] - [ReportadoUnidades]),
    [FaltanteMonto] AS ([Monto] - [ReportadoMonto])
    CONSTRAINT [PK_FondoArqueoFormaPago] PRIMARY KEY ([FondoArqueoId], [FormaPago]), 
    CONSTRAINT [FK_FondoArqueoFormaPago_FondoArqueo] FOREIGN KEY ([FondoArqueoId]) REFERENCES [FondoArqueo]([Id])
)
