CREATE TABLE [dbo].[CajaFormaPago]
(
	[Sucursal] VARCHAR(2) NOT NULL, 
	[Numero] TINYINT NOT NULL, 
	[FormaPago] INT NOT NULL, 
	[Unidades] INT NOT NULL, 
    [Monto] MONEY NOT NULL, 
    CONSTRAINT [PK_CajaFormaPago] PRIMARY KEY ([Sucursal], [Numero], [FormaPago]), 
    CONSTRAINT [FK_CajaFormaPago_Caja] FOREIGN KEY ([Sucursal],[Numero]) REFERENCES [Caja]([Sucursal],[Numero]), 
)
