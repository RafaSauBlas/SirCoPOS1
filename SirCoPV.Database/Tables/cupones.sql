CREATE TABLE [dbo].[cupones](
	[idcupon] [int] NOT NULL,
	[nombre] [varchar](25) NULL,
	[descripcion] [varchar](150) NULL,
	[restricciones] [varchar](150) NULL,
	[fecha] [date] NULL,
	[estatus] [varchar](15) NULL,
	[tipo] [varchar](15) NULL,
	[imagen] [image] NULL,
	[fecini] [date] NULL,
	[fecfin] [date] NULL,
	[idusuariocaptura] [varchar](8) NULL,
	[fumcaptura] [datetime] NULL,
	[idusuarioactiva] [varchar](8) NULL,
	[fumactiva] [datetime] NULL,
	[idusuariocancela] [varchar](8) NULL,
	[fumcancela] [datetime] NULL,
	[idusuario] [varchar](8) NULL,
	[fum] [datetime] NULL,
 CONSTRAINT [PK_cupones] PRIMARY KEY CLUSTERED 
(
	[idcupon] ASC
)
)
GO


