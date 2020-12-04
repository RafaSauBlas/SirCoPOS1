CREATE TABLE [dbo].[promociones](
	[idpromocion] [int] NOT NULL,
	[nombre] [varchar](30) NULL,
	[fecha] [date] NULL,
	[tipo] [varchar](10) NULL,
	[estatus] [varchar](15) NULL,
	[iniciopromo] [datetime] NULL,
	[finpromo] [datetime] NULL,
	[preciero] [varchar](30) NULL,
	[senalizador] [varchar](30) NULL,
	[clasificacion] [varchar](10) NULL,
	[imagen] [image] NULL,
	[minunicompra] [int] NULL,
	[minimpcompra] [numeric](18, 2) NULL,
	[unipromo] [int] NULL,
	[acumulable] [varchar](2) NULL,
	[paresunicos] [varchar](2) NULL,
	[clinoregis] [varchar](2) NULL,
	[idusuariocaptura] [varchar](8) NULL,
	[fumcaptura] [datetime] NULL,
	[idusuarioaplica] [varchar](8) NULL,
	[fumaplica] [datetime] NULL,
	[idusuariopausa] [varchar](8) NULL,
	[fumpausa] [datetime] NULL,
	[idusuariocancela] [varchar](8) NULL,
	[fumcancela] [datetime] NULL,
	[idusuario] [varchar](8) NULL,
	[fum] [datetime] NULL,
	[promosrequeridas] TINYINT NULL, --nuevo
	[duplicados] TINYINT NULL, --nuevo
	[clienterequerido] BIT NULL, --nuevo    
	[clientehistorico] INT NULL, --nuevo
	[empleadorequerido] BIT NULL, --nuevo
	[sinmaximo] BIT NULL, --nuevo 
	[importeticket] BIT NULL, --nuevo 
	[empleadocantidad] INT NULL, --nuevo 
	[empleadocantidadtipo] VARCHAR(2) NULL, --nuevo 
    CONSTRAINT [PK_promociones] PRIMARY KEY CLUSTERED 
(
	[idpromocion] ASC
)
)
GO


