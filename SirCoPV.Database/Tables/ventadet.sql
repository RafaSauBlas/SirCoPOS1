CREATE TABLE [dbo].[ventadet]
(
	[sucursal] [varchar](2) NOT NULL,
	[venta] [varchar](6) NOT NULL,
	[renglon] [smallint] NOT NULL,
	[marca] [varchar](3) NULL,
	[estilon] [varchar](7) NULL,
	[corrida] [varchar](1) NULL,
	[medida] [varchar](3) NULL,
	[serie] [varchar](13) NOT NULL,
	[idpromocion] [int] NULL,
	[idtipo] [int] NULL,
	[ctd] [smallint] NULL,
	[precio] [numeric](18, 2) NULL,
	[precdesc] [numeric](18, 2) NULL,
	[costomargen] [numeric](18, 2) NULL,
	[iva] [numeric](6, 2) NULL,
	[idusuario] [int] NULL,
	[fum] [datetime] NULL,
 [iddescuentoespecial] INT NULL, --nuevo
 [idpromocionnumero] TINYINT NULL, --nuevo
 [descuentoespecialdesc] VARCHAR(150) NULL, --nuevo
 [rebaja] [numeric](18, 2) NULL, --nuevo
 [notas] VARCHAR(200) NULL, --nuevo
 [idrazon] INT NULL, --nuevo
 CONSTRAINT [PK_ventadet] PRIMARY KEY CLUSTERED 
(
	[sucursal] ASC,
	[venta] ASC,
	[serie] ASC
), 
    CONSTRAINT [FK_ventadet_venta] FOREIGN KEY ([sucursal], [venta]) REFERENCES [dbo].[venta] ([sucursal], [venta])
)
