CREATE TABLE [dbo].[promocionesrecurrencia](
	[idpromocion] [int] NOT NULL,
	[dia] [varchar](9) NOT NULL,
	[horainicio] [varchar](5) NOT NULL,
	[horafin] [varchar](5) NULL,
	[idusuario] [varchar](8) NULL,
	[fum] [datetime] NULL,
 CONSTRAINT [PK_promocionesrecurrencia] PRIMARY KEY CLUSTERED 
(
	[idpromocion] ASC,
	[dia] ASC,
	[horainicio] ASC
)
)
GO


