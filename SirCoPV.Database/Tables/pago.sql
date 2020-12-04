CREATE TABLE [dbo].[pago](
	[sucursal] [varchar](2) NOT NULL,
	[pago] [varchar](6) NOT NULL,
	[fecha] [date] NULL,
	[estatus] [varchar](2) NULL,
	[idcajero] [int] NULL,
	[idvendedor] [int] NULL,
	[idusuario] [int] NULL,
	[fum] [datetime] NULL,
	[idusuariocancela] [int] NULL,
	[fumcancela] [datetime] NULL,
 CONSTRAINT [PK_pago] PRIMARY KEY CLUSTERED 
(
	[sucursal] ASC,
	[pago] ASC
)
)
GO
