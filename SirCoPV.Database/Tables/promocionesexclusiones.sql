CREATE TABLE [dbo].[promocionesexclusiones](
	[idpromocion] [int] NOT NULL,
	[marca] [varchar](3) NOT NULL,
	[estilon] [varchar](7) NOT NULL,
	[idusuario] [varchar](8) NULL,
	[fum] [datetime] NULL,
 CONSTRAINT [PK_promocionesexclusiones] PRIMARY KEY CLUSTERED 
(
	[idpromocion] ASC,
	[marca] ASC,
	[estilon] ASC
)
)
GO


