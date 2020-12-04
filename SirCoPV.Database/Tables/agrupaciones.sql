CREATE TABLE [dbo].[agrupaciones](
	[idagrupacion] [int] NOT NULL,
	[nombre] [varchar](30) NULL,
	[fecha] [date] NULL,
	[idusuario] [varchar](8) NULL,
	[fum] [datetime] NULL,
 CONSTRAINT [PK_agrupaciones] PRIMARY KEY CLUSTERED 
(
	[idagrupacion] ASC
)
)
GO


