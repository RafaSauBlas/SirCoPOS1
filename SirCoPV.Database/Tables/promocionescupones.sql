CREATE TABLE [dbo].[promocionescupones](
	[idpromocion] [int] NOT NULL,
	[idcupon] [int] NOT NULL,
	[idusuario] [varchar](8) NULL,
	[fum] [datetime] NULL,
 CONSTRAINT [PK_promocionescupones] PRIMARY KEY CLUSTERED 
(
	[idpromocion] ASC,
	[idcupon] ASC
)
)
GO


