CREATE TABLE [dbo].[pagodet](
	[sucursal] [varchar](2) NOT NULL,
	[pago] [varchar](6) NOT NULL,
	[idformapago] [int] NOT NULL,
	[idvaledigital] [int] NOT NULL,
	[importe] [numeric](18, 2) NULL,
	[comision] [numeric](18, 2) NULL,
	[observaciones] [varchar](150) NOT NULL,
	[iva] [numeric](6, 2) NULL,
	[idusuario] [int] NULL,
	[fum] [datetime] NULL,
	[referencia] [varchar](8) NULL, --nuevo
 [terminacion] VARCHAR(4) NULL, --nuevo
    [transaccion] VARCHAR(10) NULL, --nuevo
 [vale] CHAR(10) NULL, --nuevo
 [cvale] VARCHAR(13) NULL, --nuevo
 [movimiento] UNIQUEIDENTIFIER NULL, --nuevo
 [movimientocancela] UNIQUEIDENTIFIER NULL, --nuevo
 [formapago] VARCHAR(2) NULL, --nuevo
    CONSTRAINT [PK_pagodet_1] PRIMARY KEY CLUSTERED 
(
	[sucursal] ASC,
	[pago] ASC,
	[idformapago] ASC,
	[observaciones] ASC
)
)
GO