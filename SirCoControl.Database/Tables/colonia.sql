CREATE TABLE [dbo].[colonia](
	[idcolonia] [int] IDENTITY(1,1) NOT NULL,
	[idestado] [int] NOT NULL,
	[idciudad] [int] NOT NULL,
	[colonia] [varchar](50) NOT NULL,
	[codigopostal] [varchar](5) NOT NULL,
	[idusuario] [int] NULL,
	[fum] [datetime] NULL,
 CONSTRAINT [PK_colonia] PRIMARY KEY CLUSTERED 
(
	[idcolonia] ASC,
	[idestado] ASC,
	[idciudad] ASC,
	[colonia] ASC,
	[codigopostal] ASC
)
)
GO


