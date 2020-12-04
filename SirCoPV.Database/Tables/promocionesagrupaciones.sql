CREATE TABLE [dbo].[promocionesagrupaciones](
	[idpromocion] [int] NOT NULL,
	[idagrupacioncompra] [int] NOT NULL,
	[idagrupacionpromo] [int] NOT NULL,
	[renglon] [int] NULL,
	[idusuario] [varchar](8) NULL,
	[fum] [datetime] NULL,
 CONSTRAINT [PK_pomocionesagrupaciones] PRIMARY KEY CLUSTERED 
(
	[idpromocion] ASC,
	[idagrupacioncompra] ASC,
	[idagrupacionpromo] ASC
)
)
GO


