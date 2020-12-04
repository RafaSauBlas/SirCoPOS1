CREATE TABLE [dbo].[venta]
(
	[sucursal] [varchar](2) NOT NULL,
	[venta] [varchar](6) NOT NULL,
	[fecha] [date] NULL,
	[estatus] [varchar](2) NULL,
	[idcajero] [int] NULL,
	[idvendedor] [int] NULL,
	[idusuario] [int] NULL,
	[fum] [datetime] NULL,
	[idusuariocancela] [int] NULL,
	[fumcancela] [datetime] NULL,
 [idcliente] INT NULL, [idcorte] INT NULL, 
    [idcortecancelacion] INT NULL,     --nuevo
	[multiple] BIT NULL,     --nuevo
 CONSTRAINT [PK_venta] PRIMARY KEY CLUSTERED 
(
	[sucursal] ASC,
	[venta] ASC
)
)
