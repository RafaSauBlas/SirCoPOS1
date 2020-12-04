CREATE TABLE [dbo].[promocionesplazas](
	[idpromocion] [int] NOT NULL,
	[plaza] [varchar](2) NOT NULL,
	[sucursal] [varchar](2) NOT NULL,
	[idusuario] [varchar](8) NULL,
	[fum] [datetime] NULL,
 CONSTRAINT [PK_promocionesplazas] PRIMARY KEY CLUSTERED 
(
	[idpromocion] ASC,
	[plaza] ASC,
	[sucursal] ASC
)
)
GO


