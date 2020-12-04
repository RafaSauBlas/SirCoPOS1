CREATE TABLE [dbo].[devolucion](
	[sucursal] [varchar](2) NOT NULL,
	[devolvta] [varchar](6) NOT NULL,
	[tipo] [varchar](10) NOT NULL,
	[fecha] [date] NULL,
	[estatus] [varchar](2) NULL,
	[referencia] [varchar](8) NOT NULL,
	[comentarios] [varchar](150) NOT NULL,
	[idcajero] [int] NULL,
	[idvendedor] [int] NULL,
	[idusuario] [int] NULL,
	[fum] [datetime] NULL,
	[idusuariocancela] [int] NULL,
	[fumcancela] [datetime] NULL,
	[disponible] [numeric](18, 2) NULL, 
	[idcliente] INT NULL, --nuevo
 CONSTRAINT [PK_devoludion] PRIMARY KEY CLUSTERED 
(
	[sucursal] ASC,
	[devolvta] ASC
)
)
GO