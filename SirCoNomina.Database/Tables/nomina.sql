CREATE TABLE [dbo].[nomina](
	[idperiodo] [int] NOT NULL,
	[tiponom] [varchar](1) NOT NULL,
	[sucursal] [varchar](2) NOT NULL,
	[idempleado] [int] NOT NULL,
	[impdiastrab] [decimal](9, 2) NOT NULL,
	[impprima] [decimal](9, 2) NOT NULL,
	[diasvac] [decimal](4, 2) NOT NULL,
	[diascot] [decimal](4, 2) NOT NULL,
	[impdescanso] [decimal](9, 2) NOT NULL,
	[incap] [decimal](4, 2) NOT NULL,
	[impextras] [decimal](9, 2) NULL,
	[impretardo] [decimal](9, 2) NOT NULL,
	[impotrasd] [decimal](9, 2) NOT NULL,
	[impotrasp] [decimal](9, 2) NOT NULL,
	[pago] [decimal](9, 2) NOT NULL,
	[usuario] [varchar](8) NOT NULL,
	[fum] [datetime] NULL,
	[usumodif] [varchar](8) NOT NULL,
	[fummodif] [datetime] NULL,
 CONSTRAINT [pk_nomina] PRIMARY KEY CLUSTERED 
(
	[tiponom] ASC,
	[sucursal] ASC,
	[idperiodo] ASC,
	[idempleado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[nomina] ADD  DEFAULT (NULL) FOR [impextras]
GO

ALTER TABLE [dbo].[nomina] ADD  DEFAULT (getdate()) FOR [fum]
GO

ALTER TABLE [dbo].[nomina] ADD  DEFAULT ('') FOR [usumodif]
GO

ALTER TABLE [dbo].[nomina] ADD  DEFAULT ('0000-00-00 00:00:00') FOR [fummodif]
GO


