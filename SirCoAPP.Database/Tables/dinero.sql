CREATE TABLE [dbo].[dinero](
	[idsucursal] [int] NOT NULL,
	[cliente] [varchar](6) NOT NULL,
	[vigencia] [date] NULL,
	[importe] [decimal](18, 2) NULL,
	[saldo] [decimal](18, 2) NULL,
	[idusuario] [int] NULL,
	[fum] [datetime] NULL,
 CONSTRAINT [PK_dinero] PRIMARY KEY CLUSTERED 
(
	[idsucursal] ASC,
	[cliente] ASC
)
)
GO


