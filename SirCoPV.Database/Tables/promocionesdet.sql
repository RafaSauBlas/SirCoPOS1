CREATE TABLE [dbo].[promocionesdet](
	[idpromocion] [int] NOT NULL,
	[formapago] [varchar](20) NOT NULL,
	[numunidad] [int] NOT NULL,
	[tipo] [varchar](10) NULL,
	[impfijo] [numeric](18, 2) NULL,
	[descdirecto] [numeric](18, 2) NULL,
	[porcdinelec] [numeric](18, 2) NULL,
	[impbono] [numeric](18, 2) NULL,
	[idusuario] [varchar](8) NULL,
	[fum] [datetime] NULL,
	[descfijo] [numeric](18, 2) NULL, --nuevo
	[vigenciadinero] INT NULL, --nuevo
    
 CONSTRAINT [PK_promocionesdet_1] PRIMARY KEY CLUSTERED 
(
	[idpromocion] ASC,
	[formapago] ASC,
	[numunidad] ASC
)
)
GO


