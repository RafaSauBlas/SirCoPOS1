INSERT [dbo].[sucursal] ([idsucursal], [sucursal], [cvedwh], [descrip], [calle], [colonia], [ciudad], [estado], [codpostal], [ordecomp], [factprov], [devoprov], [traspaso], [ajustes], [invfis], [cajas], [devolvta], [bloferta], [cambprec], [visible], [idplaza], [venta], [minicio], [prioridad], [traspresaut], [vercosto], [porcmuestras], [prioridadtraspaso]) VALUES (1, N'01', N'01', N'JUAREZ', N'AV. JUAREZ # 1015 PTE.', N'CENTRO', N'TORREON', N'COAH', N'27000', 5371, 5439, 1019, 127082, 2871, 210657, 434053, 27851, NULL, NULL, N'S', 1, N'S', N'S', 1, 1, N'1', CAST(29.00 AS Decimal(10, 2)), 2)
GO
INSERT [dbo].[sucursal] ([idsucursal], [sucursal], [cvedwh], [descrip], [calle], [colonia], [ciudad], [estado], [codpostal], [ordecomp], [factprov], [devoprov], [traspaso], [ajustes], [invfis], [cajas], [devolvta], [bloferta], [cambprec], [clientes], [cvale], [visible], [idplaza], [venta], [minicio], [prioridad], [traspresaut], [vercosto], [porcmuestras], [prioridadtraspaso], [abonos], [web], [ordenweb]) VALUES (8, N'08', N'03', N'MATRIZ', N'HIDALGO #1462 PTE', N'CENTRO', N'TORREON', N'COAH', N'27000', 5490, 4594, 1062, 72764, 953, 9693, 295594, 23286, NULL, NULL, 51134, 7064, N'S', 1, N'S', N'S', 8, 2, N'1', CAST(29.00 AS Decimal(10, 2)), 1, NULL, NULL, 1)
GO


SET IDENTITY_INSERT [dbo].[ciudad] ON 
GO
INSERT [dbo].[ciudad] ([idciudad], [idestado], [ciudad], [idusuario], [fum]) VALUES (61, 6, N'TORREÓN', 132, CAST(N'2018-01-31T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[ciudad] OFF
GO
SET IDENTITY_INSERT [dbo].[colonia] ON 
GO
INSERT [dbo].[colonia] ([idcolonia], [idestado], [idciudad], [colonia], [codigopostal], [idusuario], [fum]) VALUES (10167, 6, 61, N'SAN FELIPE', N'27085', 132, CAST(N'2018-01-31T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[colonia] ([idcolonia], [idestado], [idciudad], [colonia], [codigopostal], [idusuario], [fum]) VALUES (10168, 6, 61, N'FOVISSSTE NUEVA CALIFORNIA', N'27085', 132, CAST(N'2018-01-31T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[colonia] ([idcolonia], [idestado], [idciudad], [colonia], [codigopostal], [idusuario], [fum]) VALUES (10169, 6, 61, N'RESIDENCIAL LAS TORRES', N'27085', 132, CAST(N'2018-01-31T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[colonia] ([idcolonia], [idestado], [idciudad], [colonia], [codigopostal], [idusuario], [fum]) VALUES (10170, 6, 61, N'INFONAVIT NUEVA CALIFORNIA', N'27085', 132, CAST(N'2018-01-31T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[colonia] ([idcolonia], [idestado], [idciudad], [colonia], [codigopostal], [idusuario], [fum]) VALUES (10171, 6, 61, N'NUEVA VILLAS CALIFORNIA', N'27085', 132, CAST(N'2018-01-31T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[colonia] ([idcolonia], [idestado], [idciudad], [colonia], [codigopostal], [idusuario], [fum]) VALUES (10172, 6, 61, N'HORIZONTE', N'27085', 132, CAST(N'2018-01-31T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[colonia] OFF
GO
SET IDENTITY_INSERT [dbo].[estado] ON 
GO
INSERT [dbo].[estado] ([idestado], [estado], [idusuario], [fum]) VALUES (6, N'COAHUILA DE ZARAGOZA', 132, CAST(N'2018-01-31T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[estado] OFF
GO

INSERT [dbo].[plaza] ([idplaza], [plaza], [descrip], [visible]) VALUES (1, N'01', N'TORREÓN', N'1')
GO

SET IDENTITY_INSERT [dbo].[parametros] ON 
GO
INSERT [dbo].[parametros] ([idparametro], [sucursal], [clave], [valor], [idusuario], [fum]) VALUES (74, N'99', N'DLT____335', N'30', 131, CAST(N'2019-08-09T09:06:30.097' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[parametros] OFF
GO

--blindaje
SET IDENTITY_INSERT [dbo].[parametros] ON 
GO
INSERT [dbo].[parametros] ([idparametro], [sucursal], [clave], [valor], [idusuario], [fum]) VALUES (999, N'99', N'BLINIMP', N'10', null, null)
GO
SET IDENTITY_INSERT [dbo].[parametros] OFF
GO

SET IDENTITY_INSERT [dbo].[negocioexterno] ON 
GO
INSERT [dbo].[negocioexterno] ([idnegexterno], [negocio], [descripcion], [idusuario], [fum], [idusuariomodif], [fummodif]) VALUES (217, N'CC', N'CON CREDITO         ', 132, CAST(N'2018-01-24T10:37:30.000' AS DateTime), 0, CAST(N'1900-01-01T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[negocioexterno] OFF
GO

