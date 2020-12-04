CREATE TABLE [dbo].[formaspago](
	[idformapago] [int] IDENTITY(1,1) NOT NULL,
	[formapago] [varchar](50) NULL,
	[descripcion] [varchar](150) NULL,
	[autorizacion] [bit] NULL,
	[cambio] [bit] NULL,
	[deposito] [bit] NULL,
	[idusuario] [int] NULL,
	[fum] [datetime] NULL,
	[pos] BIT NOT NULL DEFAULT 0, 
    [promocion] VARCHAR(2) NULL, 
    CONSTRAINT [PK_formaspago] PRIMARY KEY CLUSTERED 
(
	[idformapago] ASC
)
)
GO


