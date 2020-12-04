CREATE TABLE [dbo].[valeras](
	[iddistrib] [int] NOT NULL,
	[valera] [varchar](14) NOT NULL,
	[valeini] [varchar](14) NULL,
	[valefin] [varchar](14) NULL,
	[entrega] [date] NULL,
	[recoge] [varchar](150) NULL,
	[idusuario] [int] NULL,
	[fum] [datetime] NULL,
	[idusuariomodif] [int] NULL,
	[fummodif] [datetime] NULL,
 CONSTRAINT [PK_valeras] PRIMARY KEY CLUSTERED 
(
	[iddistrib] ASC,
	[valera] ASC
)
)
GO


