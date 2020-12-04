CREATE TABLE [dbo].[FondoArqueoFormaPagoTicket]
(
	[Id] INT NOT NULL IDENTITY,
	[FondoArqueoId] INT NOT NULL, 
    [FormaPago] INT NOT NULL, 
    [Monto] MONEY NOT NULL, 
    CONSTRAINT [PK_FondoArqueoFormaPagoTicket] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_FondoArqueoFormaPagoTicket_FondoArqueoFormaPago] FOREIGN KEY ([FondoArqueoId], [FormaPago]) REFERENCES [FondoArqueoFormaPago]([FondoArqueoId], [FormaPago]) 
)
