CREATE TABLE [dbo].[parametros](
	[idparametro] [int] IDENTITY(1,1) NOT NULL,
	[sucursal] [varchar](2) NOT NULL,
	[clave] [varchar](20) NOT NULL,
	[valor] [varchar](50) NOT NULL,
	[idusuario] [int] NULL,
	[fum] [datetime] NULL,
 CONSTRAINT [PK_parametros] PRIMARY KEY CLUSTERED 
(
	[sucursal] ASC,
	[clave] ASC,
	[valor] ASC
)
)
GO

ALTER TABLE [dbo].[parametros] ADD  CONSTRAINT [DF_parametros_fum]  DEFAULT (getdate()) FOR [fum]
GO


