CREATE TABLE [dbo].[nominadet](
	[idperiodo] [int] NOT NULL,
	[tiponom] [varchar](1) NOT NULL,
	[idempleado] [int] NOT NULL,
	[idpercdeduc] [int] NOT NULL,
	[idrepetitivo] [int] NOT NULL,
	[unidades] [decimal](8, 2) NOT NULL,
	[impgrav] [decimal](9, 2) NULL,
	[impexento] [decimal](9, 2) NULL,
	[usuario] [varchar](8) NOT NULL,
	[fum] [datetime] NULL,
	[usumodif] [varchar](8) NOT NULL,
	[fummodif] [datetime] NULL,
	[movimiento] UNIQUEIDENTIFIER NULL, --nuevo
    CONSTRAINT [pk_nominadet] PRIMARY KEY CLUSTERED 
(
	[idperiodo] ASC,
	[idpercdeduc] ASC,
	[idempleado] ASC,
	[idrepetitivo] ASC,
	[tiponom] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[nominadet] ADD  DEFAULT (NULL) FOR [impgrav]
GO

ALTER TABLE [dbo].[nominadet] ADD  DEFAULT (NULL) FOR [impexento]
GO

ALTER TABLE [dbo].[nominadet] ADD  DEFAULT (getdate()) FOR [fum]
GO

ALTER TABLE [dbo].[nominadet] ADD  DEFAULT ('') FOR [usumodif]
GO

ALTER TABLE [dbo].[nominadet] ADD  DEFAULT ('0000-00-00 00:00:00') FOR [fummodif]
GO


