CREATE TABLE [dbo].[calendario](
	[tipocredito] [varchar](50) NOT NULL,
	[tipo] [varchar](50) NOT NULL,
	[fechaini] [date] NOT NULL,
	[fechafin] [date] NOT NULL,
	[folioco] [varchar](50) NULL,
	[fechaaplicarcorte] [date] NULL,
	[fechaaplicar] [date] NULL,
	[fechapcini] [date] NULL,
	[fechapcfin] [date] NULL,
	[descrip] [varchar](45) NULL,
	[fechapagocliente] [date] NULL,
	[fechavencecliente] [date] NULL,
	[fum] [datetime] NULL,
 CONSTRAINT [PK_calendario] PRIMARY KEY CLUSTERED 
(
	[tipocredito] ASC,
	[tipo] ASC,
	[fechaini] ASC,
	[fechafin] ASC
)
)
GO

ALTER TABLE [dbo].[calendario] ADD  CONSTRAINT [DF_calendario_tipocredito]  DEFAULT ('DISTRIBUIDOR') FOR [tipocredito]
GO

ALTER TABLE [dbo].[calendario] ADD  CONSTRAINT [DF_calendario_fum]  DEFAULT (getdate()) FOR [fum]
GO


