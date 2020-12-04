CREATE TABLE [dbo].[promocioncred](
	[idpromocioncred] [nchar](10) NOT NULL,
	[sucursal] [varchar](2) NULL,
	[status] [varchar](2) NULL,
	[fechaaplicar] [date] NULL,
	[fechainicio] [date] NULL,
	[fechafin] [date] NULL,
	[pagosmin] [int] NULL,
	[pagosmax] [int] NULL,
	[idusuario] [int] NULL,
	[fum] [datetime] NULL,
 CONSTRAINT [PK_promocioncred] PRIMARY KEY CLUSTERED 
(
	[idpromocioncred] ASC
)
)
GO


