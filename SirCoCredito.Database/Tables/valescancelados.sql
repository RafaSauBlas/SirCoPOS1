CREATE TABLE [dbo].[valescancelados](
	[iddistrib] [int] NOT NULL,
	[valera] [varchar](14) NOT NULL,
	[valeini] [varchar](14) NOT NULL,
	[valefin] [varchar](14) NOT NULL,
	[idmotivo] [int] NULL,
	[idusuario] [int] NULL,
	[fum] [datetime] NULL,
	[idusuariomodif] [int] NULL,
	[fummodif] [datetime] NULL,
 CONSTRAINT [PK_valescancelados] PRIMARY KEY CLUSTERED 
(
	[iddistrib] ASC,
	[valera] ASC,
	[valeini] ASC
)
)
GO


