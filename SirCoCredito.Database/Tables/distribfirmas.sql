CREATE TABLE [dbo].[distribfirmas](
	[distrib] [varchar](6) NOT NULL,
	[principal] [smallint] NOT NULL,
	[nombre] [varchar](250) NOT NULL,
	[domicilio] [varchar](250) NOT NULL,
	[numfirma] [smallint] NOT NULL,
	[firma] [image] NULL,
	[pagare] [image] NULL,
	[idusuario] [int] NULL,
	[fum] [datetime] NULL,
	[idusuariomodif] [int] NULL,
	[fummodif] [datetime] NULL,
 CONSTRAINT [PK_distribfirmas] PRIMARY KEY CLUSTERED 
(
	[distrib] ASC,
	[principal] ASC,
	[nombre] ASC,
	[domicilio] ASC,
	[numfirma] ASC
)
)
GO


