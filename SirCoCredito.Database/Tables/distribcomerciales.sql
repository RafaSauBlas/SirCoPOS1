CREATE TABLE [dbo].[distribcomerciales](
	[iddistrib] [int] NOT NULL,
	[idnegexterno] [int] NOT NULL,
	[comercial] [varchar](50) NOT NULL,
	[nocuenta] [varchar](50) NOT NULL,
	[limite] [decimal](18, 2) NULL,
	[edocuenta] [image] NULL,
	[edocuenta1] [image] NULL,
	[idusuario] [int] NULL,
	[fum] [datetime] NULL,
	[idusuariomodif] [int] NULL,
	[fummodif] [datetime] NULL,
 CONSTRAINT [PK_distribcomerciales_1] PRIMARY KEY CLUSTERED 
(
	[iddistrib] ASC,
	[idnegexterno] ASC,
	[comercial] ASC,
	[nocuenta] ASC
)
)
GO


