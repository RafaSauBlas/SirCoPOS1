CREATE TABLE [dbo].[pagos](
	[idpagos] [int] IDENTITY(1,1) NOT NULL,
	[sucursal] [varchar](2) NULL,
	[folio] [varchar](6) NULL,
	[distrib] [varchar](6) NULL,
	[status] [varchar](2) NULL,
	[fecha] [date] NULL,
	[subtotal] [decimal](18, 2) NULL,
	[descuento] [decimal](18, 2) NULL,
	[descuentoadicional] [decimal](18, 2) NULL,
	[interes] [decimal](18, 2) NULL,
	[interesmoratorio] [decimal](18, 2) NULL,
	[gastoscobranza] [decimal](18, 2) NULL,
	[importe] [decimal](18, 2) NULL,
	[vencido] [decimal](18, 2) NULL,
	[descuentovencido] [decimal](18, 2) NULL,
	[cobrador] [int] NULL,
	[idconvenio] [int] NULL,
	[idusuario] [int] NULL,
	[fum] [datetime] NULL,
	[idusuariocancela] [int] NULL,
	[fumcancela] [datetime] NULL,
	[idusuarioautoriza] [int] NULL,
	[fumautoriza] [datetime] NULL,
	[idcorte] INT NULL, --nuevo
	[idcortecancelacion] INT NULL, --nuevo    
    CONSTRAINT [PK_pagos] PRIMARY KEY CLUSTERED 
(
	[idpagos] ASC
)
)
GO


