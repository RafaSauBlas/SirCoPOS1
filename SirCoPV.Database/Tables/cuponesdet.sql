CREATE TABLE [dbo].[cuponesdet](
	[idcupon] [int] NOT NULL,
	[folio] [varchar](20) NOT NULL,
	[estatus] [varchar](15) NULL,
	[idusuario] [varchar](8) NULL,
	[fum] [datetime] NULL,
 [referencia] VARCHAR(8) NULL,--nuevo 
 [idcliente] INT NULL, --nuevo    
    CONSTRAINT [PK_cuponesdet] PRIMARY KEY CLUSTERED 
(
	[idcupon] ASC,
	[folio] ASC
)
)
GO


