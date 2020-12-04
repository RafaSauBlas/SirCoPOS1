CREATE TABLE [dbo].[empleado](
	[idempleado] [int] NOT NULL,
	[idplaza] [int] NULL,
	[clave] [varchar](6) NOT NULL,
	[appaterno] [varchar](30) NOT NULL,
	[apmaterno] [varchar](30) NOT NULL,
	[nombre] [varchar](30) NOT NULL,
	[iddepto] [int] NOT NULL,
	[idpuesto] [int] NOT NULL,
	[vendedor] [varchar](2) NOT NULL,
	[idfrecpago] [int] NOT NULL,
	[jornada] [decimal](4, 2) NOT NULL,
	[entrada] [varchar](8) NOT NULL,
	[comida] [decimal](4, 2) NOT NULL,
	[descanso] [int] NOT NULL,
	[extras] [varchar](1) NOT NULL,
	[baja] [datetime] NOT NULL,
	[ingreso] [datetime] NOT NULL,
	[estatus] [varchar](1) NOT NULL,
	[bonofijo] [decimal](9, 2) NOT NULL,
	[tsalario] [varchar](1) NOT NULL,
	[zsalario] [varchar](1) NOT NULL,
	[porinteg] [decimal](5, 2) NOT NULL,
	[sdiario] [decimal](5, 2) NOT NULL,
	[shora] [decimal](5, 2) NOT NULL,
	[sinteg] [decimal](5, 2) NOT NULL,
	[ptu] [varchar](1) NOT NULL,
	[imss] [varchar](1) NOT NULL,
	[bono] [varchar](1) NOT NULL,
	[cuenta] [varchar](20) NOT NULL,
	[licencia] [varchar](18) NOT NULL,
	[unimed] [varchar](3) NOT NULL,
	[calle] [varchar](100) NOT NULL,
	[colonia] [varchar](60) NOT NULL,
	[ciudad] [varchar](40) NOT NULL,
	[estado] [varchar](40) NOT NULL,
	[codpos] [varchar](5) NOT NULL,
	[numext] [varchar](5) NOT NULL,
	[numint] [varchar](5) NOT NULL,
	[sexo] [varchar](1) NOT NULL,
	[telef1] [varchar](15) NOT NULL,
	[telef2] [varchar](15) NOT NULL,
	[email] [varchar](80) NOT NULL,
	[passemail] [varchar](30) NOT NULL,
	[nacim] [date] NOT NULL,
	[ciudadnac] [varchar](40) NOT NULL,
	[edocivil] [varchar](1) NOT NULL,
	[numhijos] [int] NOT NULL,
	[nompadre] [varchar](100) NOT NULL,
	[nommadre] [varchar](100) NOT NULL,
	[rfc] [varchar](18) NOT NULL,
	[curp] [varchar](18) NOT NULL,
	[noimss] [varchar](18) NOT NULL,
	[checa] [varchar](1) NOT NULL,
	[usuariosistema] [varchar](8) NOT NULL,
	[password] [varchar](30) NULL,
	[saldo] [decimal](6, 2) NOT NULL,
	[limcred] [decimal](6, 2) NOT NULL,
	[disponible] [decimal](6, 2) NOT NULL,
	[usuario] [varchar](8) NOT NULL,
	[fum] [datetime] NOT NULL,
	[usumodif] [varchar](8) NULL,
	[fummodif] [datetime] NULL,
	[costos] [int] NOT NULL,
	[ventas] [int] NOT NULL,
	[beneficiario1] [varchar](150) NOT NULL,
	[parentesco1] [varchar](50) NOT NULL,
	[porcentaje1] [int] NOT NULL,
	[beneficiario2] [varchar](150) NOT NULL,
	[parentesco2] [varchar](50) NOT NULL,
	[porcentaje2] [int] NOT NULL,
	[cuentabajio] [varchar](20) NOT NULL,
 [idcliente] INT NULL, -- nuevo
 [authkey] NVARCHAR(100) NULL, --nuevo
    CONSTRAINT [pk_empleado] PRIMARY KEY CLUSTERED 
(
	[idempleado] ASC,
	[clave] ASC,
	[iddepto] ASC,
	[idpuesto] ASC,
	[vendedor] ASC
)
)
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__clave__22401542]  DEFAULT ('') FOR [clave]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__appate__2334397B]  DEFAULT ('') FOR [appaterno]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__apmate__24285DB4]  DEFAULT ('') FOR [apmaterno]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__nombre__251C81ED]  DEFAULT ('') FOR [nombre]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__iddept__2610A626]  DEFAULT ((0)) FOR [iddepto]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__idpues__2704CA5F]  DEFAULT ((0)) FOR [idpuesto]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__vended__27F8EE98]  DEFAULT ('') FOR [vendedor]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__idfrec__28ED12D1]  DEFAULT ((0)) FOR [idfrecpago]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__descan__29E1370A]  DEFAULT ((7)) FOR [descanso]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__ingres__2AD55B43]  DEFAULT (getdate()) FOR [ingreso]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__estatu__2BC97F7C]  DEFAULT ('') FOR [estatus]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__bonofi__2CBDA3B5]  DEFAULT ('0.00') FOR [bonofijo]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__tsalar__2DB1C7EE]  DEFAULT ('') FOR [tsalario]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__porint__2EA5EC27]  DEFAULT ('0.00') FOR [porinteg]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__sdiari__2F9A1060]  DEFAULT ('0.00') FOR [sdiario]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__shora__308E3499]  DEFAULT ('0.00') FOR [shora]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__sinteg__318258D2]  DEFAULT ('0.00') FOR [sinteg]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__ptu__32767D0B]  DEFAULT ('') FOR [ptu]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__imss__336AA144]  DEFAULT ('') FOR [imss]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__cuenta__345EC57D]  DEFAULT ('') FOR [cuenta]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__licenc__3552E9B6]  DEFAULT ('') FOR [licencia]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__unimed__36470DEF]  DEFAULT ('') FOR [unimed]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__calle__373B3228]  DEFAULT ('') FOR [calle]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__coloni__382F5661]  DEFAULT ('') FOR [colonia]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__ciudad__39237A9A]  DEFAULT ('') FOR [ciudad]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__codpos__3A179ED3]  DEFAULT ('') FOR [codpos]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__numext__3B0BC30C]  DEFAULT ('') FOR [numext]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__numint__3BFFE745]  DEFAULT ('') FOR [numint]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__telef1__3CF40B7E]  DEFAULT ('') FOR [telef1]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__telef2__3DE82FB7]  DEFAULT ('') FOR [telef2]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__email__3EDC53F0]  DEFAULT ('') FOR [email]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__passem__3FD07829]  DEFAULT ('""') FOR [passemail]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__nacim__40C49C62]  DEFAULT (getdate()) FOR [nacim]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__ciudad__41B8C09B]  DEFAULT ('') FOR [ciudadnac]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__edociv__42ACE4D4]  DEFAULT ('') FOR [edocivil]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__numhij__43A1090D]  DEFAULT ((0)) FOR [numhijos]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__nompad__44952D46]  DEFAULT ('') FOR [nompadre]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__nommad__4589517F]  DEFAULT ('') FOR [nommadre]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__rfc__467D75B8]  DEFAULT ('') FOR [rfc]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__curp__477199F1]  DEFAULT ('') FOR [curp]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__noimss__4865BE2A]  DEFAULT ('') FOR [noimss]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__checa__4959E263]  DEFAULT ('S') FOR [checa]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__usuari__4A4E069C]  DEFAULT ('') FOR [usuariosistema]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__passwo__4B422AD5]  DEFAULT (NULL) FOR [password]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__saldo__4C364F0E]  DEFAULT ('0.00') FOR [saldo]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__limcre__4D2A7347]  DEFAULT ('0.00') FOR [limcred]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__dispon__4E1E9780]  DEFAULT ('0.00') FOR [disponible]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__usuari__4F12BBB9]  DEFAULT ('') FOR [usuario]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__fum__5006DFF2]  DEFAULT (getdate()) FOR [fum]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__usumod__50FB042B]  DEFAULT ('0000-00-00 00:00:00') FOR [usumodif]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__fummod__51EF2864]  DEFAULT ('0000-00-00 00:00:00') FOR [fummodif]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__costos__52E34C9D]  DEFAULT ((0)) FOR [costos]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__ventas__53D770D6]  DEFAULT ((0)) FOR [ventas]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__benefi__54CB950F]  DEFAULT ('') FOR [beneficiario1]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__parent__55BFB948]  DEFAULT ('') FOR [parentesco1]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__porcen__56B3DD81]  DEFAULT ((0)) FOR [porcentaje1]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__benefi__57A801BA]  DEFAULT ('') FOR [beneficiario2]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__parent__589C25F3]  DEFAULT ('') FOR [parentesco2]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__porcen__59904A2C]  DEFAULT ((0)) FOR [porcentaje2]
GO

ALTER TABLE [dbo].[empleado] ADD  CONSTRAINT [DF__empleado__cuenta__5A846E65]  DEFAULT ('') FOR [cuentabajio]
GO


