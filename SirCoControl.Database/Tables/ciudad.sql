CREATE TABLE [dbo].[ciudad](
	[idciudad] [int] IDENTITY(1,1) NOT NULL,
	[idestado] [int] NOT NULL,
	[ciudad] [varchar](50) NULL,
	[idusuario] [int] NULL,
	[fum] [datetime] NULL,
 CONSTRAINT [PK_ciudad] PRIMARY KEY CLUSTERED 
(
	[idciudad] ASC,
	[idestado] ASC
)
)
GO


