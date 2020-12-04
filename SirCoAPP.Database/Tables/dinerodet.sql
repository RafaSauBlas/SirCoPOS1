CREATE TABLE [dbo].[dinerodet](
	[idsucursal] [int] NOT NULL,
	[cliente] [varchar](6) NOT NULL,
	[sucnota] [varchar](2) NOT NULL,
	[nota] [varchar](6) NOT NULL,
	[descrip] [varchar](100) NULL,
	[vigencia] [date] NULL,
	[importe] [decimal](18, 2) NULL,
	[saldo] [decimal](18, 2) NULL,
	[tipo] [varchar](20) NULL,
	[estatus] [varchar](2) NULL,
	[idusuario] [int] NULL,
	[fum] [datetime] NULL,
 CONSTRAINT [PK_dinerodet] PRIMARY KEY CLUSTERED 
(
	[idsucursal] ASC,
	[cliente] ASC,
	[sucnota] ASC,
	[nota] ASC
)
)
GO


