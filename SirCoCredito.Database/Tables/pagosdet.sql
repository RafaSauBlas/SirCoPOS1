CREATE TABLE [dbo].[pagosdet](
	[idpagosdet] [int] IDENTITY(1,1) NOT NULL,
	[idpagos] [int] NOT NULL,
	[sucursal] [varchar](2) NOT NULL,
	[sucnot] [varchar](2) NOT NULL,
	[nota] [varchar](6) NOT NULL,
	[pago] [int] NOT NULL,
	[subtotal] [decimal](18, 2) NULL,
	[descuento] [decimal](18, 2) NULL,
	[descuentoadicional] [decimal](18, 2) NULL,
	[interes] [decimal](18, 2) NULL,
	[interesmoratorio] [decimal](18, 2) NULL,
	[gastoscobranza] [decimal](18, 2) NULL,
	[importe] [decimal](18, 2) NULL,
	[vencido] [decimal](18, 2) NULL,
	[descuentovencido] [decimal](18, 2) NULL,
	[numpago] [int] NULL,
	[pagado] [int] NULL,
	[porcdescto] [decimal](18, 2) NULL,
	[porcdesctoadicional] [decimal](18, 2) NULL,
	[porcdesctovencido] [decimal](18, 2) NULL,
	[idusuario] [int] NULL,
	[fum] [datetime] NULL,
 CONSTRAINT [PK_pagosdet_1] PRIMARY KEY CLUSTERED 
(
	[idpagosdet] ASC,
	[idpagos] ASC,
	[sucursal] ASC,
	[sucnot] ASC,
	[nota] ASC,
	[pago] ASC
)
)
GO


