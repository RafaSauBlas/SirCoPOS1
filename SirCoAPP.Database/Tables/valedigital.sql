CREATE TABLE [dbo].[valedigital](
	[idvaledigital] [int] IDENTITY(1,1) NOT NULL,
	[distrib] [varchar](6) NOT NULL,
	[idauxiliar] [int] NOT NULL,
	[idcliente] [int] NOT NULL,
	[idvale] [int] NOT NULL,
	[codigoqr] [varchar](100) NULL,
	[vigencia] [date] NULL,
	[estatus] [varchar](2) NULL,
	[imppedido] [decimal](18, 2) NULL,
	[impotorgado] [decimal](18, 2) NULL,
	[disponible] [decimal](18, 2) NULL,
	[electronica] [bit] NULL,
	[promocion] [bit] NULL,
	[comentarios] [varchar](150) NULL,
	[fum] [datetime] NULL,
 CONSTRAINT [PK_valedigital] PRIMARY KEY CLUSTERED 
(
	[idvaledigital] ASC,
	[distrib] ASC,
	[idauxiliar] ASC,
	[idcliente] ASC,
	[idvale] ASC
)
)
GO


