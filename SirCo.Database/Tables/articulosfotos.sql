CREATE TABLE [dbo].[articulosfotos](
	[marca] [varchar](3) NOT NULL,
	[estilon] [varchar](7) NOT NULL,
	[image_1] [varchar](250) NULL,
	[image_2] [varchar](250) NULL,
	[image_3] [varchar](250) NULL,
	[image_4] [varchar](250) NULL,
	[image_5] [varchar](250) NULL,
	[image_6] [varchar](250) NULL,
	[image_7] [varchar](250) NULL,
	[image_8] [varchar](250) NULL,
	[image_9] [varchar](250) NULL,
	[image_10] [varchar](250) NULL,
	[video_1] [varchar](250) NULL,
	[video_2] [varchar](250) NULL,
	[video_3] [varchar](250) NULL,
	[link_ml] [varchar](250) NULL,
	[link_amazon] [varchar](250) NULL,
	[link_pappos] [varchar](250) NULL,
	[link_linio] [varchar](250) NULL,
	[link_claroshop] [varchar](250) NULL,
	[link_wish] [varchar](250) NULL,
	[idusuario] [int] NULL,
	[fum] [datetime] NULL,
	[idusuariomodif] [int] NULL,
	[fummodif] [datetime] NULL,
	[web] [bit] NULL, -- nuevo
 CONSTRAINT [PK_articulosfotos] PRIMARY KEY CLUSTERED 
(
	[marca] ASC,
	[estilon] ASC
)
)
GO
