CREATE TABLE [dbo].[agrupacionesdet](
	[idagrupacion] [int] NOT NULL,
	[iddivisiones] [int] NOT NULL,
	[iddepto] [int] NOT NULL,
	[idfamilia] [int] NOT NULL,
	[idlinea] [int] NOT NULL,
	[idl1] [int] NOT NULL,
	[idl2] [int] NOT NULL,
	[idl3] [int] NOT NULL,
	[idl4] [int] NOT NULL,
	[idl5] [int] NOT NULL,
	[idl6] [int] NOT NULL,
	[marca] [varchar](3) NOT NULL,
	[estilon] [varchar](7) NOT NULL,
	[nivel] [varchar](12) NULL,
	[renglon] [int] NULL,
	[idusuario] [varchar](8) NULL,
	[fum] [datetime] NULL,
 CONSTRAINT [PK_agrupacionesdet] PRIMARY KEY CLUSTERED 
(
	[idagrupacion] ASC,
	[iddivisiones] ASC,
	[iddepto] ASC,
	[idfamilia] ASC,
	[idlinea] ASC,
	[idl1] ASC,
	[idl2] ASC,
	[idl3] ASC,
	[idl4] ASC,
	[idl5] ASC,
	[idl6] ASC,
	[marca] ASC,
	[estilon] ASC
)
)
GO


