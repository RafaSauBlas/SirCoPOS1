CREATE TABLE [dbo].[cliente](
	[idcliente] [int] IDENTITY(1,1) NOT NULL,
	[idsucursal] [int] NULL,
	[cliente] [varchar](6) NULL,
	[nombrecompleto] [varchar](250) NULL,
	[nombre] [varchar](100) NULL,
	[appaterno] [varchar](100) NULL,
	[apmaterno] [varchar](100) NULL,
	[sexo] [varchar](1) NULL,
	[idestado] [int] NULL,
	[idciudad] [int] NULL,
	[idcolonia] [int] NULL,
	[codigopostal] [varchar](5) NULL,
	[calle] [varchar](250) NULL,
	[numero] [smallint] NULL,
	[celular1] [varchar](10) NULL,
	[celular] [varchar](10) NULL,--nuevo
	[email] [varchar](150) NULL,
	[fecalta] [date] NULL,
	[idusuario] [int] NULL,
	[fum] [datetime] NULL,
	[idusuariomodif] [int] NULL,
	[fummodif] [datetime] NULL,
	[sistema] [varchar](10) NULL,
 [foto] VARBINARY(MAX) NULL, --nuevo 
 [idempleado] INT NULL, --nuevo
 CONSTRAINT [PK_cliente] PRIMARY KEY CLUSTERED 
(
	[idcliente] ASC
)
)
GO


