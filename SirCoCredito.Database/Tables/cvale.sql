CREATE TABLE [dbo].[cvale](
	[sucursal] [varchar](2) NOT NULL,
	[cvale] [varchar](10) NOT NULL,
	[status] [varchar](2) NULL,
	[fecha] [date] NULL,
	[distrib] [varchar](6) NULL,
	[succte] [varchar](2) NULL,
	[cliente] [varchar](6) NULL,
	[caduca] [date] NULL,
	[importe] [decimal](18, 2) NULL,
	[saldo] [decimal](18, 2) NULL,
	[referenc] [varchar](10) NULL,
	[observa] [varchar](60) NULL,
	[idusuario] [int] NULL,
	[fum] [datetime] NULL,
 CONSTRAINT [PK_cvale] PRIMARY KEY CLUSTERED 
(
	[sucursal] ASC,
	[cvale] ASC
)
)
GO
