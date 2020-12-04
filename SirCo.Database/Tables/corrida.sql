CREATE TABLE [dbo].[corrida](
	[idarticulo] [int] NOT NULL,
	[marca] [varchar](3) NOT NULL,
	[proveedor] [varchar](3) NOT NULL,
	[estilon] [varchar](7) NOT NULL,
	[corrida] [varchar](1) NOT NULL,
	[iddivisiones] [int] NOT NULL,
	[iddepto] [int] NOT NULL,
	[idfamilia] [int] NOT NULL,
	[idlinea] [int] NOT NULL,
	[idl1] [int] NOT NULL,
	[idl2] [int] NOT NULL,
	[idl3] [int] NOT NULL,
	[idl4] [int] NOT NULL,
	[idl5] [int] NOT NULL,
	[idl6] [int] NOT NULL,
	[intervalo] [varchar](1) NULL,
	[medini] [varchar](3) NULL,
	[medfin] [varchar](3) NULL,
	[costo] [numeric](9, 2) NULL,
	[costomargen] [numeric](9, 2) NULL,
	[precio] [numeric](9, 2) NULL,
	[ult_cmp] [date] NULL,
	[ult_vta] [date] NULL,
	[blofer] [varchar](8) NULL,
	[tipocrr] [varchar](1) NULL,
	[fechor] [datetime] NOT NULL,
	[mayoreo] [numeric](9, 2) NULL,
	[credito] [numeric](9, 2) NULL,
	[preciolleno] [numeric](9, 2) NOT NULL,

	[pesocaja] [numeric](9, 2) NULL,
	[alto] [numeric](9, 2) NULL,
	[fondo] [numeric](9, 2) NULL,
	[frente] [numeric](9, 2) NULL,
	[materialsuela] [int] NULL,
	[materialcalzado] [int] NULL,
	[idrecibo] [int] NULL,
 CONSTRAINT [PK_corrida_marca] PRIMARY KEY CLUSTERED 
(
	[marca] ASC,
	[estilon] ASC,
	[corrida] ASC,
	[proveedor] ASC,
	[iddivisiones] ASC,
	[iddepto] ASC,
	[idfamilia] ASC,
	[idlinea] ASC,
	[idl1] ASC,
	[idl2] ASC,
	[idl3] ASC,
	[idl4] ASC,
	[idl5] ASC,
	[idl6] ASC,
	[idarticulo] ASC
)
)
GO

ALTER TABLE [dbo].[corrida] ADD  DEFAULT ((99999)) FOR [idarticulo]
GO

ALTER TABLE [dbo].[corrida] ADD  DEFAULT (N'999') FOR [proveedor]
GO

ALTER TABLE [dbo].[corrida] ADD  DEFAULT ((0)) FOR [iddivisiones]
GO

ALTER TABLE [dbo].[corrida] ADD  DEFAULT ((0)) FOR [iddepto]
GO

ALTER TABLE [dbo].[corrida] ADD  DEFAULT ((0)) FOR [idfamilia]
GO

ALTER TABLE [dbo].[corrida] ADD  DEFAULT ((0)) FOR [idlinea]
GO

ALTER TABLE [dbo].[corrida] ADD  DEFAULT ((0)) FOR [idl1]
GO

ALTER TABLE [dbo].[corrida] ADD  DEFAULT ((0)) FOR [idl2]
GO

ALTER TABLE [dbo].[corrida] ADD  DEFAULT ((0)) FOR [idl3]
GO

ALTER TABLE [dbo].[corrida] ADD  DEFAULT ((0)) FOR [idl4]
GO

ALTER TABLE [dbo].[corrida] ADD  DEFAULT ((0)) FOR [idl5]
GO

ALTER TABLE [dbo].[corrida] ADD  DEFAULT ((0)) FOR [idl6]
GO

ALTER TABLE [dbo].[corrida] ADD  DEFAULT (NULL) FOR [intervalo]
GO

ALTER TABLE [dbo].[corrida] ADD  DEFAULT (NULL) FOR [medini]
GO

ALTER TABLE [dbo].[corrida] ADD  DEFAULT (NULL) FOR [medfin]
GO

ALTER TABLE [dbo].[corrida] ADD  DEFAULT (NULL) FOR [costo]
GO

ALTER TABLE [dbo].[corrida] ADD  DEFAULT ((0.00)) FOR [costomargen]
GO

ALTER TABLE [dbo].[corrida] ADD  DEFAULT (NULL) FOR [precio]
GO

ALTER TABLE [dbo].[corrida] ADD  DEFAULT (NULL) FOR [ult_cmp]
GO

ALTER TABLE [dbo].[corrida] ADD  DEFAULT (NULL) FOR [ult_vta]
GO

ALTER TABLE [dbo].[corrida] ADD  DEFAULT (N'') FOR [blofer]
GO

ALTER TABLE [dbo].[corrida] ADD  DEFAULT (NULL) FOR [tipocrr]
GO

ALTER TABLE [dbo].[corrida] ADD  DEFAULT (getdate()) FOR [fechor]
GO

ALTER TABLE [dbo].[corrida] ADD  DEFAULT ((0.00)) FOR [mayoreo]
GO

ALTER TABLE [dbo].[corrida] ADD  DEFAULT ((0.00)) FOR [credito]
GO

ALTER TABLE [dbo].[corrida] ADD  DEFAULT ((0.00)) FOR [preciolleno]
GO

