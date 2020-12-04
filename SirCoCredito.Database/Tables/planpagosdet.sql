CREATE TABLE [dbo].[planpagosdet](
	[sucursal] [char](2) NOT NULL,
	[nota] [char](6) NOT NULL,
	[fechaaplicar] [datetime2](0) NOT NULL,
	[pago] [bigint] NOT NULL,
	[pagos] [bigint] NOT NULL,
	[fechavencimiento] [datetime2](0) NOT NULL,
	[importe] [numeric](18, 2) NOT NULL,
	[abono] [numeric](18, 2) NOT NULL,
	[descuento] [numeric](18, 2) NOT NULL,
	[interes] [numeric](18, 2) NOT NULL,
	[gastoscobranza] [numeric](18, 2) NOT NULL,
	[pagado] [char](1) NOT NULL,
	[fechapago] [datetime2](0) NOT NULL,
	[tipopago] [char](1) NOT NULL,
	[cobrador] [int] NOT NULL,
	[idpago] [bigint] NOT NULL,
	[idconvenio] [bigint] NOT NULL,
	[idusuario] [bigint] NOT NULL,
	[fum] [datetime] NOT NULL,

	[negocio] [char](2) NOT NULL,--nuevo
	[vale] [char](10) NOT NULL,--nuevo
	[distrib] [char](6) NOT NULL, -- nuevo
 CONSTRAINT [PK_planpagosdet_sucursal] PRIMARY KEY CLUSTERED 
(
	[sucursal] ASC,
	[nota] ASC,
	[fechaaplicar] ASC,
	[pago] ASC,

	[negocio] ASC,--nuevo
	[vale] ASC,--nuevo
	[distrib] ASC -- nuevo
)
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[planpagosdet] ADD  CONSTRAINT [DF__planpagos__fecha__1ED998B2]  DEFAULT ('1900-01-01 00:00:00') FOR [fechavencimiento]
GO

ALTER TABLE [dbo].[planpagosdet] ADD  CONSTRAINT [DF__planpagos__abono__1FCDBCEB]  DEFAULT ((0.00)) FOR [abono]
GO

ALTER TABLE [dbo].[planpagosdet] ADD  CONSTRAINT [DF__planpagos__descu__20C1E124]  DEFAULT ((0.00)) FOR [descuento]
GO

ALTER TABLE [dbo].[planpagosdet] ADD  CONSTRAINT [DF__planpagos__inter__21B6055D]  DEFAULT ((0.00)) FOR [interes]
GO

ALTER TABLE [dbo].[planpagosdet] ADD  CONSTRAINT [DF__planpagos__gasto__22AA2996]  DEFAULT ((0.00)) FOR [gastoscobranza]
GO

ALTER TABLE [dbo].[planpagosdet] ADD  CONSTRAINT [DF__planpagos__fecha__239E4DCF]  DEFAULT ('1900-01-01 00:00:00') FOR [fechapago]
GO

ALTER TABLE [dbo].[planpagosdet] ADD  CONSTRAINT [DF__planpagos__tipop__24927208]  DEFAULT (N'') FOR [tipopago]
GO

ALTER TABLE [dbo].[planpagosdet] ADD  CONSTRAINT [DF__planpagos__cobra__25869641]  DEFAULT ((0)) FOR [cobrador]
GO

ALTER TABLE [dbo].[planpagosdet] ADD  CONSTRAINT [DF__planpagos__idpag__267ABA7A]  DEFAULT ((0)) FOR [idpago]
GO

ALTER TABLE [dbo].[planpagosdet] ADD  CONSTRAINT [DF__planpagos__idcon__276EDEB3]  DEFAULT ((0)) FOR [idconvenio]
GO

ALTER TABLE [dbo].[planpagosdet] ADD  CONSTRAINT [DF__planpagos__idusu__286302EC]  DEFAULT ((0)) FOR [idusuario]
GO

ALTER TABLE [dbo].[planpagosdet] ADD  CONSTRAINT [DF__planpagosde__fum__29572725]  DEFAULT (getdate()) FOR [fum]
GO

