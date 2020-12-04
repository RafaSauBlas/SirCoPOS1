CREATE TABLE [dbo].[negocioexterno](
	[idnegexterno] [int] IDENTITY(1,1) NOT NULL,
	[negocio] [varchar](2) NOT NULL,
	[descripcion] [varchar](50) NOT NULL,
	[idusuario] [int] NULL,
	[fum] [datetime] NULL,
	[idusuariomodif] [int] NULL,
	[fummodif] [datetime] NULL,
 CONSTRAINT [PK_negocioexterno] PRIMARY KEY CLUSTERED 
(
	[idnegexterno] ASC,
	[negocio] ASC,
	[descripcion] ASC
)
)
GO


