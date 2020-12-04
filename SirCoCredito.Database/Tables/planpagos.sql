CREATE TABLE [dbo].[planpagos](
	[distrib] [char](6) NOT NULL,
	[sucursal] [char](2) NOT NULL,
	[nota] [char](6) NOT NULL,
	[negocio] [char](2) NOT NULL,
	[vale] [char](10) NOT NULL,
	[desctoori] [int] NOT NULL,
	[succliente] [char](2) NOT NULL,
	[cliente] [char](6) NOT NULL,
	[idcliente] [bigint] NOT NULL,
	[fechaaplicarcorte] [datetime] NULL,
	[fechacompra] [datetime2](0) NOT NULL,
	[status] [char](2) NOT NULL,
	[importe] [numeric](18, 2) NOT NULL,
	[blindaje] [numeric] (18, 2) NULL,
	[saldo] [numeric](18, 2) NOT NULL,
	[pagos] [bigint] NOT NULL,
	[pagado] [char](1) NOT NULL,
	[observacion] [char](30) NOT NULL,
	[idusuario] [bigint] NOT NULL,
	[fum] [datetime] NOT NULL,
 CONSTRAINT [PK_planpagos_distrib] PRIMARY KEY CLUSTERED 
(
	[distrib] ASC,
	[sucursal] ASC,
	[nota] ASC,
	[negocio] ASC,--nuevo
	[vale] ASC --nuevo
)
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[planpagos] ADD  CONSTRAINT [DF_planpagos_dsctoOri]  DEFAULT ((0)) FOR [desctoori]
GO

ALTER TABLE [dbo].[planpagos] ADD  CONSTRAINT [DF__planpagos__succl__182C9B23]  DEFAULT (N'') FOR [succliente]
GO

ALTER TABLE [dbo].[planpagos] ADD  CONSTRAINT [DF__planpagos__clien__1920BF5C]  DEFAULT (N'') FOR [cliente]
GO

ALTER TABLE [dbo].[planpagos] ADD  CONSTRAINT [DF__planpagos__idcli__1A14E395]  DEFAULT ((0)) FOR [idcliente]
GO

ALTER TABLE [dbo].[planpagos] ADD  CONSTRAINT [DF__planpagos__idusu__1B0907CE]  DEFAULT ((0)) FOR [idusuario]
GO

ALTER TABLE [dbo].[planpagos] ADD  CONSTRAINT [DF__planpagos__fum__1BFD2C07]  DEFAULT (getdate()) FOR [fum]
GO

