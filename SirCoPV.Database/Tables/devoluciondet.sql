CREATE TABLE [dbo].[devoluciondet](
	[sucursal] [varchar](2) NOT NULL,
	[devolvta] [varchar](6) NOT NULL,
	[renglon] [smallint] NOT NULL,
	[marca] [varchar](3) NULL,
	[estilon] [varchar](7) NULL,
	[corrida] [varchar](1) NULL,
	[medida] [varchar](3) NULL,
	[serie] [varchar](13) NOT NULL,
	[idpromocion] [int] NULL,
	[ctd] [smallint] NULL,
	[precio] [numeric](18, 2) NULL,
	[precdesc] [numeric](18, 2) NULL,
	[costomargen] [numeric](18, 2) NULL,
	[iva] [numeric](6, 2) NULL,
	[idusuario] [int] NULL,
	[fum] [datetime] NULL,
	[notas] VARCHAR(200) NULL, --nuevo
	[idrazon] INT NULL, --nuevo
 CONSTRAINT [PK_devoluciondet] PRIMARY KEY CLUSTERED 
(
	[sucursal] ASC,
	[devolvta] ASC,
	[serie] ASC
)
)
GO
