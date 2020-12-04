CREATE TABLE [dbo].[descuentoadicional](
	[iddesctoadi] [int] IDENTITY(1,1) NOT NULL,
	[fechaini] [date] NULL,
	[fechafin] [date] NULL,
	[distribini] [varchar](6) NULL,
	[distribfin] [varchar](6) NULL,
	[sucursal] [varchar](2) NULL,
	[clasificacion] [varchar](2) NULL,
	[status] [varchar](2) NULL,
	[descto] [decimal](18, 2) NULL,
	[motivo] [varchar](50) NULL,
	[idusuario] [int] NULL,
	[fum] [datetime] NULL,
	[idusuariocancela] [int] NULL,
	[fumcancela] [datetime] NULL,
 CONSTRAINT [PK_descuentoadicional] PRIMARY KEY CLUSTERED 
(
	[iddesctoadi] ASC
)
)
GO


