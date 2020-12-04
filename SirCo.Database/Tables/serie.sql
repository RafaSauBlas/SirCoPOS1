CREATE TABLE [dbo].[serie]
(
	[serie] [varchar](13) NOT NULL,
	[sucursal] [varchar](2) NOT NULL,
	[status] [varchar](2) NOT NULL,
	[marca] [varchar](3) NOT NULL,
	[estilon] [varchar](7) NOT NULL,
	[medida] [varchar](3) NOT NULL,
	[sucurdes] [varchar](2) NOT NULL,
	[idfolio] [int] NOT NULL,
	[idarticulo] [int] NOT NULL,
	[precioini] [decimal](18, 2) NULL,
	[costoini] [decimal](18, 2) NULL,
	[preciovta] [decimal](18, 2) NULL,
	[ultcosto] [decimal](18, 2) NULL,
	[proveedors] [varchar](3) NULL,
	[costomargens] [decimal](18, 2) NULL,
	[fums] [datetime] NULL,
	[idusuariocaja] INT NULL, -- nuevo
	[fechacaja] DATETIME NULL, -- nuevo
	[web] UNIQUEIDENTIFIER NULL, -- nuevo
    CONSTRAINT [PK_serie] PRIMARY KEY CLUSTERED 
(
	[serie] ASC,
	[sucursal] ASC,
	[status] ASC,
	[marca] ASC,
	[estilon] ASC,
	[medida] ASC,
	[sucurdes] ASC,
	[idfolio] ASC,
	[idarticulo] ASC
)
)
