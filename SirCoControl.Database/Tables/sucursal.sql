CREATE TABLE [dbo].[sucursal](
	[idsucursal] [int] NOT NULL,
	[sucursal] [varchar](2) NOT NULL,
	[cvedwh] [varchar](2) NOT NULL,
	[descrip] [varchar](20) NOT NULL,
	[calle] [varchar](60) NULL,
	[colonia] [varchar](60) NULL,
	[ciudad] [varchar](60) NULL,
	[estado] [varchar](60) NULL,
	[codpostal] [varchar](5) NULL,
	[ordecomp] [int] NULL,
	[factprov] [int] NULL,
	[devoprov] [int] NULL,
	[traspaso] [int] NULL,
	[ajustes] [int] NULL,
	[invfis] [int] NULL,
	[cajas] [int] NULL,
	[devolvta] [int] NULL,
	[bloferta] [int] NULL,
	[cambprec] [int] NULL,
	[clientes] [int] NULL,
	[cvale] [int] NULL,
	[visible] [varchar](1) NOT NULL,
	[idplaza] [int] NOT NULL,
	[venta] [varchar](1) NOT NULL,
	[minicio] [varchar](1) NOT NULL,
	[prioridad] [smallint] NULL,
	[traspresaut] [smallint] NULL,
	[vercosto] [varchar](1) NULL,
	[porcmuestras] [decimal](10, 2) NULL,
	[prioridadtraspaso] [smallint] NULL,
	[abonos] INT NULL, --nuevo
	[ordenweb] TINYINT NULL, --nuevo
    [web] BIT NULL, --nuevo
 CONSTRAINT [PK_sucursal] PRIMARY KEY CLUSTERED 
(
	[idsucursal] ASC,
	[sucursal] ASC
)
)