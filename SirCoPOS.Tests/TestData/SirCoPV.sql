SET IDENTITY_INSERT [dbo].[formaspago] ON 
GO
INSERT [dbo].[formaspago] ([idformapago], [formapago], [descripcion], [autorizacion], [cambio], [deposito], [idusuario], [fum], [pos], [promocion]) VALUES (5, N'CP', N'C. PERSONA', 0, 0, 0, 132, CAST(N'2019-07-04T11:25:56.000' AS DateTime), 1, N'VA')
GO
INSERT [dbo].[formaspago] ([idformapago], [formapago], [descripcion], [autorizacion], [cambio], [deposito], [idusuario], [fum], [pos], [promocion]) VALUES (7, N'CV', N'CONTRAVALE', 0, 0, 0, 132, CAST(N'2019-07-04T11:25:56.000' AS DateTime), 1, N'VA')
GO
INSERT [dbo].[formaspago] ([idformapago], [formapago], [descripcion], [autorizacion], [cambio], [deposito], [idusuario], [fum], [pos], [promocion]) VALUES (11, N'DV', N'DEV./VENTA', 0, 1, 0, 132, CAST(N'2019-07-04T11:25:56.000' AS DateTime), 1, N'VA')
GO
INSERT [dbo].[formaspago] ([idformapago], [formapago], [descripcion], [autorizacion], [cambio], [deposito], [idusuario], [fum], [pos], [promocion]) VALUES (12, N'EF', N'EFECTIVO  ', 0, 1, 0, 132, CAST(N'2019-07-04T11:25:56.000' AS DateTime), 1, N'EF')
GO
INSERT [dbo].[formaspago] ([idformapago], [formapago], [descripcion], [autorizacion], [cambio], [deposito], [idusuario], [fum], [pos], [promocion]) VALUES (15, N'MD', N'DINERO ELE', 0, 0, 0, 132, CAST(N'2019-07-04T11:25:56.000' AS DateTime), 1, N'VA')
GO
INSERT [dbo].[formaspago] ([idformapago], [formapago], [descripcion], [autorizacion], [cambio], [deposito], [idusuario], [fum], [pos], [promocion]) VALUES (18, N'TC', N'T. CREDITO', 0, 0, 0, 132, CAST(N'2019-07-04T11:25:56.000' AS DateTime), 1, N'TC')
GO
INSERT [dbo].[formaspago] ([idformapago], [formapago], [descripcion], [autorizacion], [cambio], [deposito], [idusuario], [fum], [pos], [promocion]) VALUES (19, N'TD', N'T. DEBITO ', 0, 0, 0, 132, CAST(N'2019-07-04T11:25:56.000' AS DateTime), 1, N'TD')
GO
INSERT [dbo].[formaspago] ([idformapago], [formapago], [descripcion], [autorizacion], [cambio], [deposito], [idusuario], [fum], [pos], [promocion]) VALUES (21, N'VA', N'VALE      ', 0, 0, 0, 132, CAST(N'2019-07-04T11:25:56.000' AS DateTime), 1, N'VA')
GO
INSERT [dbo].[formaspago] ([idformapago], [formapago], [descripcion], [autorizacion], [cambio], [deposito], [idusuario], [fum], [pos], [promocion]) VALUES (97, N'VE', N'Vale Externo', NULL, NULL, NULL, NULL, NULL, 1, N'VA')
GO
INSERT [dbo].[formaspago] ([idformapago], [formapago], [descripcion], [autorizacion], [cambio], [deposito], [idusuario], [fum], [pos], [promocion]) VALUES (98, N'CD', N'Credito Distribuidor', NULL, NULL, NULL, NULL, NULL, 1, N'VA')
GO
INSERT [dbo].[formaspago] ([idformapago], [formapago], [descripcion], [autorizacion], [cambio], [deposito], [idusuario], [fum], [pos], [promocion]) VALUES (99, N'VD', N'Vale Digital', NULL, NULL, NULL, NULL, NULL, 1, N'VA')
GO
SET IDENTITY_INSERT [dbo].[formaspago] OFF
GO



INSERT [dbo].[venta] ([sucursal], [venta], [fecha], [estatus], [idcajero], [idvendedor], [idusuario], [fum], [idusuariocancela], [fumcancela]) VALUES (N'01', N'414628', CAST(N'2018-06-16' AS Date), N'AP', 0, 0, 0, CAST(N'2019-07-17T12:58:35.000' AS DateTime), 0, CAST(N'2019-07-17T12:58:35.000' AS DateTime))
GO
INSERT [dbo].[ventadet] ([sucursal], [venta], [renglon], [marca], [estilon], [corrida], [medida], [serie], [idpromocion], [idtipo], [ctd], [precio], [precdesc], [costomargen], [iva], [idusuario], [fum]) VALUES (N'01', N'414628', 1, N'YUY', N'    186', N'B', N'21 ', N'0000003445034', 0, 0, 1, CAST(499.00 AS Numeric(18, 2)), CAST(499.00 AS Numeric(18, 2)), CAST(231.00 AS Numeric(18, 2)), CAST(16.00 AS Numeric(6, 2)), 0, CAST(N'2019-07-17T13:00:15.250' AS DateTime))
INSERT [dbo].[ventadet] ([sucursal], [venta], [renglon], [marca], [estilon], [corrida], [medida], [serie], [idpromocion], [idtipo], [ctd], [precio], [precdesc], [costomargen], [iva], [idusuario], [fum]) VALUES (N'01', N'414628', 2, N'CHY', N'   2343', N'A', N'21 ', N'0000003506774', 0, 0, 1, CAST(569.00 AS Numeric(18, 2)), CAST(569.00 AS Numeric(18, 2)), CAST(258.10 AS Numeric(18, 2)), CAST(16.00 AS Numeric(6, 2)), 0, CAST(N'2019-07-17T13:00:15.250' AS DateTime))
GO

INSERT [dbo].[pago] ([sucursal], [pago], [fecha], [estatus], [idcajero], [idvendedor], [idusuario], [fum], [idusuariocancela], [fumcancela]) VALUES (N'01', N'414628', CAST(N'2018-06-16' AS Date), N'AP', 0, 0, 0, CAST(N'2019-07-17T11:26:26.053' AS DateTime), 0, CAST(N'2019-07-17T11:26:26.053' AS DateTime))
GO
INSERT [dbo].[pagodet] ([sucursal], [pago], [idformapago], [idvaledigital], [importe], [comision], [observaciones], [iva], [idusuario], [fum]) VALUES (N'01', N'414628', 21, 0, CAST(1068.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'3763       IFE           ', CAST(16.00 AS Numeric(6, 2)), 0, CAST(N'2019-07-17T11:26:28.940' AS DateTime))
GO


INSERT [dbo].[venta] ([sucursal], [venta], [fecha], [estatus], [idcajero], [idvendedor], [idusuario], [fum], [idusuariocancela], [fumcancela]) VALUES (N'01', N'424749', CAST(N'2018-12-17' AS Date), N'AP', 0, 0, 0, CAST(N'2019-07-17T12:58:35.000' AS DateTime), 0, CAST(N'2019-07-17T12:58:35.000' AS DateTime))
GO
INSERT [dbo].[ventadet] ([sucursal], [venta], [renglon], [marca], [estilon], [corrida], [medida], [serie], [idpromocion], [idtipo], [ctd], [precio], [precdesc], [costomargen], [iva], [idusuario], [fum]) VALUES (N'01', N'424749', 1, N'YUY', N'    161', N'A', N'12 ', N'0000003287958', 0, 0, 1, CAST(369.00 AS Numeric(18, 2)), CAST(232.80 AS Numeric(18, 2)), CAST(169.00 AS Numeric(18, 2)), CAST(16.00 AS Numeric(6, 2)), 0, CAST(N'2019-07-17T13:00:15.250' AS DateTime))
INSERT [dbo].[ventadet] ([sucursal], [venta], [renglon], [marca], [estilon], [corrida], [medida], [serie], [idpromocion], [idtipo], [ctd], [precio], [precdesc], [costomargen], [iva], [idusuario], [fum]) VALUES (N'01', N'424749', 2, N'ADZ', N'     52', N'A', N'15 ', N'0000003487207', 0, 0, 1, CAST(289.00 AS Numeric(18, 2)), CAST(289.00 AS Numeric(18, 2)), CAST(125.00 AS Numeric(18, 2)), CAST(16.00 AS Numeric(6, 2)), 0, CAST(N'2019-07-17T13:00:15.250' AS DateTime))
GO

INSERT [dbo].[venta] ([sucursal], [venta], [fecha], [estatus], [idcajero], [idvendedor], [idusuario], [fum], [idusuariocancela], [fumcancela]) VALUES (N'01', N'422168', CAST(N'2018-11-03' AS Date), N'AP', 0, 0, 0, CAST(N'2019-07-17T12:58:35.000' AS DateTime), 0, CAST(N'2019-07-17T12:58:35.000' AS DateTime))
GO
INSERT [dbo].[ventadet] ([sucursal], [venta], [renglon], [marca], [estilon], [corrida], [medida], [serie], [idpromocion], [idtipo], [ctd], [precio], [precdesc], [costomargen], [iva], [idusuario], [fum]) VALUES (N'01', N'422168', 1, N'OZO', N'   2458', N'A', N'28 ', N'0000003517177', 0, 0, 1, CAST(539.00 AS Numeric(18, 2)), CAST(539.00 AS Numeric(18, 2)), CAST(275.00 AS Numeric(18, 2)), CAST(16.00 AS Numeric(6, 2)), 0, CAST(N'2019-07-17T13:00:15.250' AS DateTime))
GO
INSERT [dbo].[ventadet] ([sucursal], [venta], [renglon], [marca], [estilon], [corrida], [medida], [serie], [idpromocion], [idtipo], [ctd], [precio], [precdesc], [costomargen], [iva], [idusuario], [fum]) VALUES (N'01', N'422168', 2, N'GOS', N'    626', N'A', N'29 ', N'0000003518445', 0, 0, 1, CAST(509.00 AS Numeric(18, 2)), CAST(509.00 AS Numeric(18, 2)), CAST(255.00 AS Numeric(18, 2)), CAST(16.00 AS Numeric(6, 2)), 0, CAST(N'2019-07-17T13:00:15.250' AS DateTime))
GO
INSERT [dbo].[ventadet] ([sucursal], [venta], [renglon], [marca], [estilon], [corrida], [medida], [serie], [idpromocion], [idtipo], [ctd], [precio], [precdesc], [costomargen], [iva], [idusuario], [fum]) VALUES (N'01', N'422168', 3, N'HKY', N'     31', N'A', N'29 ', N'0000003540668', 0, 0, 1, CAST(479.00 AS Numeric(18, 2)), CAST(479.00 AS Numeric(18, 2)), CAST(178.00 AS Numeric(18, 2)), CAST(16.00 AS Numeric(6, 2)), 0, CAST(N'2019-07-17T13:00:15.250' AS DateTime))
GO
INSERT [dbo].[ventadet] ([sucursal], [venta], [renglon], [marca], [estilon], [corrida], [medida], [serie], [idpromocion], [idtipo], [ctd], [precio], [precdesc], [costomargen], [iva], [idusuario], [fum]) VALUES (N'01', N'422168', 4, N'MLA', N'    208', N'A', N'26-', N'0000003562356', 0, 0, 1, CAST(519.00 AS Numeric(18, 2)), CAST(519.00 AS Numeric(18, 2)), CAST(255.00 AS Numeric(18, 2)), CAST(16.00 AS Numeric(6, 2)), 0, CAST(N'2019-07-17T13:00:15.250' AS DateTime))
GO
INSERT [dbo].[ventadet] ([sucursal], [venta], [renglon], [marca], [estilon], [corrida], [medida], [serie], [idpromocion], [idtipo], [ctd], [precio], [precdesc], [costomargen], [iva], [idusuario], [fum]) VALUES (N'01', N'422168', 5, N'BNT', N'     22', N'F', N'01 ', N'0000003567974', 0, 0, 1, CAST(69.00 AS Numeric(18, 2)), CAST(69.00 AS Numeric(18, 2)), CAST(23.75 AS Numeric(18, 2)), CAST(16.00 AS Numeric(6, 2)), 0, CAST(N'2019-07-17T13:00:15.250' AS DateTime))
GO
INSERT [dbo].[ventadet] ([sucursal], [venta], [renglon], [marca], [estilon], [corrida], [medida], [serie], [idpromocion], [idtipo], [ctd], [precio], [precdesc], [costomargen], [iva], [idusuario], [fum]) VALUES (N'01', N'422168', 6, N'BNT', N'     29', N'F', N'01 ', N'0000003568047', 0, 0, 1, CAST(89.00 AS Numeric(18, 2)), CAST(89.00 AS Numeric(18, 2)), CAST(36.50 AS Numeric(18, 2)), CAST(16.00 AS Numeric(6, 2)), 0, CAST(N'2019-07-17T13:00:15.250' AS DateTime))
GO
INSERT [dbo].[ventadet] ([sucursal], [venta], [renglon], [marca], [estilon], [corrida], [medida], [serie], [idpromocion], [idtipo], [ctd], [precio], [precdesc], [costomargen], [iva], [idusuario], [fum]) VALUES (N'01', N'422168', 7, N'VAN', N'    417', N'A', N'22-', N'0000003581619', 0, 0, 1, CAST(799.00 AS Numeric(18, 2)), CAST(799.00 AS Numeric(18, 2)), CAST(385.73 AS Numeric(18, 2)), CAST(16.00 AS Numeric(6, 2)), 0, CAST(N'2019-07-17T13:00:15.250' AS DateTime))
GO
INSERT [dbo].[ventadet] ([sucursal], [venta], [renglon], [marca], [estilon], [corrida], [medida], [serie], [idpromocion], [idtipo], [ctd], [precio], [precdesc], [costomargen], [iva], [idusuario], [fum]) VALUES (N'01', N'422168', 8, N'CHY', N'   2442', N'A', N'16-', N'0000003583185', 0, 0, 1, CAST(569.00 AS Numeric(18, 2)), CAST(569.00 AS Numeric(18, 2)), CAST(258.10 AS Numeric(18, 2)), CAST(16.00 AS Numeric(6, 2)), 0, CAST(N'2019-07-17T13:00:15.250' AS DateTime))
GO
INSERT [dbo].[ventadet] ([sucursal], [venta], [renglon], [marca], [estilon], [corrida], [medida], [serie], [idpromocion], [idtipo], [ctd], [precio], [precdesc], [costomargen], [iva], [idusuario], [fum]) VALUES (N'01', N'422168', 9, N'CHY', N'   2517', N'A', N'22 ', N'0000003584766', 0, 0, 1, CAST(739.00 AS Numeric(18, 2)), CAST(739.00 AS Numeric(18, 2)), CAST(335.69 AS Numeric(18, 2)), CAST(16.00 AS Numeric(6, 2)), 0, CAST(N'2019-07-17T13:00:15.250' AS DateTime))
GO
INSERT [dbo].[ventadet] ([sucursal], [venta], [renglon], [marca], [estilon], [corrida], [medida], [serie], [idpromocion], [idtipo], [ctd], [precio], [precdesc], [costomargen], [iva], [idusuario], [fum]) VALUES (N'01', N'422168', 10, N'CHY', N'   2434', N'A', N'16 ', N'0000003584922', 0, 0, 1, CAST(599.00 AS Numeric(18, 2)), CAST(599.00 AS Numeric(18, 2)), CAST(273.62 AS Numeric(18, 2)), CAST(16.00 AS Numeric(6, 2)), 0, CAST(N'2019-07-17T13:00:15.250' AS DateTime))
GO


--promocion adidas
INSERT [dbo].[agrupaciones] ([idagrupacion], [nombre], [fecha], [idusuario], [fum]) VALUES (111, N'PROMOCION ADIDAS', CAST(N'2019-06-18' AS Date), N'RESM1979', CAST(N'2019-06-18T11:37:16.380' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (111, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ADD', N'', N'Marca', 1, N'RESM1979', CAST(N'2019-06-18T11:37:28.830' AS DateTime))
GO
INSERT [dbo].[cupones] ([idcupon], [nombre], [descripcion], [restricciones], [fecha], [estatus], [tipo], [imagen], [fecini], [fecfin], [idusuariocaptura], [fumcaptura], [idusuarioactiva], [fumactiva], [idusuariocancela], [fumcancela], [idusuario], [fum]) VALUES (2, N'CUPONES ADIDAS', N'CUPON DE DESCUENTO EN LA MARCA ADIDAS', N'SOLO ENTRA MARCA ADIDAS', CAST(N'2019-06-18' AS Date), N'ACTIVO', N'MULTIPLE', NULL, CAST(N'2019-06-20' AS Date), CAST(N'2019-07-31' AS Date), N'RESM1979', CAST(N'2019-06-18T11:39:01.273' AS DateTime), N'RESM1979', CAST(N'2019-06-18T11:46:50.070' AS DateTime), NULL, NULL, N'RESM1979', CAST(N'2019-06-18T11:46:50.070' AS DateTime))
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum]) VALUES (2, N'0000000101', N'ACTIVO', N'RESM1979', CAST(N'2019-06-18T11:39:40.973' AS DateTime))
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum]) VALUES (2, N'0000000102', N'ACTIVO', N'RESM1979', CAST(N'2019-06-18T11:39:40.977' AS DateTime))
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum]) VALUES (2, N'0000000103', N'ACTIVO', N'RESM1979', CAST(N'2019-06-18T11:39:40.980' AS DateTime))
GO
INSERT [dbo].[promociones] ([idpromocion], [nombre], [fecha], [tipo], [estatus], [iniciopromo], [finpromo], [preciero], [senalizador], [clasificacion], [imagen], [minunicompra], [minimpcompra], [unipromo], [acumulable], [paresunicos], [clinoregis], [idusuariocaptura], [fumcaptura], [idusuarioaplica], [fumaplica], [idusuariopausa], [fumpausa], [idusuariocancela], [fumcancela], [idusuario], [fum]) VALUES (10, N'TENIS ADIDAS 15%', CAST(N'2019-06-18' AS Date), N'DIRECTA', N'ACTIVO', CAST(N'2019-06-20T00:00:00.000' AS DateTime), CAST(N'2019-07-31T00:00:00.000' AS DateTime), N'AZUL', N'BLANCO', N'REBAJA', NULL, 1, CAST(0.00 AS Numeric(18, 2)), 0, N'NO', N'NO', N'SI', N'RESM1979', CAST(N'2019-06-18T11:37:00.703' AS DateTime), N'RESM1979', CAST(N'2019-06-18T11:50:48.280' AS DateTime), NULL, NULL, NULL, NULL, N'RESM1979', CAST(N'2019-06-18T11:50:48.280' AS DateTime))
GO
INSERT [dbo].[promocionesagrupaciones] ([idpromocion], [idagrupacioncompra], [idagrupacionpromo], [renglon], [idusuario], [fum]) VALUES (10, 0, 111, 1, N'RESM1979', CAST(N'2019-06-18T11:37:55.013' AS DateTime))
GO
INSERT [dbo].[promocionescupones] ([idpromocion], [idcupon], [idusuario], [fum]) VALUES (10, 2, N'RESM1979', CAST(N'2019-06-18T11:47:10.283' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (10, N'TO', 0, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(15.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), 10, N'RESM1979', CAST(N'2019-06-18T11:44:24.153' AS DateTime))
GO
INSERT [dbo].[promocionesplazas] ([idpromocion], [plaza], [sucursal], [idusuario], [fum]) VALUES (10, N'01', N'', N'RESM1979', CAST(N'2019-06-18T11:49:38.350' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (10, N'TC', 0, N'PROMO', CAST(0.00 AS Numeric(18, 2)), 5, 10, 0, N'RESM1979', CAST(N'2019-06-18T11:44:24.153' AS DateTime))
GO

--promocion 3x2
INSERT [dbo].[agrupaciones] ([idagrupacion], [nombre], [fecha], [idusuario], [fum]) VALUES (12, N'PROMOCION  3 X2', CAST(N'2019-07-08' AS Date), N'RESM1979', CAST(N'2019-07-08T09:28:35.993' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (12, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'PAU', N'', N'Marca', 2, N'RESM1979', CAST(N'2019-07-08T09:29:18.843' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (12, 1, 1, 2, 547, 1122, 0, 0, 0, 0, 0, N'', N'', N'L1', 1, N'RESM1979', CAST(N'2019-07-08T09:29:03.343' AS DateTime))
GO
INSERT [dbo].[cupones] ([idcupon], [nombre], [descripcion], [restricciones], [fecha], [estatus], [tipo], [imagen], [fecini], [fecfin], [idusuariocaptura], [fumcaptura], [idusuarioactiva], [fumactiva], [idusuariocancela], [fumcancela], [idusuario], [fum]) VALUES (3, N'CUPON DE 3X2', N'CUPON VALIDO EN PROMOCION 3X2', N'NO ENTRA MARCA OZONO,  FLEXI,  CONVERSE', CAST(N'2019-07-08' AS Date), N'ACTIVO', N'MULTIPLE', null, CAST(N'2019-07-08' AS Date), CAST(N'2019-08-31' AS Date), N'RESM1979', CAST(N'2019-07-08T09:31:24.677' AS DateTime), N'RESM1979', CAST(N'2019-07-08T10:38:28.027' AS DateTime), NULL, NULL, N'RESM1979', CAST(N'2019-07-08T10:38:28.027' AS DateTime))
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia]) VALUES (3, N'0000000181', N'ACTIVO', N'RESM1979', CAST(N'2019-07-08T09:33:40.380' AS DateTime), NULL)
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia]) VALUES (3, N'0000000182', N'ACTIVO', N'RESM1979', CAST(N'2019-07-08T09:33:40.413' AS DateTime), NULL)
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia]) VALUES (3, N'0000000183', N'ACTIVO', N'RESM1979', CAST(N'2019-07-08T09:33:40.457' AS DateTime), NULL)
GO
INSERT [dbo].[promociones] ([idpromocion], [nombre], [fecha], [tipo], [estatus], [iniciopromo], [finpromo], [preciero], [senalizador], [clasificacion], [imagen], [minunicompra], [minimpcompra], [unipromo], [acumulable], [paresunicos], [clinoregis], [idusuariocaptura], [fumcaptura], [idusuarioaplica], [fumaplica], [idusuariopausa], [fumpausa], [idusuariocancela], [fumcancela], [idusuario], [fum]) VALUES (11, N'PROMOCION 3X2', CAST(N'2019-07-08' AS Date), N'AxB', N'ACTIVO', CAST(N'2019-07-08T00:00:00.000' AS DateTime), CAST(N'2019-08-31T00:00:00.000' AS DateTime), N'AZUL', N'BLANCO', N'REBAJA', null, 2, CAST(0.00 AS Numeric(18, 2)), 1, N'SI', N'SI', N'SI', N'RESM1979', CAST(N'2019-07-08T09:35:55.153' AS DateTime), N'RESM1979', CAST(N'2019-07-08T10:40:03.700' AS DateTime), N'RESM1979', CAST(N'2019-07-08T10:39:49.870' AS DateTime), NULL, NULL, N'RESM1979', CAST(N'2019-07-08T10:40:03.700' AS DateTime))
GO
INSERT [dbo].[promocionesagrupaciones] ([idpromocion], [idagrupacioncompra], [idagrupacionpromo], [renglon], [idusuario], [fum]) VALUES (11, 12, 12, 1, N'RESM1979', CAST(N'2019-07-08T09:36:12.530' AS DateTime))
GO
INSERT [dbo].[promocionescupones] ([idpromocion], [idcupon], [idusuario], [fum]) VALUES (11, 3, N'RESM1979', CAST(N'2019-07-08T10:38:46.060' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (11, N'TO', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-07-08T09:35:55.220' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (11, N'TO', 2, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-07-08T09:35:55.220' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (11, N'TO', 3, N'PROMO', CAST(0.00 AS Numeric(18, 2)), 100, CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-07-08T09:35:55.223' AS DateTime))
GO
INSERT [dbo].[promocionesexclusiones] ([idpromocion], [marca], [estilon], [idusuario], [fum]) VALUES (11, N'CVR', N'', N'RESM1979', CAST(N'2019-07-08T09:37:13.657' AS DateTime))
GO
INSERT [dbo].[promocionesexclusiones] ([idpromocion], [marca], [estilon], [idusuario], [fum]) VALUES (11, N'FFF', N'', N'RESM1979', CAST(N'2019-07-08T09:36:46.563' AS DateTime))
GO
INSERT [dbo].[promocionesexclusiones] ([idpromocion], [marca], [estilon], [idusuario], [fum]) VALUES (11, N'OZO', N'', N'RESM1979', CAST(N'2019-07-08T09:36:39.287' AS DateTime))
GO
INSERT [dbo].[promocionesplazas] ([idpromocion], [plaza], [sucursal], [idusuario], [fum]) VALUES (11, N'01', N'', N'RESM1979', CAST(N'2019-07-08T09:37:21.050' AS DateTime))
GO


--promocion 3x249
INSERT [dbo].[agrupaciones] ([idagrupacion], [nombre], [fecha], [idusuario], [fum]) VALUES (1, N'PROMOCION 3X249 EFECTIVO', CAST(N'2019-10-17' AS Date), N'RESM1979', CAST(N'2019-10-17T10:15:24.800' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ADZ', N'     20', N'Modelo', 205, N'RESM1979', CAST(N'2019-10-17T11:33:14.393' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ADZ', N'     38', N'Modelo', 309, N'RESM1979', CAST(N'2019-10-17T11:33:14.580' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ADZ', N'     41', N'Modelo', 270, N'RESM1979', CAST(N'2019-10-17T11:33:14.513' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ADZ', N'     52', N'Modelo', 323, N'RESM1979', CAST(N'2019-10-17T11:33:14.603' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ADZ', N'     53', N'Modelo', 285, N'RESM1979', CAST(N'2019-10-17T11:33:14.537' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ADZ', N'     57', N'Modelo', 286, N'RESM1979', CAST(N'2019-10-17T11:33:14.540' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ADZ', N'     58', N'Modelo', 287, N'RESM1979', CAST(N'2019-10-17T11:33:14.540' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ARA', N'    154', N'Modelo', 106, N'RESM1979', CAST(N'2019-10-17T11:33:14.153' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ARA', N'    156', N'Modelo', 107, N'RESM1979', CAST(N'2019-10-17T11:33:14.157' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ARA', N'    159', N'Modelo', 45, N'RESM1979', CAST(N'2019-10-17T11:33:14.073' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ARA', N'    160', N'Modelo', 108, N'RESM1979', CAST(N'2019-10-17T11:33:14.157' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ARA', N'    161', N'Modelo', 109, N'RESM1979', CAST(N'2019-10-17T11:33:14.160' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ARA', N'    162', N'Modelo', 284, N'RESM1979', CAST(N'2019-10-17T11:33:14.537' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ARA', N'    163', N'Modelo', 46, N'RESM1979', CAST(N'2019-10-17T11:33:14.073' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ARA', N'    164', N'Modelo', 47, N'RESM1979', CAST(N'2019-10-17T11:33:14.073' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ARA', N'    165', N'Modelo', 48, N'RESM1979', CAST(N'2019-10-17T11:33:14.077' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'BLA', N'     31', N'Modelo', 206, N'RESM1979', CAST(N'2019-10-17T11:33:14.397' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'BLA', N'     33', N'Modelo', 207, N'RESM1979', CAST(N'2019-10-17T11:33:14.400' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'BVE', N'     46', N'Modelo', 179, N'RESM1979', CAST(N'2019-10-17T11:33:14.333' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'BVE', N'     48', N'Modelo', 38, N'RESM1979', CAST(N'2019-10-17T11:33:14.063' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'BVE', N'     49', N'Modelo', 180, N'RESM1979', CAST(N'2019-10-17T11:33:14.337' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'BVE', N'     50', N'Modelo', 181, N'RESM1979', CAST(N'2019-10-17T11:33:14.340' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'BVE', N'     51', N'Modelo', 182, N'RESM1979', CAST(N'2019-10-17T11:33:14.340' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'BVE', N'     52', N'Modelo', 183, N'RESM1979', CAST(N'2019-10-17T11:33:14.340' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'BVE', N'     62', N'Modelo', 39, N'RESM1979', CAST(N'2019-10-17T11:33:14.067' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'BVE', N'     63', N'Modelo', 40, N'RESM1979', CAST(N'2019-10-17T11:33:14.067' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'CAR', N'    284', N'Modelo', 307, N'RESM1979', CAST(N'2019-10-17T11:33:14.577' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'CFT', N'      7', N'Modelo', 58, N'RESM1979', CAST(N'2019-10-17T11:33:14.090' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'CFT', N'      8', N'Modelo', 271, N'RESM1979', CAST(N'2019-10-17T11:33:14.517' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'CFT', N'      9', N'Modelo', 272, N'RESM1979', CAST(N'2019-10-17T11:33:14.517' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'CFT', N'     10', N'Modelo', 59, N'RESM1979', CAST(N'2019-10-17T11:33:14.090' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'CFT', N'     19', N'Modelo', 225, N'RESM1979', CAST(N'2019-10-17T11:33:14.440' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'CFT', N'     20', N'Modelo', 76, N'RESM1979', CAST(N'2019-10-17T11:33:14.110' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'CFT', N'     21', N'Modelo', 77, N'RESM1979', CAST(N'2019-10-17T11:33:14.113' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'CFT', N'     22', N'Modelo', 301, N'RESM1979', CAST(N'2019-10-17T11:33:14.563' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'CTA', N'    619', N'Modelo', 184, N'RESM1979', CAST(N'2019-10-17T11:33:14.343' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ENS', N'      7', N'Modelo', 122, N'RESM1979', CAST(N'2019-10-17T11:33:14.190' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'EXL', N'     16', N'Modelo', 155, N'RESM1979', CAST(N'2019-10-17T11:33:14.270' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'EXU', N'     19', N'Modelo', 139, N'RESM1979', CAST(N'2019-10-17T11:33:14.233' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'EXU', N'     67', N'Modelo', 296, N'RESM1979', CAST(N'2019-10-17T11:33:14.557' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'EXU', N'     68', N'Modelo', 297, N'RESM1979', CAST(N'2019-10-17T11:33:14.560' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'GOS', N'    561', N'Modelo', 226, N'RESM1979', CAST(N'2019-10-17T11:33:14.440' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'GOS', N'    608', N'Modelo', 208, N'RESM1979', CAST(N'2019-10-17T11:33:14.400' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'GOS', N'    633', N'Modelo', 18, N'RESM1979', CAST(N'2019-10-17T11:33:14.040' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'GOS', N'    634', N'Modelo', 19, N'RESM1979', CAST(N'2019-10-17T11:33:14.043' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'GOS', N'    635', N'Modelo', 20, N'RESM1979', CAST(N'2019-10-17T11:33:14.043' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'GOS', N'    695', N'Modelo', 209, N'RESM1979', CAST(N'2019-10-17T11:33:14.400' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'HKY', N'     22', N'Modelo', 294, N'RESM1979', CAST(N'2019-10-17T11:33:14.553' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'HKY', N'     24', N'Modelo', 295, N'RESM1979', CAST(N'2019-10-17T11:33:14.553' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'HKY', N'     29', N'Modelo', 2, N'RESM1979', CAST(N'2019-10-17T11:33:14.023' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'HKY', N'     32', N'Modelo', 3, N'RESM1979', CAST(N'2019-10-17T11:33:14.023' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'INS', N'     51', N'Modelo', 140, N'RESM1979', CAST(N'2019-10-17T11:33:14.233' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'INS', N'     86', N'Modelo', 21, N'RESM1979', CAST(N'2019-10-17T11:33:14.043' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'INS', N'     91', N'Modelo', 141, N'RESM1979', CAST(N'2019-10-17T11:33:14.240' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'INS', N'    111', N'Modelo', 262, N'RESM1979', CAST(N'2019-10-17T11:33:14.500' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ISH', N'     26', N'Modelo', 132, N'RESM1979', CAST(N'2019-10-17T11:33:14.217' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'JAK', N'     17', N'Modelo', 185, N'RESM1979', CAST(N'2019-10-17T11:33:14.343' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'JAK', N'     18', N'Modelo', 186, N'RESM1979', CAST(N'2019-10-17T11:33:14.347' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'JAK', N'     39', N'Modelo', 227, N'RESM1979', CAST(N'2019-10-17T11:33:14.443' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'JAK', N'     45', N'Modelo', 60, N'RESM1979', CAST(N'2019-10-17T11:33:14.090' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'JAK', N'     46', N'Modelo', 210, N'RESM1979', CAST(N'2019-10-17T11:33:14.403' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'KLI', N'      1', N'Modelo', 9, N'RESM1979', CAST(N'2019-10-17T11:33:14.030' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'KOA', N'      2', N'Modelo', 78, N'RESM1979', CAST(N'2019-10-17T11:33:14.113' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'KOA', N'      4', N'Modelo', 79, N'RESM1979', CAST(N'2019-10-17T11:33:14.113' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'LDL', N'      7', N'Modelo', 228, N'RESM1979', CAST(N'2019-10-17T11:33:14.443' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'LDL', N'     17', N'Modelo', 80, N'RESM1979', CAST(N'2019-10-17T11:33:14.117' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'LDL', N'     18', N'Modelo', 314, N'RESM1979', CAST(N'2019-10-17T11:33:14.587' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'LDL', N'     19', N'Modelo', 81, N'RESM1979', CAST(N'2019-10-17T11:33:14.117' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'LOV', N'      6', N'Modelo', 142, N'RESM1979', CAST(N'2019-10-17T11:33:14.240' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MAE', N'      4', N'Modelo', 130, N'RESM1979', CAST(N'2019-10-17T11:33:14.213' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MAE', N'      7', N'Modelo', 1, N'RESM1979', CAST(N'2019-10-17T11:33:14.020' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MCQ', N'      1', N'Modelo', 111, N'RESM1979', CAST(N'2019-10-17T11:33:14.163' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MCQ', N'      2', N'Modelo', 242, N'RESM1979', CAST(N'2019-10-17T11:33:14.467' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MCQ', N'      4', N'Modelo', 170, N'RESM1979', CAST(N'2019-10-17T11:33:14.310' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MCQ', N'      5', N'Modelo', 171, N'RESM1979', CAST(N'2019-10-17T11:33:14.313' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MCQ', N'     11', N'Modelo', 264, N'RESM1979', CAST(N'2019-10-17T11:33:14.503' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MCQ', N'     12', N'Modelo', 280, N'RESM1979', CAST(N'2019-10-17T11:33:14.530' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MCQ', N'     13', N'Modelo', 102, N'RESM1979', CAST(N'2019-10-17T11:33:14.143' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MCQ', N'     14', N'Modelo', 243, N'RESM1979', CAST(N'2019-10-17T11:33:14.470' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MCQ', N'     15', N'Modelo', 103, N'RESM1979', CAST(N'2019-10-17T11:33:14.147' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MCQ', N'     16', N'Modelo', 244, N'RESM1979', CAST(N'2019-10-17T11:33:14.470' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MLA', N'     21', N'Modelo', 10, N'RESM1979', CAST(N'2019-10-17T11:33:14.030' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MLA', N'     78', N'Modelo', 133, N'RESM1979', CAST(N'2019-10-17T11:33:14.220' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MLA', N'    186', N'Modelo', 134, N'RESM1979', CAST(N'2019-10-17T11:33:14.220' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MLA', N'    189', N'Modelo', 135, N'RESM1979', CAST(N'2019-10-17T11:33:14.220' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MLO', N'      1', N'Modelo', 143, N'RESM1979', CAST(N'2019-10-17T11:33:14.240' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MLO', N'      5', N'Modelo', 144, N'RESM1979', CAST(N'2019-10-17T11:33:14.243' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MLO', N'      7', N'Modelo', 302, N'RESM1979', CAST(N'2019-10-17T11:33:14.567' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MLO', N'      8', N'Modelo', 22, N'RESM1979', CAST(N'2019-10-17T11:33:14.047' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MLO', N'      9', N'Modelo', 23, N'RESM1979', CAST(N'2019-10-17T11:33:14.047' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MLO', N'     10', N'Modelo', 24, N'RESM1979', CAST(N'2019-10-17T11:33:14.047' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MLO', N'     11', N'Modelo', 25, N'RESM1979', CAST(N'2019-10-17T11:33:14.050' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MLO', N'     12', N'Modelo', 145, N'RESM1979', CAST(N'2019-10-17T11:33:14.247' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNB', N'    136', N'Modelo', 123, N'RESM1979', CAST(N'2019-10-17T11:33:14.193' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNP', N'    155', N'Modelo', 61, N'RESM1979', CAST(N'2019-10-17T11:33:14.090' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNP', N'    159', N'Modelo', 196, N'RESM1979', CAST(N'2019-10-17T11:33:14.377' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNP', N'    165', N'Modelo', 324, N'RESM1979', CAST(N'2019-10-17T11:33:14.603' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNP', N'    170', N'Modelo', 211, N'RESM1979', CAST(N'2019-10-17T11:33:14.410' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNP', N'    171', N'Modelo', 212, N'RESM1979', CAST(N'2019-10-17T11:33:14.413' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNP', N'    172', N'Modelo', 213, N'RESM1979', CAST(N'2019-10-17T11:33:14.413' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNP', N'    175', N'Modelo', 315, N'RESM1979', CAST(N'2019-10-17T11:33:14.590' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNP', N'    178', N'Modelo', 32, N'RESM1979', CAST(N'2019-10-17T11:33:14.057' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNP', N'    179', N'Modelo', 172, N'RESM1979', CAST(N'2019-10-17T11:33:14.317' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNP', N'    180', N'Modelo', 33, N'RESM1979', CAST(N'2019-10-17T11:33:14.060' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNP', N'    181', N'Modelo', 34, N'RESM1979', CAST(N'2019-10-17T11:33:14.060' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNP', N'    182', N'Modelo', 35, N'RESM1979', CAST(N'2019-10-17T11:33:14.060' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNP', N'    183', N'Modelo', 36, N'RESM1979', CAST(N'2019-10-17T11:33:14.060' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNP', N'    184', N'Modelo', 37, N'RESM1979', CAST(N'2019-10-17T11:33:14.063' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNP', N'    185', N'Modelo', 29, N'RESM1979', CAST(N'2019-10-17T11:33:14.053' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MON', N'      8', N'Modelo', 310, N'RESM1979', CAST(N'2019-10-17T11:33:14.580' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MON', N'      9', N'Modelo', 214, N'RESM1979', CAST(N'2019-10-17T11:33:14.417' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MON', N'     26', N'Modelo', 137, N'RESM1979', CAST(N'2019-10-17T11:33:14.223' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MON', N'     27', N'Modelo', 62, N'RESM1979', CAST(N'2019-10-17T11:33:14.093' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MON', N'     31', N'Modelo', 259, N'RESM1979', CAST(N'2019-10-17T11:33:14.493' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1082', N'Modelo', 188, N'RESM1979', CAST(N'2019-10-17T11:33:14.357' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1084', N'Modelo', 251, N'RESM1979', CAST(N'2019-10-17T11:33:14.480' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1085', N'Modelo', 252, N'RESM1979', CAST(N'2019-10-17T11:33:14.483' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1103', N'Modelo', 253, N'RESM1979', CAST(N'2019-10-17T11:33:14.483' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1127', N'Modelo', 63, N'RESM1979', CAST(N'2019-10-17T11:33:14.093' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1129', N'Modelo', 215, N'RESM1979', CAST(N'2019-10-17T11:33:14.420' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1131', N'Modelo', 197, N'RESM1979', CAST(N'2019-10-17T11:33:14.380' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1136', N'Modelo', 64, N'RESM1979', CAST(N'2019-10-17T11:33:14.097' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1137', N'Modelo', 116, N'RESM1979', CAST(N'2019-10-17T11:33:14.183' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1141', N'Modelo', 110, N'RESM1979', CAST(N'2019-10-17T11:33:14.160' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1149', N'Modelo', 325, N'RESM1979', CAST(N'2019-10-17T11:33:14.607' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1150', N'Modelo', 326, N'RESM1979', CAST(N'2019-10-17T11:33:14.610' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1153', N'Modelo', 327, N'RESM1979', CAST(N'2019-10-17T11:33:14.610' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1154', N'Modelo', 288, N'RESM1979', CAST(N'2019-10-17T11:33:14.543' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1158', N'Modelo', 65, N'RESM1979', CAST(N'2019-10-17T11:33:14.097' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1159', N'Modelo', 311, N'RESM1979', CAST(N'2019-10-17T11:33:14.583' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NLK', N'      6', N'Modelo', 303, N'RESM1979', CAST(N'2019-10-17T11:33:14.570' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NLK', N'     20', N'Modelo', 156, N'RESM1979', CAST(N'2019-10-17T11:33:14.273' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'OZO', N'   2323', N'Modelo', 146, N'RESM1979', CAST(N'2019-10-17T11:33:14.247' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'OZO', N'   2349', N'Modelo', 124, N'RESM1979', CAST(N'2019-10-17T11:33:14.193' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'OZO', N'   2354', N'Modelo', 125, N'RESM1979', CAST(N'2019-10-17T11:33:14.197' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'OZO', N'   2373', N'Modelo', 190, N'RESM1979', CAST(N'2019-10-17T11:33:14.360' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'OZO', N'   2383', N'Modelo', 147, N'RESM1979', CAST(N'2019-10-17T11:33:14.250' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'OZO', N'   2426', N'Modelo', 298, N'RESM1979', CAST(N'2019-10-17T11:33:14.560' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'OZO', N'   2428', N'Modelo', 7, N'RESM1979', CAST(N'2019-10-17T11:33:14.030' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'OZO', N'   2429', N'Modelo', 258, N'RESM1979', CAST(N'2019-10-17T11:33:14.493' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'OZO', N'   2510', N'Modelo', 261, N'RESM1979', CAST(N'2019-10-17T11:33:14.497' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'OZO', N'   2514', N'Modelo', 260, N'RESM1979', CAST(N'2019-10-17T11:33:14.497' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'OZO', N'   2546', N'Modelo', 26, N'RESM1979', CAST(N'2019-10-17T11:33:14.050' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'OZO', N'   2547', N'Modelo', 27, N'RESM1979', CAST(N'2019-10-17T11:33:14.050' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'PAT', N'     86', N'Modelo', 229, N'RESM1979', CAST(N'2019-10-17T11:33:14.447' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'PAU', N'    609', N'Modelo', 157, N'RESM1979', CAST(N'2019-10-17T11:33:14.277' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'PAU', N'    610', N'Modelo', 158, N'RESM1979', CAST(N'2019-10-17T11:33:14.280' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'RIL', N'     23', N'Modelo', 266, N'RESM1979', CAST(N'2019-10-17T11:33:14.507' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'RIL', N'     31', N'Modelo', 55, N'RESM1979', CAST(N'2019-10-17T11:33:14.083' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'RIL', N'     33', N'Modelo', 267, N'RESM1979', CAST(N'2019-10-17T11:33:14.507' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'RIL', N'     34', N'Modelo', 198, N'RESM1979', CAST(N'2019-10-17T11:33:14.380' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'RIL', N'     35', N'Modelo', 56, N'RESM1979', CAST(N'2019-10-17T11:33:14.087' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'RIL', N'     36', N'Modelo', 199, N'RESM1979', CAST(N'2019-10-17T11:33:14.380' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'RIL', N'     37', N'Modelo', 57, N'RESM1979', CAST(N'2019-10-17T11:33:14.087' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SKO', N'     14', N'Modelo', 112, N'RESM1979', CAST(N'2019-10-17T11:33:14.167' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SKO', N'     15', N'Modelo', 113, N'RESM1979', CAST(N'2019-10-17T11:33:14.170' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SKO', N'     16', N'Modelo', 49, N'RESM1979', CAST(N'2019-10-17T11:33:14.077' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SKO', N'     17', N'Modelo', 191, N'RESM1979', CAST(N'2019-10-17T11:33:14.363' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SKO', N'     18', N'Modelo', 50, N'RESM1979', CAST(N'2019-10-17T11:33:14.080' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SKO', N'     19', N'Modelo', 51, N'RESM1979', CAST(N'2019-10-17T11:33:14.080' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SKO', N'     20', N'Modelo', 114, N'RESM1979', CAST(N'2019-10-17T11:33:14.173' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SKO', N'     21', N'Modelo', 115, N'RESM1979', CAST(N'2019-10-17T11:33:14.180' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SRF', N'     26', N'Modelo', 119, N'RESM1979', CAST(N'2019-10-17T11:33:14.187' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SRF', N'     27', N'Modelo', 120, N'RESM1979', CAST(N'2019-10-17T11:33:14.190' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SRF', N'     29', N'Modelo', 121, N'RESM1979', CAST(N'2019-10-17T11:33:14.190' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SRF', N'     33', N'Modelo', 160, N'RESM1979', CAST(N'2019-10-17T11:33:14.290' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SRF', N'     34', N'Modelo', 161, N'RESM1979', CAST(N'2019-10-17T11:33:14.290' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SRF', N'     35', N'Modelo', 162, N'RESM1979', CAST(N'2019-10-17T11:33:14.290' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SRF', N'     80', N'Modelo', 148, N'RESM1979', CAST(N'2019-10-17T11:33:14.253' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SRF', N'     86', N'Modelo', 149, N'RESM1979', CAST(N'2019-10-17T11:33:14.260' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'STS', N'     98', N'Modelo', 254, N'RESM1979', CAST(N'2019-10-17T11:33:14.487' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'STS', N'    150', N'Modelo', 322, N'RESM1979', CAST(N'2019-10-17T11:33:14.600' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'STS', N'    153', N'Modelo', 255, N'RESM1979', CAST(N'2019-10-17T11:33:14.487' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'STS', N'    154', N'Modelo', 126, N'RESM1979', CAST(N'2019-10-17T11:33:14.200' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'STS', N'    167', N'Modelo', 189, N'RESM1979', CAST(N'2019-10-17T11:33:14.360' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'STS', N'    170', N'Modelo', 256, N'RESM1979', CAST(N'2019-10-17T11:33:14.490' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'STS', N'    171', N'Modelo', 293, N'RESM1979', CAST(N'2019-10-17T11:33:14.550' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'T60', N'      8', N'Modelo', 136, N'RESM1979', CAST(N'2019-10-17T11:33:14.223' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'T60', N'     13', N'Modelo', 150, N'RESM1979', CAST(N'2019-10-17T11:33:14.260' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TNC', N'     44', N'Modelo', 163, N'RESM1979', CAST(N'2019-10-17T11:33:14.293' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TNS', N'      1', N'Modelo', 216, N'RESM1979', CAST(N'2019-10-17T11:33:14.423' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TNS', N'      2', N'Modelo', 312, N'RESM1979', CAST(N'2019-10-17T11:33:14.583' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TNS', N'      3', N'Modelo', 66, N'RESM1979', CAST(N'2019-10-17T11:33:14.100' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TNS', N'      4', N'Modelo', 304, N'RESM1979', CAST(N'2019-10-17T11:33:14.570' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TNS', N'      5', N'Modelo', 164, N'RESM1979', CAST(N'2019-10-17T11:33:14.293' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TNS', N'      6', N'Modelo', 30, N'RESM1979', CAST(N'2019-10-17T11:33:14.053' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TNS', N'      7', N'Modelo', 31, N'RESM1979', CAST(N'2019-10-17T11:33:14.057' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1619', N'Modelo', 151, N'RESM1979', CAST(N'2019-10-17T11:33:14.263' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1626', N'Modelo', 127, N'RESM1979', CAST(N'2019-10-17T11:33:14.203' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1627', N'Modelo', 128, N'RESM1979', CAST(N'2019-10-17T11:33:14.210' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1628', N'Modelo', 129, N'RESM1979', CAST(N'2019-10-17T11:33:14.213' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1631', N'Modelo', 152, N'RESM1979', CAST(N'2019-10-17T11:33:14.263' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1633', N'Modelo', 153, N'RESM1979', CAST(N'2019-10-17T11:33:14.267' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1634', N'Modelo', 154, N'RESM1979', CAST(N'2019-10-17T11:33:14.270' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1638', N'Modelo', 230, N'RESM1979', CAST(N'2019-10-17T11:33:14.447' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1639', N'Modelo', 231, N'RESM1979', CAST(N'2019-10-17T11:33:14.450' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1643', N'Modelo', 245, N'RESM1979', CAST(N'2019-10-17T11:33:14.470' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1649', N'Modelo', 11, N'RESM1979', CAST(N'2019-10-17T11:33:14.033' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1650', N'Modelo', 12, N'RESM1979', CAST(N'2019-10-17T11:33:14.033' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1651', N'Modelo', 289, N'RESM1979', CAST(N'2019-10-17T11:33:14.547' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1652', N'Modelo', 328, N'RESM1979', CAST(N'2019-10-17T11:33:14.610' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1653', N'Modelo', 117, N'RESM1979', CAST(N'2019-10-17T11:33:14.183' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1654', N'Modelo', 290, N'RESM1979', CAST(N'2019-10-17T11:33:14.547' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1655', N'Modelo', 291, N'RESM1979', CAST(N'2019-10-17T11:33:14.550' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1656', N'Modelo', 292, N'RESM1979', CAST(N'2019-10-17T11:33:14.550' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1658', N'Modelo', 67, N'RESM1979', CAST(N'2019-10-17T11:33:14.100' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1660', N'Modelo', 68, N'RESM1979', CAST(N'2019-10-17T11:33:14.100' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1665', N'Modelo', 69, N'RESM1979', CAST(N'2019-10-17T11:33:14.100' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1667', N'Modelo', 70, N'RESM1979', CAST(N'2019-10-17T11:33:14.103' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOP', N'     75', N'Modelo', 299, N'RESM1979', CAST(N'2019-10-17T11:33:14.560' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOP', N'     79', N'Modelo', 300, N'RESM1979', CAST(N'2019-10-17T11:33:14.563' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOP', N'     80', N'Modelo', 8, N'RESM1979', CAST(N'2019-10-17T11:33:14.030' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOP', N'     89', N'Modelo', 13, N'RESM1979', CAST(N'2019-10-17T11:33:14.037' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOP', N'     92', N'Modelo', 316, N'RESM1979', CAST(N'2019-10-17T11:33:14.590' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOP', N'     93', N'Modelo', 317, N'RESM1979', CAST(N'2019-10-17T11:33:14.593' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOP', N'    106', N'Modelo', 159, N'RESM1979', CAST(N'2019-10-17T11:33:14.287' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOP', N'    107', N'Modelo', 28, N'RESM1979', CAST(N'2019-10-17T11:33:14.053' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOP', N'    111', N'Modelo', 82, N'RESM1979', CAST(N'2019-10-17T11:33:14.120' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOP', N'    112', N'Modelo', 83, N'RESM1979', CAST(N'2019-10-17T11:33:14.120' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    550', N'Modelo', 232, N'RESM1979', CAST(N'2019-10-17T11:33:14.450' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    572', N'Modelo', 233, N'RESM1979', CAST(N'2019-10-17T11:33:14.453' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    589', N'Modelo', 192, N'RESM1979', CAST(N'2019-10-17T11:33:14.367' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    590', N'Modelo', 193, N'RESM1979', CAST(N'2019-10-17T11:33:14.367' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    591', N'Modelo', 194, N'RESM1979', CAST(N'2019-10-17T11:33:14.370' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    592', N'Modelo', 265, N'RESM1979', CAST(N'2019-10-17T11:33:14.503' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    596', N'Modelo', 195, N'RESM1979', CAST(N'2019-10-17T11:33:14.370' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    597', N'Modelo', 173, N'RESM1979', CAST(N'2019-10-17T11:33:14.317' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    601', N'Modelo', 174, N'RESM1979', CAST(N'2019-10-17T11:33:14.320' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    602', N'Modelo', 175, N'RESM1979', CAST(N'2019-10-17T11:33:14.320' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    607', N'Modelo', 176, N'RESM1979', CAST(N'2019-10-17T11:33:14.323' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    608', N'Modelo', 177, N'RESM1979', CAST(N'2019-10-17T11:33:14.327' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    619', N'Modelo', 165, N'RESM1979', CAST(N'2019-10-17T11:33:14.297' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    620', N'Modelo', 166, N'RESM1979', CAST(N'2019-10-17T11:33:14.297' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    628', N'Modelo', 217, N'RESM1979', CAST(N'2019-10-17T11:33:14.423' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    632', N'Modelo', 218, N'RESM1979', CAST(N'2019-10-17T11:33:14.430' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    639', N'Modelo', 274, N'RESM1979', CAST(N'2019-10-17T11:33:14.520' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    640', N'Modelo', 234, N'RESM1979', CAST(N'2019-10-17T11:33:14.453' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    645', N'Modelo', 235, N'RESM1979', CAST(N'2019-10-17T11:33:14.457' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    705', N'Modelo', 71, N'RESM1979', CAST(N'2019-10-17T11:33:14.103' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    713', N'Modelo', 219, N'RESM1979', CAST(N'2019-10-17T11:33:14.430' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    714', N'Modelo', 220, N'RESM1979', CAST(N'2019-10-17T11:33:14.433' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    715', N'Modelo', 221, N'RESM1979', CAST(N'2019-10-17T11:33:14.433' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    716', N'Modelo', 222, N'RESM1979', CAST(N'2019-10-17T11:33:14.437' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    718', N'Modelo', 236, N'RESM1979', CAST(N'2019-10-17T11:33:14.457' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    722', N'Modelo', 237, N'RESM1979', CAST(N'2019-10-17T11:33:14.460' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    723', N'Modelo', 238, N'RESM1979', CAST(N'2019-10-17T11:33:14.460' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    725', N'Modelo', 84, N'RESM1979', CAST(N'2019-10-17T11:33:14.120' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    726', N'Modelo', 318, N'RESM1979', CAST(N'2019-10-17T11:33:14.593' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    727', N'Modelo', 85, N'RESM1979', CAST(N'2019-10-17T11:33:14.123' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    728', N'Modelo', 319, N'RESM1979', CAST(N'2019-10-17T11:33:14.597' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    734', N'Modelo', 223, N'RESM1979', CAST(N'2019-10-17T11:33:14.437' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    740', N'Modelo', 313, N'RESM1979', CAST(N'2019-10-17T11:33:14.587' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    741', N'Modelo', 273, N'RESM1979', CAST(N'2019-10-17T11:33:14.520' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    742', N'Modelo', 275, N'RESM1979', CAST(N'2019-10-17T11:33:14.520' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    744', N'Modelo', 86, N'RESM1979', CAST(N'2019-10-17T11:33:14.123' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    745', N'Modelo', 87, N'RESM1979', CAST(N'2019-10-17T11:33:14.127' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    747', N'Modelo', 276, N'RESM1979', CAST(N'2019-10-17T11:33:14.523' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    749', N'Modelo', 239, N'RESM1979', CAST(N'2019-10-17T11:33:14.463' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    750', N'Modelo', 88, N'RESM1979', CAST(N'2019-10-17T11:33:14.127' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    751', N'Modelo', 89, N'RESM1979', CAST(N'2019-10-17T11:33:14.130' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    762', N'Modelo', 178, N'RESM1979', CAST(N'2019-10-17T11:33:14.330' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    763', N'Modelo', 305, N'RESM1979', CAST(N'2019-10-17T11:33:14.570' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    769', N'Modelo', 224, N'RESM1979', CAST(N'2019-10-17T11:33:14.440' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    780', N'Modelo', 52, N'RESM1979', CAST(N'2019-10-17T11:33:14.080' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    781', N'Modelo', 306, N'RESM1979', CAST(N'2019-10-17T11:33:14.573' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    782', N'Modelo', 53, N'RESM1979', CAST(N'2019-10-17T11:33:14.083' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    783', N'Modelo', 54, N'RESM1979', CAST(N'2019-10-17T11:33:14.083' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    784', N'Modelo', 72, N'RESM1979', CAST(N'2019-10-17T11:33:14.107' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    785', N'Modelo', 41, N'RESM1979', CAST(N'2019-10-17T11:33:14.067' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    786', N'Modelo', 187, N'RESM1979', CAST(N'2019-10-17T11:33:14.353' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    787', N'Modelo', 42, N'RESM1979', CAST(N'2019-10-17T11:33:14.070' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    788', N'Modelo', 43, N'RESM1979', CAST(N'2019-10-17T11:33:14.070' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    789', N'Modelo', 44, N'RESM1979', CAST(N'2019-10-17T11:33:14.070' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    790', N'Modelo', 90, N'RESM1979', CAST(N'2019-10-17T11:33:14.130' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    791', N'Modelo', 91, N'RESM1979', CAST(N'2019-10-17T11:33:14.130' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    804', N'Modelo', 308, N'RESM1979', CAST(N'2019-10-17T11:33:14.577' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    805', N'Modelo', 268, N'RESM1979', CAST(N'2019-10-17T11:33:14.510' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    816', N'Modelo', 269, N'RESM1979', CAST(N'2019-10-17T11:33:14.510' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    837', N'Modelo', 277, N'RESM1979', CAST(N'2019-10-17T11:33:14.527' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    838', N'Modelo', 92, N'RESM1979', CAST(N'2019-10-17T11:33:14.130' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    839', N'Modelo', 93, N'RESM1979', CAST(N'2019-10-17T11:33:14.133' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    840', N'Modelo', 240, N'RESM1979', CAST(N'2019-10-17T11:33:14.463' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    842', N'Modelo', 94, N'RESM1979', CAST(N'2019-10-17T11:33:14.133' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    843', N'Modelo', 95, N'RESM1979', CAST(N'2019-10-17T11:33:14.137' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    844', N'Modelo', 278, N'RESM1979', CAST(N'2019-10-17T11:33:14.527' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    848', N'Modelo', 96, N'RESM1979', CAST(N'2019-10-17T11:33:14.137' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    861', N'Modelo', 73, N'RESM1979', CAST(N'2019-10-17T11:33:14.107' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    862', N'Modelo', 74, N'RESM1979', CAST(N'2019-10-17T11:33:14.110' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TVA', N'      7', N'Modelo', 4, N'RESM1979', CAST(N'2019-10-17T11:33:14.023' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TVA', N'      8', N'Modelo', 131, N'RESM1979', CAST(N'2019-10-17T11:33:14.217' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TVA', N'      9', N'Modelo', 5, N'RESM1979', CAST(N'2019-10-17T11:33:14.027' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TVA', N'     10', N'Modelo', 6, N'RESM1979', CAST(N'2019-10-17T11:33:14.027' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TYP', N'      4', N'Modelo', 257, N'RESM1979', CAST(N'2019-10-17T11:33:14.490' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'VCY', N'      1', N'Modelo', 97, N'RESM1979', CAST(N'2019-10-17T11:33:14.137' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'VCY', N'      2', N'Modelo', 98, N'RESM1979', CAST(N'2019-10-17T11:33:14.140' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'VCY', N'      3', N'Modelo', 99, N'RESM1979', CAST(N'2019-10-17T11:33:14.140' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'VCY', N'      4', N'Modelo', 320, N'RESM1979', CAST(N'2019-10-17T11:33:14.597' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'VCY', N'      5', N'Modelo', 100, N'RESM1979', CAST(N'2019-10-17T11:33:14.143' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'VCY', N'      6', N'Modelo', 321, N'RESM1979', CAST(N'2019-10-17T11:33:14.600' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'VCY', N'      7', N'Modelo', 101, N'RESM1979', CAST(N'2019-10-17T11:33:14.143' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'WPL', N'     19', N'Modelo', 75, N'RESM1979', CAST(N'2019-10-17T11:33:14.110' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'WPL', N'     20', N'Modelo', 14, N'RESM1979', CAST(N'2019-10-17T11:33:14.037' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'WPL', N'     21', N'Modelo', 15, N'RESM1979', CAST(N'2019-10-17T11:33:14.037' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'WPL', N'     22', N'Modelo', 16, N'RESM1979', CAST(N'2019-10-17T11:33:14.040' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'WPL', N'     23', N'Modelo', 17, N'RESM1979', CAST(N'2019-10-17T11:33:14.040' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'WPL', N'     26', N'Modelo', 138, N'RESM1979', CAST(N'2019-10-17T11:33:14.227' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'WPL', N'     27', N'Modelo', 118, N'RESM1979', CAST(N'2019-10-17T11:33:14.187' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YON', N'     21', N'Modelo', 241, N'RESM1979', CAST(N'2019-10-17T11:33:14.467' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YON', N'     28', N'Modelo', 246, N'RESM1979', CAST(N'2019-10-17T11:33:14.473' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YON', N'     42', N'Modelo', 247, N'RESM1979', CAST(N'2019-10-17T11:33:14.477' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YON', N'     44', N'Modelo', 167, N'RESM1979', CAST(N'2019-10-17T11:33:14.300' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YON', N'     45', N'Modelo', 168, N'RESM1979', CAST(N'2019-10-17T11:33:14.300' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YON', N'     48', N'Modelo', 200, N'RESM1979', CAST(N'2019-10-17T11:33:14.387' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YON', N'     53', N'Modelo', 201, N'RESM1979', CAST(N'2019-10-17T11:33:14.387' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YON', N'     54', N'Modelo', 202, N'RESM1979', CAST(N'2019-10-17T11:33:14.390' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YON', N'     55', N'Modelo', 203, N'RESM1979', CAST(N'2019-10-17T11:33:14.390' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YON', N'     64', N'Modelo', 263, N'RESM1979', CAST(N'2019-10-17T11:33:14.500' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YON', N'     68', N'Modelo', 169, N'RESM1979', CAST(N'2019-10-17T11:33:14.303' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YON', N'     70', N'Modelo', 104, N'RESM1979', CAST(N'2019-10-17T11:33:14.147' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YON', N'     71', N'Modelo', 105, N'RESM1979', CAST(N'2019-10-17T11:33:14.150' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YUY', N'    155', N'Modelo', 248, N'RESM1979', CAST(N'2019-10-17T11:33:14.477' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YUY', N'    157', N'Modelo', 281, N'RESM1979', CAST(N'2019-10-17T11:33:14.530' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YUY', N'    159', N'Modelo', 249, N'RESM1979', CAST(N'2019-10-17T11:33:14.480' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YUY', N'    160', N'Modelo', 282, N'RESM1979', CAST(N'2019-10-17T11:33:14.533' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YUY', N'    165', N'Modelo', 283, N'RESM1979', CAST(N'2019-10-17T11:33:14.533' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YUY', N'    198', N'Modelo', 250, N'RESM1979', CAST(N'2019-10-17T11:33:14.480' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YUY', N'    199', N'Modelo', 204, N'RESM1979', CAST(N'2019-10-17T11:33:14.390' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ZLA', N'     10', N'Modelo', 279, N'RESM1979', CAST(N'2019-10-17T11:33:14.530' AS DateTime))
GO
INSERT [dbo].[promociones] ([idpromocion], [nombre], [fecha], [tipo], [estatus], [iniciopromo], [finpromo], [preciero], [senalizador], [clasificacion], [imagen], [minunicompra], [minimpcompra], [unipromo], [acumulable], [paresunicos], [clinoregis], [idusuariocaptura], [fumcaptura], [idusuarioaplica], [fumaplica], [idusuariopausa], [fumpausa], [idusuariocancela], [fumcancela], [idusuario], [fum]) VALUES (4, N'PROMOCION 3X249', CAST(N'2019-10-17' AS Date), N'DIRECTA', N'ACTIVO', CAST(N'2019-10-17T00:00:00.000' AS DateTime), CAST(N'2019-12-31T00:00:00.000' AS DateTime), N'AZUL', N'BLANCO', N'REBAJA', null, 3, CAST(0.00 AS Numeric(18, 2)), 0, N'NO', N'SI', N'SI', N'RESM1979', CAST(N'2019-10-17T12:03:16.083' AS DateTime), N'RESM1979', CAST(N'2019-10-17T12:11:53.440' AS DateTime), NULL, NULL, NULL, NULL, N'RESM1979', CAST(N'2019-10-17T12:11:53.440' AS DateTime))
GO
INSERT [dbo].[promocionesagrupaciones] ([idpromocion], [idagrupacioncompra], [idagrupacionpromo], [renglon], [idusuario], [fum]) VALUES (4, 0, 1, 1, N'RESM1979', CAST(N'2019-10-17T12:03:24.117' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (4, N'CP', 0, N'PROMO', CAST(259.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T12:05:53.027' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (4, N'CS', 0, N'PROMO', CAST(259.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T12:05:53.023' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (4, N'CV', 0, N'PROMO', CAST(259.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T12:05:53.020' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (4, N'EF', 0, N'PROMO', CAST(83.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T12:05:53.007' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (4, N'MD', 0, N'PROMO', CAST(83.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T12:05:53.013' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (4, N'TC', 0, N'PROMO', CAST(83.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T12:05:53.010' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (4, N'TD', 0, N'PROMO', CAST(83.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T12:05:53.010' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (4, N'TO', 0, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T12:05:53.000' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (4, N'VA', 0, N'PROMO', CAST(259.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T12:05:53.017' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (4, N'VS', 0, N'PROMO', CAST(259.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T12:05:53.020' AS DateTime))
GO
INSERT [dbo].[promocionesplazas] ([idpromocion], [plaza], [sucursal], [idusuario], [fum]) VALUES (4, N'01', N'', N'RESM1979', CAST(N'2019-10-17T12:11:22.447' AS DateTime))
GO
INSERT [dbo].[promocionesrecurrencia] ([idpromocion], [dia], [horainicio], [horafin], [idusuario], [fum]) VALUES (4, N'Lunes', N'00:00', N'23:59', N'RESM1979', CAST(N'2019-12-27T14:13:34.000' AS DateTime))
GO
INSERT [dbo].[promocionesrecurrencia] ([idpromocion], [dia], [horainicio], [horafin], [idusuario], [fum]) VALUES (4, N'Sábado', N'00:00', N'23:59', N'RESM1979', CAST(N'2019-12-27T14:13:40.393' AS DateTime))
GO


--promocion 3X1 Y 1/2 - 5 elementos

INSERT [dbo].[promociones] ([idpromocion], [nombre], [fecha], [tipo], [estatus], [iniciopromo], [finpromo], [preciero], [senalizador], [clasificacion], [imagen], [minunicompra], [minimpcompra], [unipromo], [acumulable], [paresunicos], [clinoregis], [idusuariocaptura], [fumcaptura], [idusuarioaplica], [fumaplica], [idusuariopausa], [fumpausa], [idusuariocancela], [fumcancela], [idusuario], [fum]) VALUES (18, N'PROMOCION  3X1 Y 1/2', CAST(N'2019-12-27' AS Date), N'AxB', N'ACTIVO', CAST(N'2019-12-27T00:00:00.000' AS DateTime), CAST(N'2020-02-27T00:00:00.000' AS DateTime), N'AZUL', N'BLANCO', N'REBAJA', null, 3, CAST(0.00 AS Numeric(18, 2)), 2, N'NO', N'SI', N'SI', N'RESM1979', CAST(N'2019-12-27T11:55:57.323' AS DateTime), N'RESM1979', CAST(N'2019-12-27T14:15:12.280' AS DateTime), NULL, NULL, NULL, NULL, N'RESM1979', CAST(N'2019-12-27T14:15:12.280' AS DateTime))
GO
INSERT [dbo].[promocionesagrupaciones] ([idpromocion], [idagrupacioncompra], [idagrupacionpromo], [renglon], [idusuario], [fum]) VALUES (18, 2, 2, 1, N'RESM1979', CAST(N'2019-12-27T12:35:14.290' AS DateTime))
GO
--INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (18, N'EF', 4, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T12:33:22.387' AS DateTime))
--GO
--INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (18, N'MD', 4, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T12:33:22.400' AS DateTime))
--GO
--INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (18, N'TC', 4, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T12:33:22.400' AS DateTime))
--GO
--INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (18, N'TD', 4, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T12:33:22.400' AS DateTime))
--GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (18, N'TO', 4, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(100.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T12:33:22.387' AS DateTime))
GO

INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (18, N'TC', 5, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(50.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T12:33:39.497' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (18, N'TD', 5, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(50.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T12:33:39.497' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (18, N'TO', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T12:46:31.903' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (18, N'TO', 2, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T11:55:57.323' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (18, N'TO', 3, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T11:55:57.323' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (18, N'TO', 5, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T12:33:39.497' AS DateTime))
GO
INSERT [dbo].[promocionesplazas] ([idpromocion], [plaza], [sucursal], [idusuario], [fum]) VALUES (18, N'01', N'01', N'RESM1979', CAST(N'2019-12-27T14:13:55.817' AS DateTime))
GO
INSERT [dbo].[promocionesplazas] ([idpromocion], [plaza], [sucursal], [idusuario], [fum]) VALUES (18, N'01', N'08', N'RESM1979', CAST(N'2019-12-27T14:13:55.817' AS DateTime))
GO
INSERT [dbo].[promocionesplazas] ([idpromocion], [plaza], [sucursal], [idusuario], [fum]) VALUES (18, N'02', N'11', N'RESM1979', CAST(N'2019-12-27T14:13:51.190' AS DateTime))
GO
INSERT [dbo].[promocionesrecurrencia] ([idpromocion], [dia], [horainicio], [horafin], [idusuario], [fum]) VALUES (18, N'Lunes', N'00:00', N'23:59', N'RESM1979', CAST(N'2019-12-27T14:13:34.000' AS DateTime))
GO
INSERT [dbo].[promocionesrecurrencia] ([idpromocion], [dia], [horainicio], [horafin], [idusuario], [fum]) VALUES (18, N'Sábado', N'00:00', N'23:59', N'RESM1979', CAST(N'2019-12-27T14:13:40.393' AS DateTime))
GO
INSERT [dbo].[agrupaciones] ([idagrupacion], [nombre], [fecha], [idusuario], [fum]) VALUES (2, N'PROMOCION CALZADO DAMA 30% EFE', CAST(N'2019-10-17' AS Date), N'RESM1979', CAST(N'2019-10-17T10:42:20.663' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (2, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'', N'', N'Departamento', 1, N'RESM1979', CAST(N'2019-10-17T10:42:44.727' AS DateTime))
GO

--promocion 3X1 Y 1/2 - 3 elementos
--INSERT [dbo].[agrupaciones] ([idagrupacion], [nombre], [fecha], [idusuario], [fum]) VALUES (2, N'PROMOCION CALZADO DAMA 30% EFE', CAST(N'2019-10-17' AS Date), N'RESM1979', CAST(N'2019-10-17T10:42:20.663' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (2, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, N'', N'', N'Departamento', 1, N'RESM1979', CAST(N'2019-10-17T10:42:44.727' AS DateTime))
--GO
INSERT [dbo].[promociones] ([idpromocion], [nombre], [fecha], [tipo], [estatus], [iniciopromo], [finpromo], [preciero], [senalizador], [clasificacion], [imagen], [minunicompra], [minimpcompra], [unipromo], [acumulable], [paresunicos], [clinoregis], [idusuariocaptura], [fumcaptura], [idusuarioaplica], [fumaplica], [idusuariopausa], [fumpausa], [idusuariocancela], [fumcancela], [idusuario], [fum]) VALUES (180, N'PROMOCION  3X1 Y 1/2', CAST(N'2019-12-27' AS Date), N'AxB', N'ACTIVO', CAST(N'2019-12-27T00:00:00.000' AS DateTime), CAST(N'2020-02-27T00:00:00.000' AS DateTime), N'AZUL', N'BLANCO', N'REBAJA', null, 1, CAST(0.00 AS Numeric(18, 2)), 2, N'NO', N'SI', N'SI', N'RESM1979', CAST(N'2019-12-27T11:55:57.323' AS DateTime), N'RESM1979', CAST(N'2019-12-27T14:15:12.280' AS DateTime), NULL, NULL, NULL, NULL, N'RESM1979', CAST(N'2019-12-27T14:15:12.280' AS DateTime))
GO
INSERT [dbo].[promocionesagrupaciones] ([idpromocion], [idagrupacioncompra], [idagrupacionpromo], [renglon], [idusuario], [fum]) VALUES (180, 2, 2, 1, N'RESM1979', CAST(N'2019-12-27T12:35:14.290' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (180, N'TC', 3, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(50.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T12:33:39.497' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (180, N'TD', 3, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(50.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T12:33:39.497' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (180, N'TO', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T12:46:31.903' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (180, N'TO', 2, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(100.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T12:33:22.387' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (180, N'TO', 3, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T12:33:39.497' AS DateTime))
GO
INSERT [dbo].[promocionesplazas] ([idpromocion], [plaza], [sucursal], [idusuario], [fum]) VALUES (180, N'01', N'08', N'RESM1979', CAST(N'2019-12-27T14:13:55.817' AS DateTime))
GO
INSERT [dbo].[promocionesplazas] ([idpromocion], [plaza], [sucursal], [idusuario], [fum]) VALUES (180, N'02', N'11', N'RESM1979', CAST(N'2019-12-27T14:13:51.190' AS DateTime))
GO



-- promocion SANDALIA 2X1
INSERT [dbo].[agrupaciones] ([idagrupacion], [nombre], [fecha], [idusuario], [fum]) VALUES (11, N'AGRUPACIONES SANDALIA 2X1', CAST(N'2019-12-10' AS Date), N'RESM1979', CAST(N'2019-12-10T12:42:39.053' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (11, 1, 1, 2, 547, 1123, 0, 0, 0, 0, 0, N'', N'', N'L1', 1, N'RESM1979', CAST(N'2019-12-10T11:03:00.890' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (11, 1, 1, 155, 549, 1155, 0, 0, 0, 0, 0, N'', N'', N'L1', 6, N'RESM1979', CAST(N'2019-12-10T11:06:01.157' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (11, 1, 2, 9, 548, 1136, 0, 0, 0, 0, 0, N'', N'', N'L1', 2, N'RESM1979', CAST(N'2019-12-10T11:03:22.110' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (11, 1, 2, 156, 550, 1161, 0, 0, 0, 0, 0, N'', N'', N'L1', 7, N'RESM1979', CAST(N'2019-12-10T11:06:49.067' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (11, 1, 15, 68, 303, 479, 0, 0, 0, 0, 0, N'', N'', N'L1', 3, N'RESM1979', CAST(N'2019-12-10T11:03:46.347' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (11, 1, 15, 68, 304, 506, 0, 0, 0, 0, 0, N'', N'', N'L1', 4, N'RESM1979', CAST(N'2019-12-10T11:04:20.680' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (11, 1, 15, 157, 551, 1171, 0, 0, 0, 0, 0, N'', N'', N'L1', 8, N'RESM1979', CAST(N'2019-12-10T11:07:51.697' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (11, 1, 15, 157, 552, 1178, 0, 0, 0, 0, 0, N'', N'', N'L1', 9, N'RESM1979', CAST(N'2019-12-10T11:08:57.273' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (11, 1, 16, 73, 311, 535, 0, 0, 0, 0, 0, N'', N'', N'L1', 5, N'RESM1979', CAST(N'2019-12-10T11:05:12.043' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (11, 1, 16, 73, 312, 539, 0, 0, 0, 0, 0, N'', N'', N'L1', 12, N'RESM1979', CAST(N'2019-12-10T11:11:00.763' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (11, 1, 16, 158, 553, 1183, 0, 0, 0, 0, 0, N'', N'', N'L1', 11, N'RESM1979', CAST(N'2019-12-10T11:10:20.917' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (11, 1, 16, 158, 554, 1188, 0, 0, 0, 0, 0, N'', N'', N'L1', 10, N'RESM1979', CAST(N'2019-12-10T11:09:45.330' AS DateTime))
GO
--INSERT [dbo].[promociones] ([idpromocion], [nombre], [fecha], [tipo], [estatus], [iniciopromo], [finpromo], [preciero], [senalizador], [clasificacion], [imagen], [minunicompra], [minimpcompra], [unipromo], [acumulable], [paresunicos], [clinoregis], [idusuariocaptura], [fumcaptura], [idusuarioaplica], [fumaplica], [idusuariopausa], [fumpausa], [idusuariocancela], [fumcancela], [idusuario], [fum]) VALUES (15, N'SANDALIA 2X1', CAST(N'2019-12-10' AS Date), N'AxB', N'ACTIVO', CAST(N'2019-12-10T00:00:00.000' AS DateTime), CAST(N'2020-02-29T00:00:00.000' AS DateTime), N'AZUL', N'BLANCO', N'REBAJA', NULL, 2, CAST(0.00 AS Numeric(18, 2)), 1, N'NO', N'SI', N'SI', N'RESM1979', CAST(N'2019-12-10T12:39:47.403' AS DateTime), N'RESM1979', CAST(N'2019-12-10T12:48:16.930' AS DateTime), NULL, NULL, NULL, NULL, N'RESM1979', CAST(N'2019-12-10T12:48:16.930' AS DateTime))
INSERT [dbo].[promociones] ([idpromocion], [nombre], [fecha], [tipo], [estatus], [iniciopromo], [finpromo], [preciero], [senalizador], [clasificacion], [imagen], [minunicompra], [minimpcompra], [unipromo], [acumulable], [paresunicos], [clinoregis], [idusuariocaptura], [fumcaptura], [idusuarioaplica], [fumaplica], [idusuariopausa], [fumpausa], [idusuariocancela], [fumcancela], [idusuario], [fum]) VALUES (15, N'SANDALIA 2X1', CAST(N'2019-12-10' AS Date), N'AxB', N'ACTIVO', CAST(N'2019-12-10T00:00:00.000' AS DateTime), CAST(N'2020-02-29T00:00:00.000' AS DateTime), N'AZUL', N'BLANCO', N'REBAJA', NULL, 1, CAST(0.00 AS Numeric(18, 2)), 1, N'NO', N'SI', N'SI', N'RESM1979', CAST(N'2019-12-10T12:39:47.403' AS DateTime), N'RESM1979', CAST(N'2019-12-10T12:48:16.930' AS DateTime), NULL, NULL, NULL, NULL, N'RESM1979', CAST(N'2019-12-10T12:48:16.930' AS DateTime))
GO
INSERT [dbo].[promocionesagrupaciones] ([idpromocion], [idagrupacioncompra], [idagrupacionpromo], [renglon], [idusuario], [fum]) VALUES (15, 11, 11, 1, N'RESM1979', CAST(N'2019-12-10T12:40:12.550' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (15, N'TO', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-10T12:39:47.403' AS DateTime))
GO
--INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (15, N'TO', 2, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-10T12:39:47.403' AS DateTime))
--GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (15, N'TO', 3, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(100.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-10T12:40:47.820' AS DateTime))
GO
INSERT [dbo].[promocionesexclusiones] ([idpromocion], [marca], [estilon], [idusuario], [fum]) VALUES (15, N'OZO', N'', N'RESM1979', CAST(N'2019-12-10T12:41:25.360' AS DateTime))
GO
INSERT [dbo].[promocionesplazas] ([idpromocion], [plaza], [sucursal], [idusuario], [fum]) VALUES (15, N'01', N'', N'RESM1979', CAST(N'2019-12-10T12:41:36.517' AS DateTime))
GO

-- promocion 3 descuento y 1 accerio gratis
INSERT [dbo].[agrupaciones] ([idagrupacion], [nombre], [fecha], [idusuario], [fum]) VALUES (13, N'AGRUPACION CALZADO CABALLERO', CAST(N'2019-12-10' AS Date), N'RESM1979', CAST(N'2019-12-10T12:28:31.520' AS DateTime))
GO
INSERT [dbo].[agrupaciones] ([idagrupacion], [nombre], [fecha], [idusuario], [fum]) VALUES (14, N'AGRUPACION ACCESORIOS', CAST(N'2019-12-27' AS Date), N'RESM1979', CAST(N'2019-12-27T10:17:10.787' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (13, 1, 2, 0, 0, 0, 0, 0, 0, 0, 0, N'', N'', N'Departamento', 1, N'RESM1979', CAST(N'2019-12-10T12:28:43.643' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (14, 2, 8, 0, 0, 0, 0, 0, 0, 0, 0, N'TOY', N'', N'Departamento', 1, N'RESM1979', CAST(N'2019-12-27T10:19:16.083' AS DateTime))
GO
INSERT [dbo].[cupones] ([idcupon], [nombre], [descripcion], [restricciones], [fecha], [estatus], [tipo], [imagen], [fecini], [fecfin], [idusuariocaptura], [fumcaptura], [idusuarioactiva], [fumactiva], [idusuariocancela], [fumcancela], [idusuario], [fum]) VALUES (4, N'CUPON COMPRA EXCLUSIVA', N'LA PROMOCION INCLUYE UNIDADES GRATIS CON MONEDERO ELECTRONICO Y BONO DE DESCUENTO', N'NO ENTRA MARCA OZONO NI CONVERESE', CAST(N'2019-12-27' AS Date), N'ACTIVO', N'MULTIPLE', null, CAST(N'2019-12-27' AS Date), CAST(N'2020-02-29' AS Date), N'RESM1979', CAST(N'2019-12-27T14:02:05.210' AS DateTime), N'RESM1979', CAST(N'2019-12-27T14:09:52.073' AS DateTime), NULL, NULL, N'RESM1979', CAST(N'2019-12-27T14:09:52.073' AS DateTime))
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia], [idcliente]) VALUES (4, N'0000000251', N'ACTIVO', N'RESM1979', CAST(N'2019-12-27T14:07:24.197' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia], [idcliente]) VALUES (4, N'0000000252', N'ACTIVO', N'RESM1979', CAST(N'2019-12-27T14:07:24.197' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia], [idcliente]) VALUES (4, N'0000000253', N'ACTIVO', N'RESM1979', CAST(N'2019-12-27T14:07:24.197' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia], [idcliente]) VALUES (4, N'0000000254', N'ACTIVO', N'RESM1979', CAST(N'2019-12-27T14:07:24.197' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia], [idcliente]) VALUES (4, N'0000000255', N'ACTIVO', N'RESM1979', CAST(N'2019-12-27T14:07:24.197' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia], [idcliente]) VALUES (4, N'0000000256', N'ACTIVO', N'RESM1979', CAST(N'2019-12-27T14:07:24.197' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia], [idcliente]) VALUES (4, N'0000000257', N'ACTIVO', N'RESM1979', CAST(N'2019-12-27T14:07:24.197' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia], [idcliente]) VALUES (4, N'0000000258', N'ACTIVO', N'RESM1979', CAST(N'2019-12-27T14:07:24.197' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia], [idcliente]) VALUES (4, N'0000000259', N'ACTIVO', N'RESM1979', CAST(N'2019-12-27T14:07:24.197' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia], [idcliente]) VALUES (4, N'0000000260', N'ACTIVO', N'RESM1979', CAST(N'2019-12-27T14:07:24.210' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia], [idcliente]) VALUES (4, N'0000000261', N'ACTIVO', N'RESM1979', CAST(N'2019-12-27T14:07:24.210' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia], [idcliente]) VALUES (4, N'0000000262', N'ACTIVO', N'RESM1979', CAST(N'2019-12-27T14:07:24.210' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia], [idcliente]) VALUES (4, N'0000000263', N'ACTIVO', N'RESM1979', CAST(N'2019-12-27T14:07:24.210' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia], [idcliente]) VALUES (4, N'0000000264', N'ACTIVO', N'RESM1979', CAST(N'2019-12-27T14:07:24.210' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia], [idcliente]) VALUES (4, N'0000000265', N'ACTIVO', N'RESM1979', CAST(N'2019-12-27T14:07:24.210' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia], [idcliente]) VALUES (4, N'0000000266', N'ACTIVO', N'RESM1979', CAST(N'2019-12-27T14:07:24.210' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia], [idcliente]) VALUES (4, N'0000000267', N'ACTIVO', N'RESM1979', CAST(N'2019-12-27T14:07:24.210' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia], [idcliente]) VALUES (4, N'0000000268', N'ACTIVO', N'RESM1979', CAST(N'2019-12-27T14:07:24.210' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia], [idcliente]) VALUES (4, N'0000000269', N'ACTIVO', N'RESM1979', CAST(N'2019-12-27T14:07:24.210' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia], [idcliente]) VALUES (4, N'0000000270', N'ACTIVO', N'RESM1979', CAST(N'2019-12-27T14:07:24.210' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia], [idcliente]) VALUES (4, N'0000000271', N'ACTIVO', N'RESM1979', CAST(N'2019-12-27T14:07:24.210' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia], [idcliente]) VALUES (4, N'0000000272', N'ACTIVO', N'RESM1979', CAST(N'2019-12-27T14:07:24.227' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia], [idcliente]) VALUES (4, N'0000000273', N'ACTIVO', N'RESM1979', CAST(N'2019-12-27T14:07:24.227' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia], [idcliente]) VALUES (4, N'0000000274', N'ACTIVO', N'RESM1979', CAST(N'2019-12-27T14:07:24.227' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia], [idcliente]) VALUES (4, N'0000000275', N'ACTIVO', N'RESM1979', CAST(N'2019-12-27T14:07:24.227' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia], [idcliente]) VALUES (4, N'0000000276', N'ACTIVO', N'RESM1979', CAST(N'2019-12-27T14:07:24.227' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia], [idcliente]) VALUES (4, N'0000000277', N'ACTIVO', N'RESM1979', CAST(N'2019-12-27T14:07:24.227' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia], [idcliente]) VALUES (4, N'0000000278', N'ACTIVO', N'RESM1979', CAST(N'2019-12-27T14:07:24.227' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia], [idcliente]) VALUES (4, N'0000000279', N'ACTIVO', N'RESM1979', CAST(N'2019-12-27T14:07:24.227' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[cuponesdet] ([idcupon], [folio], [estatus], [idusuario], [fum], [referencia], [idcliente]) VALUES (4, N'0000000280', N'ACTIVO', N'RESM1979', CAST(N'2019-12-27T14:07:24.227' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[promociones] ([idpromocion], [nombre], [fecha], [tipo], [estatus], [iniciopromo], [finpromo], [preciero], [senalizador], [clasificacion], [imagen], [minunicompra], [minimpcompra], [unipromo], [acumulable], [paresunicos], [clinoregis], [idusuariocaptura], [fumcaptura], [idusuarioaplica], [fumaplica], [idusuariopausa], [fumpausa], [idusuariocancela], [fumcancela], [idusuario], [fum]) VALUES (17, N'ACCESORIOS GRATIS  X CALZADO C', CAST(N'2019-12-27' AS Date), N'AxB', N'ACTIVO', CAST(N'2019-12-27T00:00:00.000' AS DateTime), CAST(N'2020-02-29T00:00:00.000' AS DateTime), N'AZUL', N'BLANCO', N'REBAJA', null, 3, CAST(0.00 AS Numeric(18, 2)), 1, N'NO', N'SI', N'SI', N'RESM1979', CAST(N'2019-12-27T10:32:09.857' AS DateTime), N'RESM1979', CAST(N'2019-12-27T14:11:34.260' AS DateTime), NULL, NULL, NULL, NULL, N'RESM1979', CAST(N'2019-12-27T14:11:34.260' AS DateTime))
GO
INSERT [dbo].[promocionesagrupaciones] ([idpromocion], [idagrupacioncompra], [idagrupacionpromo], [renglon], [idusuario], [fum]) VALUES (17, 13, 14, 1, N'RESM1979', CAST(N'2019-12-27T11:48:00.190' AS DateTime))
GO
INSERT [dbo].[promocionescupones] ([idpromocion], [idcupon], [idusuario], [fum]) VALUES (17, 4, N'RESM1979', CAST(N'2019-12-27T14:09:57.870' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'CP', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:52:10.027' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'CP', 2, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:52:26.543' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'CP', 3, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:51:33.150' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'CP', 4, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(100.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:53:40.790' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'CS', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:52:10.027' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'CS', 2, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:52:26.543' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'CS', 3, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:51:33.150' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'CS', 4, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(100.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:53:40.790' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'CV', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:52:10.027' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'CV', 2, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:52:26.543' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'CV', 3, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:51:33.150' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'CV', 4, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(100.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:53:40.790' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'DV', 3, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:51:33.133' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'DV', 4, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(100.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:53:40.773' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'EF', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(50.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:52:10.010' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'EF', 2, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:52:26.513' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'EF', 3, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:51:33.133' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'EF', 4, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(100.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(100.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:53:40.760' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'MD', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:52:10.010' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'MD', 2, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:52:26.530' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'MD', 3, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:51:33.133' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'MD', 4, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(100.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(100.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:53:40.773' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'TC', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(50.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:52:10.010' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'TC', 2, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:52:26.530' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'TC', 3, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:51:33.133' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'TC', 4, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(100.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(100.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:53:40.773' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'TD', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(50.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:52:10.010' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'TD', 2, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:52:26.530' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'TD', 3, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:51:33.133' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'TD', 4, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(100.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(100.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:53:40.773' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'TO', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:52:10.010' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'TO', 2, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:52:26.513' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'TO', 3, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:51:33.133' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'TO', 4, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:53:40.760' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'VA', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:52:10.027' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'VA', 2, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:52:26.530' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'VA', 3, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:51:33.150' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'VS', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:52:10.027' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'VS', 2, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:52:26.530' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'VS', 3, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:51:33.150' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum]) VALUES (17, N'VS', 4, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(100.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-12-27T13:53:40.773' AS DateTime))
GO
INSERT [dbo].[promocionesexclusiones] ([idpromocion], [marca], [estilon], [idusuario], [fum]) VALUES (17, N'CVR', N'', N'RESM1979', CAST(N'2019-12-27T13:48:36.293' AS DateTime))
GO
INSERT [dbo].[promocionesexclusiones] ([idpromocion], [marca], [estilon], [idusuario], [fum]) VALUES (17, N'OZO', N'', N'RESM1979', CAST(N'2019-12-27T13:48:31.480' AS DateTime))
GO
INSERT [dbo].[promocionesplazas] ([idpromocion], [plaza], [sucursal], [idusuario], [fum]) VALUES (17, N'01', N'01', N'RESM1979', CAST(N'2019-12-27T13:32:47.963' AS DateTime))
GO
INSERT [dbo].[promocionesplazas] ([idpromocion], [plaza], [sucursal], [idusuario], [fum]) VALUES (17, N'01', N'02', N'RESM1979', CAST(N'2019-12-27T13:32:53.277' AS DateTime))
GO
INSERT [dbo].[promocionesplazas] ([idpromocion], [plaza], [sucursal], [idusuario], [fum]) VALUES (17, N'01', N'08', N'RESM1979', CAST(N'2019-12-27T13:33:07.527' AS DateTime))
GO
INSERT [dbo].[promocionesrecurrencia] ([idpromocion], [dia], [horainicio], [horafin], [idusuario], [fum]) VALUES (17, N'Martes', N'10:00', N'22:00', N'RESM1979', CAST(N'2019-12-27T13:33:49.300' AS DateTime))
GO


--promocion calzado 20% cred, 30% cont, sin DV
--2, N'CALZADO 20% CREDITO, 30% CONTA'
--50% va sin promo
INSERT [dbo].[agrupaciones] ([idagrupacion], [nombre], [fecha], [idusuario], [fum]) VALUES (3, N'CALZADO 20% CREDITO 30% CONTAD', CAST(N'2019-10-17' AS Date), N'RESM1979', CAST(N'2019-10-17T11:09:57.383' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (3, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'', N'', N'División', 1, N'RESM1979', CAST(N'2019-10-17T11:10:19.247' AS DateTime))
GO
INSERT [dbo].[promociones] ([idpromocion], [nombre], [fecha], [tipo], [estatus], [iniciopromo], [finpromo], [preciero], [senalizador], [clasificacion], [imagen], [minunicompra], [minimpcompra], [unipromo], [acumulable], [paresunicos], [clinoregis], [idusuariocaptura], [fumcaptura], [idusuarioaplica], [fumaplica], [idusuariopausa], [fumpausa], [idusuariocancela], [fumcancela], [idusuario], [fum], [promosrequeridas], [duplicados]) VALUES (2, N'CALZADO 20% CREDITO, 30% CONTA', CAST(N'2019-10-17' AS Date), N'DIRECTA', N'ACTIVO', CAST(N'2019-10-18T00:00:00.000' AS DateTime), CAST(N'2020-02-28T00:00:00.000' AS DateTime), N'AZUL', N'BLANCO', N'REBAJA', null, 1, CAST(0.00 AS Numeric(18, 2)), 0, N'NO', N'SI', N'SI', N'RESM1979', CAST(N'2019-10-17T11:11:41.653' AS DateTime), N'RESM1979', CAST(N'2019-10-17T12:09:09.680' AS DateTime), N'RESM1979', CAST(N'2019-11-16T10:16:44.550' AS DateTime), NULL, NULL, N'RESM1979', CAST(N'2019-11-16T10:16:44.550' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[promocionesagrupaciones] ([idpromocion], [idagrupacioncompra], [idagrupacionpromo], [renglon], [idusuario], [fum]) VALUES (2, 0, 3, 1, N'RESM1979', CAST(N'2019-10-17T11:11:56.313' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (2, N'CP', 0, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(20.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.930' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (2, N'CS', 0, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(20.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.930' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (2, N'CV', 0, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(20.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.927' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (2, N'EF', 0, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(30.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.910' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (2, N'MD', 0, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(30.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.917' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (2, N'TC', 0, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(30.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.917' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (2, N'TD', 0, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(30.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.913' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (2, N'TO', 0, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.907' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (2, N'VA', 0, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(20.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.920' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (2, N'VD', 0, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(20.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.920' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (2, N'VS', 0, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(50.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.923' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesexclusiones] ([idpromocion], [marca], [estilon], [idusuario], [fum]) VALUES (2, N'FFF', N'', N'RESM1979', CAST(N'2019-10-17T11:12:23.020' AS DateTime))
GO
INSERT [dbo].[promocionesexclusiones] ([idpromocion], [marca], [estilon], [idusuario], [fum]) VALUES (2, N'OZO', N'', N'RESM1979', CAST(N'2019-10-17T11:12:55.407' AS DateTime))
GO
INSERT [dbo].[promocionesplazas] ([idpromocion], [plaza], [sucursal], [idusuario], [fum]) VALUES (2, N'01', N'', N'RESM1979', CAST(N'2019-10-17T12:08:57.990' AS DateTime))
GO

-- PROMOCION 149 CONT/ 259 CREDIT
--INSERT [dbo].[agrupaciones] ([idagrupacion], [nombre], [fecha], [idusuario], [fum]) VALUES (1, N'PROMOCION 3X249 EFECTIVO', CAST(N'2019-10-17' AS Date), N'RESM1979', CAST(N'2019-10-17T10:15:24.800' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ADZ', N'     20', N'Modelo', 205, N'RESM1979', CAST(N'2019-10-17T11:33:14.393' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ADZ', N'     38', N'Modelo', 309, N'RESM1979', CAST(N'2019-10-17T11:33:14.580' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ADZ', N'     41', N'Modelo', 270, N'RESM1979', CAST(N'2019-10-17T11:33:14.513' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ADZ', N'     52', N'Modelo', 323, N'RESM1979', CAST(N'2019-10-17T11:33:14.603' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ADZ', N'     53', N'Modelo', 285, N'RESM1979', CAST(N'2019-10-17T11:33:14.537' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ADZ', N'     57', N'Modelo', 286, N'RESM1979', CAST(N'2019-10-17T11:33:14.540' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ADZ', N'     58', N'Modelo', 287, N'RESM1979', CAST(N'2019-10-17T11:33:14.540' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ARA', N'    154', N'Modelo', 106, N'RESM1979', CAST(N'2019-10-17T11:33:14.153' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ARA', N'    156', N'Modelo', 107, N'RESM1979', CAST(N'2019-10-17T11:33:14.157' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ARA', N'    159', N'Modelo', 45, N'RESM1979', CAST(N'2019-10-17T11:33:14.073' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ARA', N'    160', N'Modelo', 108, N'RESM1979', CAST(N'2019-10-17T11:33:14.157' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ARA', N'    161', N'Modelo', 109, N'RESM1979', CAST(N'2019-10-17T11:33:14.160' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ARA', N'    162', N'Modelo', 284, N'RESM1979', CAST(N'2019-10-17T11:33:14.537' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ARA', N'    163', N'Modelo', 46, N'RESM1979', CAST(N'2019-10-17T11:33:14.073' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ARA', N'    164', N'Modelo', 47, N'RESM1979', CAST(N'2019-10-17T11:33:14.073' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ARA', N'    165', N'Modelo', 48, N'RESM1979', CAST(N'2019-10-17T11:33:14.077' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'BLA', N'     31', N'Modelo', 206, N'RESM1979', CAST(N'2019-10-17T11:33:14.397' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'BLA', N'     33', N'Modelo', 207, N'RESM1979', CAST(N'2019-10-17T11:33:14.400' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'BVE', N'     46', N'Modelo', 179, N'RESM1979', CAST(N'2019-10-17T11:33:14.333' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'BVE', N'     48', N'Modelo', 38, N'RESM1979', CAST(N'2019-10-17T11:33:14.063' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'BVE', N'     49', N'Modelo', 180, N'RESM1979', CAST(N'2019-10-17T11:33:14.337' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'BVE', N'     50', N'Modelo', 181, N'RESM1979', CAST(N'2019-10-17T11:33:14.340' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'BVE', N'     51', N'Modelo', 182, N'RESM1979', CAST(N'2019-10-17T11:33:14.340' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'BVE', N'     52', N'Modelo', 183, N'RESM1979', CAST(N'2019-10-17T11:33:14.340' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'BVE', N'     62', N'Modelo', 39, N'RESM1979', CAST(N'2019-10-17T11:33:14.067' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'BVE', N'     63', N'Modelo', 40, N'RESM1979', CAST(N'2019-10-17T11:33:14.067' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'CAR', N'    284', N'Modelo', 307, N'RESM1979', CAST(N'2019-10-17T11:33:14.577' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'CFT', N'      7', N'Modelo', 58, N'RESM1979', CAST(N'2019-10-17T11:33:14.090' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'CFT', N'      8', N'Modelo', 271, N'RESM1979', CAST(N'2019-10-17T11:33:14.517' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'CFT', N'      9', N'Modelo', 272, N'RESM1979', CAST(N'2019-10-17T11:33:14.517' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'CFT', N'     10', N'Modelo', 59, N'RESM1979', CAST(N'2019-10-17T11:33:14.090' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'CFT', N'     19', N'Modelo', 225, N'RESM1979', CAST(N'2019-10-17T11:33:14.440' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'CFT', N'     20', N'Modelo', 76, N'RESM1979', CAST(N'2019-10-17T11:33:14.110' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'CFT', N'     21', N'Modelo', 77, N'RESM1979', CAST(N'2019-10-17T11:33:14.113' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'CFT', N'     22', N'Modelo', 301, N'RESM1979', CAST(N'2019-10-17T11:33:14.563' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'CTA', N'    619', N'Modelo', 184, N'RESM1979', CAST(N'2019-10-17T11:33:14.343' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ENS', N'      7', N'Modelo', 122, N'RESM1979', CAST(N'2019-10-17T11:33:14.190' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'EXL', N'     16', N'Modelo', 155, N'RESM1979', CAST(N'2019-10-17T11:33:14.270' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'EXU', N'     19', N'Modelo', 139, N'RESM1979', CAST(N'2019-10-17T11:33:14.233' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'EXU', N'     67', N'Modelo', 296, N'RESM1979', CAST(N'2019-10-17T11:33:14.557' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'EXU', N'     68', N'Modelo', 297, N'RESM1979', CAST(N'2019-10-17T11:33:14.560' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'GOS', N'    561', N'Modelo', 226, N'RESM1979', CAST(N'2019-10-17T11:33:14.440' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'GOS', N'    608', N'Modelo', 208, N'RESM1979', CAST(N'2019-10-17T11:33:14.400' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'GOS', N'    633', N'Modelo', 18, N'RESM1979', CAST(N'2019-10-17T11:33:14.040' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'GOS', N'    634', N'Modelo', 19, N'RESM1979', CAST(N'2019-10-17T11:33:14.043' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'GOS', N'    635', N'Modelo', 20, N'RESM1979', CAST(N'2019-10-17T11:33:14.043' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'GOS', N'    695', N'Modelo', 209, N'RESM1979', CAST(N'2019-10-17T11:33:14.400' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'HKY', N'     22', N'Modelo', 294, N'RESM1979', CAST(N'2019-10-17T11:33:14.553' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'HKY', N'     24', N'Modelo', 295, N'RESM1979', CAST(N'2019-10-17T11:33:14.553' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'HKY', N'     29', N'Modelo', 2, N'RESM1979', CAST(N'2019-10-17T11:33:14.023' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'HKY', N'     32', N'Modelo', 3, N'RESM1979', CAST(N'2019-10-17T11:33:14.023' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'INS', N'     51', N'Modelo', 140, N'RESM1979', CAST(N'2019-10-17T11:33:14.233' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'INS', N'     86', N'Modelo', 21, N'RESM1979', CAST(N'2019-10-17T11:33:14.043' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'INS', N'     91', N'Modelo', 141, N'RESM1979', CAST(N'2019-10-17T11:33:14.240' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'INS', N'    111', N'Modelo', 262, N'RESM1979', CAST(N'2019-10-17T11:33:14.500' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ISH', N'     26', N'Modelo', 132, N'RESM1979', CAST(N'2019-10-17T11:33:14.217' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'JAK', N'     17', N'Modelo', 185, N'RESM1979', CAST(N'2019-10-17T11:33:14.343' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'JAK', N'     18', N'Modelo', 186, N'RESM1979', CAST(N'2019-10-17T11:33:14.347' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'JAK', N'     39', N'Modelo', 227, N'RESM1979', CAST(N'2019-10-17T11:33:14.443' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'JAK', N'     45', N'Modelo', 60, N'RESM1979', CAST(N'2019-10-17T11:33:14.090' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'JAK', N'     46', N'Modelo', 210, N'RESM1979', CAST(N'2019-10-17T11:33:14.403' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'KLI', N'      1', N'Modelo', 9, N'RESM1979', CAST(N'2019-10-17T11:33:14.030' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'KOA', N'      2', N'Modelo', 78, N'RESM1979', CAST(N'2019-10-17T11:33:14.113' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'KOA', N'      4', N'Modelo', 79, N'RESM1979', CAST(N'2019-10-17T11:33:14.113' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'LDL', N'      7', N'Modelo', 228, N'RESM1979', CAST(N'2019-10-17T11:33:14.443' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'LDL', N'     17', N'Modelo', 80, N'RESM1979', CAST(N'2019-10-17T11:33:14.117' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'LDL', N'     18', N'Modelo', 314, N'RESM1979', CAST(N'2019-10-17T11:33:14.587' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'LDL', N'     19', N'Modelo', 81, N'RESM1979', CAST(N'2019-10-17T11:33:14.117' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'LOV', N'      6', N'Modelo', 142, N'RESM1979', CAST(N'2019-10-17T11:33:14.240' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MAE', N'      4', N'Modelo', 130, N'RESM1979', CAST(N'2019-10-17T11:33:14.213' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MAE', N'      7', N'Modelo', 1, N'RESM1979', CAST(N'2019-10-17T11:33:14.020' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MCQ', N'      1', N'Modelo', 111, N'RESM1979', CAST(N'2019-10-17T11:33:14.163' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MCQ', N'      2', N'Modelo', 242, N'RESM1979', CAST(N'2019-10-17T11:33:14.467' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MCQ', N'      4', N'Modelo', 170, N'RESM1979', CAST(N'2019-10-17T11:33:14.310' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MCQ', N'      5', N'Modelo', 171, N'RESM1979', CAST(N'2019-10-17T11:33:14.313' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MCQ', N'     11', N'Modelo', 264, N'RESM1979', CAST(N'2019-10-17T11:33:14.503' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MCQ', N'     12', N'Modelo', 280, N'RESM1979', CAST(N'2019-10-17T11:33:14.530' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MCQ', N'     13', N'Modelo', 102, N'RESM1979', CAST(N'2019-10-17T11:33:14.143' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MCQ', N'     14', N'Modelo', 243, N'RESM1979', CAST(N'2019-10-17T11:33:14.470' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MCQ', N'     15', N'Modelo', 103, N'RESM1979', CAST(N'2019-10-17T11:33:14.147' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MCQ', N'     16', N'Modelo', 244, N'RESM1979', CAST(N'2019-10-17T11:33:14.470' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MLA', N'     21', N'Modelo', 10, N'RESM1979', CAST(N'2019-10-17T11:33:14.030' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MLA', N'     78', N'Modelo', 133, N'RESM1979', CAST(N'2019-10-17T11:33:14.220' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MLA', N'    186', N'Modelo', 134, N'RESM1979', CAST(N'2019-10-17T11:33:14.220' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MLA', N'    189', N'Modelo', 135, N'RESM1979', CAST(N'2019-10-17T11:33:14.220' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MLO', N'      1', N'Modelo', 143, N'RESM1979', CAST(N'2019-10-17T11:33:14.240' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MLO', N'      5', N'Modelo', 144, N'RESM1979', CAST(N'2019-10-17T11:33:14.243' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MLO', N'      7', N'Modelo', 302, N'RESM1979', CAST(N'2019-10-17T11:33:14.567' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MLO', N'      8', N'Modelo', 22, N'RESM1979', CAST(N'2019-10-17T11:33:14.047' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MLO', N'      9', N'Modelo', 23, N'RESM1979', CAST(N'2019-10-17T11:33:14.047' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MLO', N'     10', N'Modelo', 24, N'RESM1979', CAST(N'2019-10-17T11:33:14.047' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MLO', N'     11', N'Modelo', 25, N'RESM1979', CAST(N'2019-10-17T11:33:14.050' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MLO', N'     12', N'Modelo', 145, N'RESM1979', CAST(N'2019-10-17T11:33:14.247' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNB', N'    136', N'Modelo', 123, N'RESM1979', CAST(N'2019-10-17T11:33:14.193' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNP', N'    155', N'Modelo', 61, N'RESM1979', CAST(N'2019-10-17T11:33:14.090' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNP', N'    159', N'Modelo', 196, N'RESM1979', CAST(N'2019-10-17T11:33:14.377' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNP', N'    165', N'Modelo', 324, N'RESM1979', CAST(N'2019-10-17T11:33:14.603' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNP', N'    170', N'Modelo', 211, N'RESM1979', CAST(N'2019-10-17T11:33:14.410' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNP', N'    171', N'Modelo', 212, N'RESM1979', CAST(N'2019-10-17T11:33:14.413' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNP', N'    172', N'Modelo', 213, N'RESM1979', CAST(N'2019-10-17T11:33:14.413' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNP', N'    175', N'Modelo', 315, N'RESM1979', CAST(N'2019-10-17T11:33:14.590' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNP', N'    178', N'Modelo', 32, N'RESM1979', CAST(N'2019-10-17T11:33:14.057' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNP', N'    179', N'Modelo', 172, N'RESM1979', CAST(N'2019-10-17T11:33:14.317' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNP', N'    180', N'Modelo', 33, N'RESM1979', CAST(N'2019-10-17T11:33:14.060' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNP', N'    181', N'Modelo', 34, N'RESM1979', CAST(N'2019-10-17T11:33:14.060' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNP', N'    182', N'Modelo', 35, N'RESM1979', CAST(N'2019-10-17T11:33:14.060' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNP', N'    183', N'Modelo', 36, N'RESM1979', CAST(N'2019-10-17T11:33:14.060' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNP', N'    184', N'Modelo', 37, N'RESM1979', CAST(N'2019-10-17T11:33:14.063' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MNP', N'    185', N'Modelo', 29, N'RESM1979', CAST(N'2019-10-17T11:33:14.053' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MON', N'      8', N'Modelo', 310, N'RESM1979', CAST(N'2019-10-17T11:33:14.580' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MON', N'      9', N'Modelo', 214, N'RESM1979', CAST(N'2019-10-17T11:33:14.417' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MON', N'     26', N'Modelo', 137, N'RESM1979', CAST(N'2019-10-17T11:33:14.223' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MON', N'     27', N'Modelo', 62, N'RESM1979', CAST(N'2019-10-17T11:33:14.093' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'MON', N'     31', N'Modelo', 259, N'RESM1979', CAST(N'2019-10-17T11:33:14.493' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1082', N'Modelo', 188, N'RESM1979', CAST(N'2019-10-17T11:33:14.357' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1084', N'Modelo', 251, N'RESM1979', CAST(N'2019-10-17T11:33:14.480' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1085', N'Modelo', 252, N'RESM1979', CAST(N'2019-10-17T11:33:14.483' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1103', N'Modelo', 253, N'RESM1979', CAST(N'2019-10-17T11:33:14.483' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1127', N'Modelo', 63, N'RESM1979', CAST(N'2019-10-17T11:33:14.093' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1129', N'Modelo', 215, N'RESM1979', CAST(N'2019-10-17T11:33:14.420' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1131', N'Modelo', 197, N'RESM1979', CAST(N'2019-10-17T11:33:14.380' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1136', N'Modelo', 64, N'RESM1979', CAST(N'2019-10-17T11:33:14.097' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1137', N'Modelo', 116, N'RESM1979', CAST(N'2019-10-17T11:33:14.183' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1141', N'Modelo', 110, N'RESM1979', CAST(N'2019-10-17T11:33:14.160' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1149', N'Modelo', 325, N'RESM1979', CAST(N'2019-10-17T11:33:14.607' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1150', N'Modelo', 326, N'RESM1979', CAST(N'2019-10-17T11:33:14.610' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1153', N'Modelo', 327, N'RESM1979', CAST(N'2019-10-17T11:33:14.610' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1154', N'Modelo', 288, N'RESM1979', CAST(N'2019-10-17T11:33:14.543' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1158', N'Modelo', 65, N'RESM1979', CAST(N'2019-10-17T11:33:14.097' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NIN', N'   1159', N'Modelo', 311, N'RESM1979', CAST(N'2019-10-17T11:33:14.583' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NLK', N'      6', N'Modelo', 303, N'RESM1979', CAST(N'2019-10-17T11:33:14.570' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'NLK', N'     20', N'Modelo', 156, N'RESM1979', CAST(N'2019-10-17T11:33:14.273' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'OZO', N'   2323', N'Modelo', 146, N'RESM1979', CAST(N'2019-10-17T11:33:14.247' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'OZO', N'   2349', N'Modelo', 124, N'RESM1979', CAST(N'2019-10-17T11:33:14.193' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'OZO', N'   2354', N'Modelo', 125, N'RESM1979', CAST(N'2019-10-17T11:33:14.197' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'OZO', N'   2373', N'Modelo', 190, N'RESM1979', CAST(N'2019-10-17T11:33:14.360' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'OZO', N'   2383', N'Modelo', 147, N'RESM1979', CAST(N'2019-10-17T11:33:14.250' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'OZO', N'   2426', N'Modelo', 298, N'RESM1979', CAST(N'2019-10-17T11:33:14.560' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'OZO', N'   2428', N'Modelo', 7, N'RESM1979', CAST(N'2019-10-17T11:33:14.030' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'OZO', N'   2429', N'Modelo', 258, N'RESM1979', CAST(N'2019-10-17T11:33:14.493' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'OZO', N'   2510', N'Modelo', 261, N'RESM1979', CAST(N'2019-10-17T11:33:14.497' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'OZO', N'   2514', N'Modelo', 260, N'RESM1979', CAST(N'2019-10-17T11:33:14.497' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'OZO', N'   2546', N'Modelo', 26, N'RESM1979', CAST(N'2019-10-17T11:33:14.050' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'OZO', N'   2547', N'Modelo', 27, N'RESM1979', CAST(N'2019-10-17T11:33:14.050' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'PAT', N'     86', N'Modelo', 229, N'RESM1979', CAST(N'2019-10-17T11:33:14.447' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'PAU', N'    609', N'Modelo', 157, N'RESM1979', CAST(N'2019-10-17T11:33:14.277' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'PAU', N'    610', N'Modelo', 158, N'RESM1979', CAST(N'2019-10-17T11:33:14.280' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'RIL', N'     23', N'Modelo', 266, N'RESM1979', CAST(N'2019-10-17T11:33:14.507' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'RIL', N'     31', N'Modelo', 55, N'RESM1979', CAST(N'2019-10-17T11:33:14.083' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'RIL', N'     33', N'Modelo', 267, N'RESM1979', CAST(N'2019-10-17T11:33:14.507' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'RIL', N'     34', N'Modelo', 198, N'RESM1979', CAST(N'2019-10-17T11:33:14.380' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'RIL', N'     35', N'Modelo', 56, N'RESM1979', CAST(N'2019-10-17T11:33:14.087' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'RIL', N'     36', N'Modelo', 199, N'RESM1979', CAST(N'2019-10-17T11:33:14.380' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'RIL', N'     37', N'Modelo', 57, N'RESM1979', CAST(N'2019-10-17T11:33:14.087' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SKO', N'     14', N'Modelo', 112, N'RESM1979', CAST(N'2019-10-17T11:33:14.167' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SKO', N'     15', N'Modelo', 113, N'RESM1979', CAST(N'2019-10-17T11:33:14.170' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SKO', N'     16', N'Modelo', 49, N'RESM1979', CAST(N'2019-10-17T11:33:14.077' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SKO', N'     17', N'Modelo', 191, N'RESM1979', CAST(N'2019-10-17T11:33:14.363' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SKO', N'     18', N'Modelo', 50, N'RESM1979', CAST(N'2019-10-17T11:33:14.080' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SKO', N'     19', N'Modelo', 51, N'RESM1979', CAST(N'2019-10-17T11:33:14.080' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SKO', N'     20', N'Modelo', 114, N'RESM1979', CAST(N'2019-10-17T11:33:14.173' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SKO', N'     21', N'Modelo', 115, N'RESM1979', CAST(N'2019-10-17T11:33:14.180' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SRF', N'     26', N'Modelo', 119, N'RESM1979', CAST(N'2019-10-17T11:33:14.187' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SRF', N'     27', N'Modelo', 120, N'RESM1979', CAST(N'2019-10-17T11:33:14.190' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SRF', N'     29', N'Modelo', 121, N'RESM1979', CAST(N'2019-10-17T11:33:14.190' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SRF', N'     33', N'Modelo', 160, N'RESM1979', CAST(N'2019-10-17T11:33:14.290' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SRF', N'     34', N'Modelo', 161, N'RESM1979', CAST(N'2019-10-17T11:33:14.290' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SRF', N'     35', N'Modelo', 162, N'RESM1979', CAST(N'2019-10-17T11:33:14.290' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SRF', N'     80', N'Modelo', 148, N'RESM1979', CAST(N'2019-10-17T11:33:14.253' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'SRF', N'     86', N'Modelo', 149, N'RESM1979', CAST(N'2019-10-17T11:33:14.260' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'STS', N'     98', N'Modelo', 254, N'RESM1979', CAST(N'2019-10-17T11:33:14.487' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'STS', N'    150', N'Modelo', 322, N'RESM1979', CAST(N'2019-10-17T11:33:14.600' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'STS', N'    153', N'Modelo', 255, N'RESM1979', CAST(N'2019-10-17T11:33:14.487' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'STS', N'    154', N'Modelo', 126, N'RESM1979', CAST(N'2019-10-17T11:33:14.200' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'STS', N'    167', N'Modelo', 189, N'RESM1979', CAST(N'2019-10-17T11:33:14.360' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'STS', N'    170', N'Modelo', 256, N'RESM1979', CAST(N'2019-10-17T11:33:14.490' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'STS', N'    171', N'Modelo', 293, N'RESM1979', CAST(N'2019-10-17T11:33:14.550' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'T60', N'      8', N'Modelo', 136, N'RESM1979', CAST(N'2019-10-17T11:33:14.223' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'T60', N'     13', N'Modelo', 150, N'RESM1979', CAST(N'2019-10-17T11:33:14.260' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TNC', N'     44', N'Modelo', 163, N'RESM1979', CAST(N'2019-10-17T11:33:14.293' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TNS', N'      1', N'Modelo', 216, N'RESM1979', CAST(N'2019-10-17T11:33:14.423' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TNS', N'      2', N'Modelo', 312, N'RESM1979', CAST(N'2019-10-17T11:33:14.583' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TNS', N'      3', N'Modelo', 66, N'RESM1979', CAST(N'2019-10-17T11:33:14.100' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TNS', N'      4', N'Modelo', 304, N'RESM1979', CAST(N'2019-10-17T11:33:14.570' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TNS', N'      5', N'Modelo', 164, N'RESM1979', CAST(N'2019-10-17T11:33:14.293' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TNS', N'      6', N'Modelo', 30, N'RESM1979', CAST(N'2019-10-17T11:33:14.053' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TNS', N'      7', N'Modelo', 31, N'RESM1979', CAST(N'2019-10-17T11:33:14.057' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1619', N'Modelo', 151, N'RESM1979', CAST(N'2019-10-17T11:33:14.263' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1626', N'Modelo', 127, N'RESM1979', CAST(N'2019-10-17T11:33:14.203' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1627', N'Modelo', 128, N'RESM1979', CAST(N'2019-10-17T11:33:14.210' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1628', N'Modelo', 129, N'RESM1979', CAST(N'2019-10-17T11:33:14.213' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1631', N'Modelo', 152, N'RESM1979', CAST(N'2019-10-17T11:33:14.263' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1633', N'Modelo', 153, N'RESM1979', CAST(N'2019-10-17T11:33:14.267' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1634', N'Modelo', 154, N'RESM1979', CAST(N'2019-10-17T11:33:14.270' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1638', N'Modelo', 230, N'RESM1979', CAST(N'2019-10-17T11:33:14.447' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1639', N'Modelo', 231, N'RESM1979', CAST(N'2019-10-17T11:33:14.450' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1643', N'Modelo', 245, N'RESM1979', CAST(N'2019-10-17T11:33:14.470' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1649', N'Modelo', 11, N'RESM1979', CAST(N'2019-10-17T11:33:14.033' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1650', N'Modelo', 12, N'RESM1979', CAST(N'2019-10-17T11:33:14.033' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1651', N'Modelo', 289, N'RESM1979', CAST(N'2019-10-17T11:33:14.547' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1652', N'Modelo', 328, N'RESM1979', CAST(N'2019-10-17T11:33:14.610' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1653', N'Modelo', 117, N'RESM1979', CAST(N'2019-10-17T11:33:14.183' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1654', N'Modelo', 290, N'RESM1979', CAST(N'2019-10-17T11:33:14.547' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1655', N'Modelo', 291, N'RESM1979', CAST(N'2019-10-17T11:33:14.550' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1656', N'Modelo', 292, N'RESM1979', CAST(N'2019-10-17T11:33:14.550' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1658', N'Modelo', 67, N'RESM1979', CAST(N'2019-10-17T11:33:14.100' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1660', N'Modelo', 68, N'RESM1979', CAST(N'2019-10-17T11:33:14.100' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1665', N'Modelo', 69, N'RESM1979', CAST(N'2019-10-17T11:33:14.100' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOK', N'   1667', N'Modelo', 70, N'RESM1979', CAST(N'2019-10-17T11:33:14.103' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOP', N'     75', N'Modelo', 299, N'RESM1979', CAST(N'2019-10-17T11:33:14.560' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOP', N'     79', N'Modelo', 300, N'RESM1979', CAST(N'2019-10-17T11:33:14.563' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOP', N'     80', N'Modelo', 8, N'RESM1979', CAST(N'2019-10-17T11:33:14.030' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOP', N'     89', N'Modelo', 13, N'RESM1979', CAST(N'2019-10-17T11:33:14.037' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOP', N'     92', N'Modelo', 316, N'RESM1979', CAST(N'2019-10-17T11:33:14.590' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOP', N'     93', N'Modelo', 317, N'RESM1979', CAST(N'2019-10-17T11:33:14.593' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOP', N'    106', N'Modelo', 159, N'RESM1979', CAST(N'2019-10-17T11:33:14.287' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOP', N'    107', N'Modelo', 28, N'RESM1979', CAST(N'2019-10-17T11:33:14.053' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOP', N'    111', N'Modelo', 82, N'RESM1979', CAST(N'2019-10-17T11:33:14.120' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TOP', N'    112', N'Modelo', 83, N'RESM1979', CAST(N'2019-10-17T11:33:14.120' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    550', N'Modelo', 232, N'RESM1979', CAST(N'2019-10-17T11:33:14.450' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    572', N'Modelo', 233, N'RESM1979', CAST(N'2019-10-17T11:33:14.453' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    589', N'Modelo', 192, N'RESM1979', CAST(N'2019-10-17T11:33:14.367' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    590', N'Modelo', 193, N'RESM1979', CAST(N'2019-10-17T11:33:14.367' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    591', N'Modelo', 194, N'RESM1979', CAST(N'2019-10-17T11:33:14.370' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    592', N'Modelo', 265, N'RESM1979', CAST(N'2019-10-17T11:33:14.503' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    596', N'Modelo', 195, N'RESM1979', CAST(N'2019-10-17T11:33:14.370' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    597', N'Modelo', 173, N'RESM1979', CAST(N'2019-10-17T11:33:14.317' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    601', N'Modelo', 174, N'RESM1979', CAST(N'2019-10-17T11:33:14.320' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    602', N'Modelo', 175, N'RESM1979', CAST(N'2019-10-17T11:33:14.320' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    607', N'Modelo', 176, N'RESM1979', CAST(N'2019-10-17T11:33:14.323' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    608', N'Modelo', 177, N'RESM1979', CAST(N'2019-10-17T11:33:14.327' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    619', N'Modelo', 165, N'RESM1979', CAST(N'2019-10-17T11:33:14.297' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    620', N'Modelo', 166, N'RESM1979', CAST(N'2019-10-17T11:33:14.297' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    628', N'Modelo', 217, N'RESM1979', CAST(N'2019-10-17T11:33:14.423' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    632', N'Modelo', 218, N'RESM1979', CAST(N'2019-10-17T11:33:14.430' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    639', N'Modelo', 274, N'RESM1979', CAST(N'2019-10-17T11:33:14.520' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    640', N'Modelo', 234, N'RESM1979', CAST(N'2019-10-17T11:33:14.453' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    645', N'Modelo', 235, N'RESM1979', CAST(N'2019-10-17T11:33:14.457' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    705', N'Modelo', 71, N'RESM1979', CAST(N'2019-10-17T11:33:14.103' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    713', N'Modelo', 219, N'RESM1979', CAST(N'2019-10-17T11:33:14.430' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    714', N'Modelo', 220, N'RESM1979', CAST(N'2019-10-17T11:33:14.433' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    715', N'Modelo', 221, N'RESM1979', CAST(N'2019-10-17T11:33:14.433' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    716', N'Modelo', 222, N'RESM1979', CAST(N'2019-10-17T11:33:14.437' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    718', N'Modelo', 236, N'RESM1979', CAST(N'2019-10-17T11:33:14.457' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    722', N'Modelo', 237, N'RESM1979', CAST(N'2019-10-17T11:33:14.460' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    723', N'Modelo', 238, N'RESM1979', CAST(N'2019-10-17T11:33:14.460' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    725', N'Modelo', 84, N'RESM1979', CAST(N'2019-10-17T11:33:14.120' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    726', N'Modelo', 318, N'RESM1979', CAST(N'2019-10-17T11:33:14.593' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    727', N'Modelo', 85, N'RESM1979', CAST(N'2019-10-17T11:33:14.123' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    728', N'Modelo', 319, N'RESM1979', CAST(N'2019-10-17T11:33:14.597' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    734', N'Modelo', 223, N'RESM1979', CAST(N'2019-10-17T11:33:14.437' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    740', N'Modelo', 313, N'RESM1979', CAST(N'2019-10-17T11:33:14.587' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    741', N'Modelo', 273, N'RESM1979', CAST(N'2019-10-17T11:33:14.520' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    742', N'Modelo', 275, N'RESM1979', CAST(N'2019-10-17T11:33:14.520' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    744', N'Modelo', 86, N'RESM1979', CAST(N'2019-10-17T11:33:14.123' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    745', N'Modelo', 87, N'RESM1979', CAST(N'2019-10-17T11:33:14.127' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    747', N'Modelo', 276, N'RESM1979', CAST(N'2019-10-17T11:33:14.523' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    749', N'Modelo', 239, N'RESM1979', CAST(N'2019-10-17T11:33:14.463' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    750', N'Modelo', 88, N'RESM1979', CAST(N'2019-10-17T11:33:14.127' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    751', N'Modelo', 89, N'RESM1979', CAST(N'2019-10-17T11:33:14.130' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    762', N'Modelo', 178, N'RESM1979', CAST(N'2019-10-17T11:33:14.330' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    763', N'Modelo', 305, N'RESM1979', CAST(N'2019-10-17T11:33:14.570' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    769', N'Modelo', 224, N'RESM1979', CAST(N'2019-10-17T11:33:14.440' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    780', N'Modelo', 52, N'RESM1979', CAST(N'2019-10-17T11:33:14.080' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    781', N'Modelo', 306, N'RESM1979', CAST(N'2019-10-17T11:33:14.573' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    782', N'Modelo', 53, N'RESM1979', CAST(N'2019-10-17T11:33:14.083' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    783', N'Modelo', 54, N'RESM1979', CAST(N'2019-10-17T11:33:14.083' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    784', N'Modelo', 72, N'RESM1979', CAST(N'2019-10-17T11:33:14.107' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    785', N'Modelo', 41, N'RESM1979', CAST(N'2019-10-17T11:33:14.067' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    786', N'Modelo', 187, N'RESM1979', CAST(N'2019-10-17T11:33:14.353' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    787', N'Modelo', 42, N'RESM1979', CAST(N'2019-10-17T11:33:14.070' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    788', N'Modelo', 43, N'RESM1979', CAST(N'2019-10-17T11:33:14.070' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    789', N'Modelo', 44, N'RESM1979', CAST(N'2019-10-17T11:33:14.070' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    790', N'Modelo', 90, N'RESM1979', CAST(N'2019-10-17T11:33:14.130' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    791', N'Modelo', 91, N'RESM1979', CAST(N'2019-10-17T11:33:14.130' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    804', N'Modelo', 308, N'RESM1979', CAST(N'2019-10-17T11:33:14.577' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    805', N'Modelo', 268, N'RESM1979', CAST(N'2019-10-17T11:33:14.510' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    816', N'Modelo', 269, N'RESM1979', CAST(N'2019-10-17T11:33:14.510' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    837', N'Modelo', 277, N'RESM1979', CAST(N'2019-10-17T11:33:14.527' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    838', N'Modelo', 92, N'RESM1979', CAST(N'2019-10-17T11:33:14.130' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    839', N'Modelo', 93, N'RESM1979', CAST(N'2019-10-17T11:33:14.133' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    840', N'Modelo', 240, N'RESM1979', CAST(N'2019-10-17T11:33:14.463' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    842', N'Modelo', 94, N'RESM1979', CAST(N'2019-10-17T11:33:14.133' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    843', N'Modelo', 95, N'RESM1979', CAST(N'2019-10-17T11:33:14.137' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    844', N'Modelo', 278, N'RESM1979', CAST(N'2019-10-17T11:33:14.527' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    848', N'Modelo', 96, N'RESM1979', CAST(N'2019-10-17T11:33:14.137' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    861', N'Modelo', 73, N'RESM1979', CAST(N'2019-10-17T11:33:14.107' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TRO', N'    862', N'Modelo', 74, N'RESM1979', CAST(N'2019-10-17T11:33:14.110' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TVA', N'      7', N'Modelo', 4, N'RESM1979', CAST(N'2019-10-17T11:33:14.023' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TVA', N'      8', N'Modelo', 131, N'RESM1979', CAST(N'2019-10-17T11:33:14.217' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TVA', N'      9', N'Modelo', 5, N'RESM1979', CAST(N'2019-10-17T11:33:14.027' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TVA', N'     10', N'Modelo', 6, N'RESM1979', CAST(N'2019-10-17T11:33:14.027' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'TYP', N'      4', N'Modelo', 257, N'RESM1979', CAST(N'2019-10-17T11:33:14.490' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'VCY', N'      1', N'Modelo', 97, N'RESM1979', CAST(N'2019-10-17T11:33:14.137' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'VCY', N'      2', N'Modelo', 98, N'RESM1979', CAST(N'2019-10-17T11:33:14.140' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'VCY', N'      3', N'Modelo', 99, N'RESM1979', CAST(N'2019-10-17T11:33:14.140' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'VCY', N'      4', N'Modelo', 320, N'RESM1979', CAST(N'2019-10-17T11:33:14.597' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'VCY', N'      5', N'Modelo', 100, N'RESM1979', CAST(N'2019-10-17T11:33:14.143' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'VCY', N'      6', N'Modelo', 321, N'RESM1979', CAST(N'2019-10-17T11:33:14.600' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'VCY', N'      7', N'Modelo', 101, N'RESM1979', CAST(N'2019-10-17T11:33:14.143' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'WPL', N'     19', N'Modelo', 75, N'RESM1979', CAST(N'2019-10-17T11:33:14.110' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'WPL', N'     20', N'Modelo', 14, N'RESM1979', CAST(N'2019-10-17T11:33:14.037' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'WPL', N'     21', N'Modelo', 15, N'RESM1979', CAST(N'2019-10-17T11:33:14.037' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'WPL', N'     22', N'Modelo', 16, N'RESM1979', CAST(N'2019-10-17T11:33:14.040' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'WPL', N'     23', N'Modelo', 17, N'RESM1979', CAST(N'2019-10-17T11:33:14.040' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'WPL', N'     26', N'Modelo', 138, N'RESM1979', CAST(N'2019-10-17T11:33:14.227' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'WPL', N'     27', N'Modelo', 118, N'RESM1979', CAST(N'2019-10-17T11:33:14.187' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YON', N'     21', N'Modelo', 241, N'RESM1979', CAST(N'2019-10-17T11:33:14.467' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YON', N'     28', N'Modelo', 246, N'RESM1979', CAST(N'2019-10-17T11:33:14.473' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YON', N'     42', N'Modelo', 247, N'RESM1979', CAST(N'2019-10-17T11:33:14.477' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YON', N'     44', N'Modelo', 167, N'RESM1979', CAST(N'2019-10-17T11:33:14.300' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YON', N'     45', N'Modelo', 168, N'RESM1979', CAST(N'2019-10-17T11:33:14.300' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YON', N'     48', N'Modelo', 200, N'RESM1979', CAST(N'2019-10-17T11:33:14.387' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YON', N'     53', N'Modelo', 201, N'RESM1979', CAST(N'2019-10-17T11:33:14.387' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YON', N'     54', N'Modelo', 202, N'RESM1979', CAST(N'2019-10-17T11:33:14.390' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YON', N'     55', N'Modelo', 203, N'RESM1979', CAST(N'2019-10-17T11:33:14.390' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YON', N'     64', N'Modelo', 263, N'RESM1979', CAST(N'2019-10-17T11:33:14.500' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YON', N'     68', N'Modelo', 169, N'RESM1979', CAST(N'2019-10-17T11:33:14.303' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YON', N'     70', N'Modelo', 104, N'RESM1979', CAST(N'2019-10-17T11:33:14.147' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YON', N'     71', N'Modelo', 105, N'RESM1979', CAST(N'2019-10-17T11:33:14.150' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YUY', N'    155', N'Modelo', 248, N'RESM1979', CAST(N'2019-10-17T11:33:14.477' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YUY', N'    157', N'Modelo', 281, N'RESM1979', CAST(N'2019-10-17T11:33:14.530' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YUY', N'    159', N'Modelo', 249, N'RESM1979', CAST(N'2019-10-17T11:33:14.480' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YUY', N'    160', N'Modelo', 282, N'RESM1979', CAST(N'2019-10-17T11:33:14.533' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YUY', N'    165', N'Modelo', 283, N'RESM1979', CAST(N'2019-10-17T11:33:14.533' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YUY', N'    198', N'Modelo', 250, N'RESM1979', CAST(N'2019-10-17T11:33:14.480' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'YUY', N'    199', N'Modelo', 204, N'RESM1979', CAST(N'2019-10-17T11:33:14.390' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'ZLA', N'     10', N'Modelo', 279, N'RESM1979', CAST(N'2019-10-17T11:33:14.530' AS DateTime))
--GO
INSERT [dbo].[promociones] ([idpromocion], [nombre], [fecha], [tipo], [estatus], [iniciopromo], [finpromo], [preciero], [senalizador], [clasificacion], [imagen], [minunicompra], [minimpcompra], [unipromo], [acumulable], [paresunicos], [clinoregis], [idusuariocaptura], [fumcaptura], [idusuarioaplica], [fumaplica], [idusuariopausa], [fumpausa], [idusuariocancela], [fumcancela], [idusuario], [fum], [promosrequeridas], [duplicados], [clienterequerido]) VALUES (3, N'PROMOCION 149 CONT/ 259 CREDIT', CAST(N'2019-10-17' AS Date), N'DIRECTA', N'ACTIVO', CAST(N'2019-10-17T00:00:00.000' AS DateTime), CAST(N'2020-02-28T00:00:00.000' AS DateTime), N'AZUL', N'BLANCO', N'REBAJA', null, 1, CAST(0.00 AS Numeric(18, 2)), 0, N'NO', N'SI', N'SI', N'RESM1979', CAST(N'2019-10-17T11:44:15.237' AS DateTime), N'RESM1979', CAST(N'2019-10-17T12:11:05.983' AS DateTime), NULL, NULL, NULL, NULL, N'RESM1979', CAST(N'2019-10-17T12:11:05.983' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[promocionesagrupaciones] ([idpromocion], [idagrupacioncompra], [idagrupacionpromo], [renglon], [idusuario], [fum]) VALUES (3, 0, 1, 1, N'RESM1979', CAST(N'2019-10-17T11:44:23.470' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (3, N'CP', 0, N'PROMO', CAST(259.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.917' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (3, N'CV', 0, N'PROMO', CAST(259.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.917' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (3, N'EF', 0, N'PROMO', CAST(149.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.917' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (3, N'TC', 0, N'PROMO', CAST(149.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:48:42.750' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (3, N'TD', 0, N'PROMO', CAST(149.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.917' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (3, N'VA', 0, N'PROMO', CAST(259.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.917' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (3, N'VD', 0, N'PROMO', CAST(259.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.917' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesplazas] ([idpromocion], [plaza], [sucursal], [idusuario], [fum]) VALUES (3, N'01', N'', N'RESM1979', CAST(N'2019-10-17T12:10:38.820' AS DateTime))
GO

-- promocion 30% desc caballero, min 2, importe 3,500
--INSERT [dbo].[agrupaciones] ([idagrupacion], [nombre], [fecha], [idusuario], [fum]) VALUES (13, N'AGRUPACION CALZADO CABALLERO', CAST(N'2019-12-10' AS Date), N'RESM1979', CAST(N'2019-12-10T12:28:31.520' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (13, 1, 2, 0, 0, 0, 0, 0, 0, 0, 0, N'', N'', N'Departamento', 1, N'RESM1979', CAST(N'2019-12-10T12:28:43.643' AS DateTime))
--GO
INSERT [dbo].[promociones] ([idpromocion], [nombre], [fecha], [tipo], [estatus], [iniciopromo], [finpromo], [preciero], [senalizador], [clasificacion], [imagen], [minunicompra], [minimpcompra], [unipromo], [acumulable], [paresunicos], [clinoregis], [idusuariocaptura], [fumcaptura], [idusuarioaplica], [fumaplica], [idusuariopausa], [fumpausa], [idusuariocancela], [fumcancela], [idusuario], [fum], [promosrequeridas], [duplicados], [clienterequerido]) VALUES (14, N'30% DSCTO CALZADO CABALLERO', CAST(N'2019-12-10' AS Date), N'DIRECTA', N'ACTIVO', CAST(N'2019-12-10T00:00:00.000' AS DateTime), CAST(N'2020-01-31T00:00:00.000' AS DateTime), N'AZUL', N'BLANCO', N'REBAJA', null, 2, CAST(3500.00 AS Numeric(18, 2)), 0, N'NO', N'SI', N'SI', N'RESM1979', CAST(N'2019-12-10T12:28:07.797' AS DateTime), N'RESM1979', CAST(N'2019-12-10T12:37:31.980' AS DateTime), NULL, NULL, NULL, NULL, N'RESM1979', CAST(N'2019-12-10T12:37:31.980' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[promocionesagrupaciones] ([idpromocion], [idagrupacioncompra], [idagrupacionpromo], [renglon], [idusuario], [fum]) VALUES (14, 0, 13, 1, N'RESM1979', CAST(N'2019-12-10T12:29:24.753' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (14, N'TO', 0, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(30.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.917' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesexclusiones] ([idpromocion], [marca], [estilon], [idusuario], [fum]) VALUES (14, N'FFF', N'', N'RESM1979', CAST(N'2019-12-10T12:30:04.183' AS DateTime))
GO
INSERT [dbo].[promocionesplazas] ([idpromocion], [plaza], [sucursal], [idusuario], [fum]) VALUES (14, N'01', N'', N'RESM1979', CAST(N'2019-12-10T12:30:13.793' AS DateTime))
GO

--CHALECO $99 x $1800 CALZADO
--INSERT [dbo].[agrupaciones] ([idagrupacion], [nombre], [fecha], [idusuario], [fum]) VALUES (3, N'CALZADO 20% CREDITO 30% CONTAD', CAST(N'2019-10-17' AS Date), N'RESM1979', CAST(N'2019-10-17T11:09:57.383' AS DateTime))
--GO
--INSERT [dbo].[agrupaciones] ([idagrupacion], [nombre], [fecha], [idusuario], [fum]) VALUES (14, N'AGRUPACION ACCESORIOS', CAST(N'2019-12-27' AS Date), N'RESM1979', CAST(N'2019-12-27T10:17:10.787' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (3, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'', N'', N'División', 1, N'RESM1979', CAST(N'2019-10-17T11:10:19.247' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (14, 2, 8, 0, 0, 0, 0, 0, 0, 0, 0, N'TOY', N'', N'Departamento', 1, N'RESM1979', CAST(N'2019-12-27T10:19:16.083' AS DateTime))
--GO
INSERT [dbo].[promociones] ([idpromocion], [nombre], [fecha], [tipo], [estatus], [iniciopromo], [finpromo], [preciero], [senalizador], [clasificacion], [imagen], [minunicompra], [minimpcompra], [unipromo], [acumulable], [paresunicos], [clinoregis], [idusuariocaptura], [fumcaptura], [idusuarioaplica], [fumaplica], [idusuariopausa], [fumpausa], [idusuariocancela], [fumcancela], [idusuario], [fum], [promosrequeridas], [duplicados], [clienterequerido]) VALUES (20, N'CHALECO $99 x $1800 CALZADO', CAST(N'2019-12-27' AS Date), N'AxB', N'ACTIVO', CAST(N'2019-12-27T00:00:00.000' AS DateTime), CAST(N'2020-02-29T00:00:00.000' AS DateTime), N'AZUL', N'BLANCO', N'REBAJA', null, 1, CAST(1899.00 AS Numeric(18, 2)), 1, N'SI', N'SI', N'SI', N'RESM1979', CAST(N'2019-12-27T10:32:09.857' AS DateTime), N'RESM1979', CAST(N'2019-12-27T14:11:34.260' AS DateTime), NULL, NULL, NULL, NULL, N'RESM1979', CAST(N'2019-12-27T14:11:34.260' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[promocionesagrupaciones] ([idpromocion], [idagrupacioncompra], [idagrupacionpromo], [renglon], [idusuario], [fum]) VALUES (20, 3, 14, 1, N'RESM1979', CAST(N'2019-10-17T12:03:24.117' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (20, N'CP', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.917' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (20, N'CV', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.917' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (20, N'EF', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.917' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (20, N'MD', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.917' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (20, N'TC', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.917' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (20, N'TD', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.917' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (20, N'TO', 2, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(100.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.917' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (20, N'VA', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.917' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (20, N'VD', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.917' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesplazas] ([idpromocion], [plaza], [sucursal], [idusuario], [fum]) VALUES (20, N'01', N'', N'RESM1979', CAST(N'2019-12-10T12:41:36.517' AS DateTime))
GO

--CHALECO $99 x $1800 CALZADO - 120
--INSERT [dbo].[agrupaciones] ([idagrupacion], [nombre], [fecha], [idusuario], [fum]) VALUES (3, N'CALZADO 20% CREDITO 30% CONTAD', CAST(N'2019-10-17' AS Date), N'RESM1979', CAST(N'2019-10-17T11:09:57.383' AS DateTime))
--GO
INSERT [dbo].[agrupaciones] ([idagrupacion], [nombre], [fecha], [idusuario], [fum]) VALUES (16, N'CHALECOS TODOS MODELOS', CAST(N'2019-12-27' AS Date), N'RESM1979', CAST(N'2019-12-27T10:17:10.787' AS DateTime))
GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (3, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'', N'', N'División', 1, N'RESM1979', CAST(N'2019-10-17T11:10:19.247' AS DateTime))
--GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (16, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'CRE', N'      1', N'Modelo', 1, N'RESM1979', CAST(N'2019-10-17T11:33:14.020' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (16, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'CRE', N'      2', N'Modelo', 2, N'RESM1979', CAST(N'2019-10-17T11:33:14.020' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (16, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'CRE', N'      3', N'Modelo', 3, N'RESM1979', CAST(N'2019-10-17T11:33:14.020' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (16, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'CRE', N'      4', N'Modelo', 4, N'RESM1979', CAST(N'2019-10-17T11:33:14.020' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (16, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'CRE', N'      5', N'Modelo', 5, N'RESM1979', CAST(N'2019-10-17T11:33:14.020' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (16, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'CRE', N'      6', N'Modelo', 6, N'RESM1979', CAST(N'2019-10-17T11:33:14.020' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (16, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'CRE', N'      7', N'Modelo', 7, N'RESM1979', CAST(N'2019-10-17T11:33:14.020' AS DateTime))
GO
INSERT [dbo].[promociones] ([idpromocion], [nombre], [fecha], [tipo], [estatus], [iniciopromo], [finpromo], [preciero], [senalizador], [clasificacion], [imagen], [minunicompra], [minimpcompra], [unipromo], [acumulable], [paresunicos], [clinoregis], [idusuariocaptura], [fumcaptura], [idusuarioaplica], [fumaplica], [idusuariopausa], [fumpausa], [idusuariocancela], [fumcancela], [idusuario], [fum], [promosrequeridas], [duplicados], [clienterequerido], [importeticket]) VALUES (120, N'CHALECO $99 x $1800 CALZADO', CAST(N'2019-12-27' AS Date), N'AxB', N'ACTIVO', CAST(N'2019-12-27T00:00:00.000' AS DateTime), CAST(N'2050-03-31T00:00:00.000' AS DateTime), N'AZUL', N'BLANCO', N'REBAJA', null, 0, CAST(1899.00 AS Numeric(18, 2)), 1, N'NO', N'SI', N'SI', N'RESM1979', CAST(N'2019-12-27T10:32:09.857' AS DateTime), N'RESM1979', CAST(N'2019-12-27T14:11:34.260' AS DateTime), NULL, NULL, NULL, NULL, N'RESM1979', CAST(N'2019-12-27T14:11:34.260' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[promocionesagrupaciones] ([idpromocion], [idagrupacioncompra], [idagrupacionpromo], [renglon], [idusuario], [fum]) VALUES (120, 3, 16, 1, N'RESM1979', CAST(N'2019-10-17T12:03:24.117' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (120, N'CP', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.917' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (120, N'CV', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.917' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (120, N'EF', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.917' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (120, N'MD', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.917' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (120, N'TC', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.917' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (120, N'TD', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.917' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (120, N'TO', 2, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(50.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-11-12T11:44:44.490' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (120, N'VD', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.917' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesplazas] ([idpromocion], [plaza], [sucursal], [idusuario], [fum]) VALUES (120, N'01', N'', N'RESM1979', CAST(N'2019-12-10T12:41:36.517' AS DateTime))
GO

--20% DSCTO CALZADO CABALLERO - min compra 3500.00, DIRECTA
--INSERT [dbo].[agrupaciones] ([idagrupacion], [nombre], [fecha], [idusuario], [fum]) VALUES (13, N'AGRUPACION CALZADO CABALLERO', CAST(N'2019-12-10' AS Date), N'RESM1979', CAST(N'2019-12-10T12:28:31.520' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (13, 1, 2, 0, 0, 0, 0, 0, 0, 0, 0, N'', N'', N'Departamento', 1, N'RESM1979', CAST(N'2019-12-10T12:28:43.643' AS DateTime))
--GO
INSERT [dbo].[promociones] ([idpromocion], [nombre], [fecha], [tipo], [estatus], [iniciopromo], [finpromo], [preciero], [senalizador], [clasificacion], [imagen], [minunicompra], [minimpcompra], [unipromo], [acumulable], [paresunicos], [clinoregis], [idusuariocaptura], [fumcaptura], [idusuarioaplica], [fumaplica], [idusuariopausa], [fumpausa], [idusuariocancela], [fumcancela], [idusuario], [fum], [promosrequeridas], [duplicados], [clienterequerido], [importeticket]) VALUES (140, N'20% DSCTO CALZADO CABALLERO', CAST(N'2019-12-10' AS Date), N'DIRECTA', N'ACTIVO', CAST(N'2019-12-10T00:00:00.000' AS DateTime), CAST(N'2020-03-31T00:00:00.000' AS DateTime), N'AZUL', N'BLANCO', N'REBAJA', null, 0, CAST(3500.00 AS Numeric(18, 2)), 0, N'NO', N'SI', N'SI', N'RESM1979', CAST(N'2019-12-10T12:28:07.797' AS DateTime), N'RESM1979', CAST(N'2019-12-10T12:37:31.980' AS DateTime), NULL, NULL, NULL, NULL, N'RESM1979', CAST(N'2019-12-10T12:37:31.980' AS DateTime), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[promocionesagrupaciones] ([idpromocion], [idagrupacioncompra], [idagrupacionpromo], [renglon], [idusuario], [fum]) VALUES (140, 0, 13, 1, N'RESM1979', CAST(N'2019-12-10T12:29:24.753' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (140, N'TO', 0, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(20.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.917' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesexclusiones] ([idpromocion], [marca], [estilon], [idusuario], [fum]) VALUES (140, N'FFF', N'', N'RESM1979', CAST(N'2019-12-10T12:30:04.183' AS DateTime))
GO
INSERT [dbo].[promocionesplazas] ([idpromocion], [plaza], [sucursal], [idusuario], [fum]) VALUES (140, N'01', N'', N'RESM1979', CAST(N'2019-12-10T12:30:13.793' AS DateTime))
GO


--24	3x2 en CALZADO, axb min 2800.00, ticket
INSERT [dbo].[agrupaciones] ([idagrupacion], [nombre], [fecha], [idusuario], [fum]) VALUES (30, N'CALZADO 20% CREDITO 30% CONTAD', CAST(N'2019-10-17' AS Date), N'RESM1979', CAST(N'2019-10-17T11:09:57.383' AS DateTime))
GO
INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (30, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'', N'', N'División', 1, N'RESM1979', CAST(N'2019-10-17T11:10:19.247' AS DateTime))
GO
INSERT [dbo].[promociones] ([idpromocion], [nombre], [fecha], [tipo], [estatus], [iniciopromo], [finpromo], [preciero], [senalizador], [clasificacion], [imagen], [minunicompra], [minimpcompra], [unipromo], [acumulable], [paresunicos], [clinoregis], [idusuariocaptura], [fumcaptura], [idusuarioaplica], [fumaplica], [idusuariopausa], [fumpausa], [idusuariocancela], [fumcancela], [idusuario], [fum], [promosrequeridas], [duplicados], [clienterequerido], [importeticket]) VALUES (24, N'3x2 en CALZADO', CAST(N'2019-12-10' AS Date), N'AxB', N'ACTIVO', CAST(N'2019-12-10T00:00:00.000' AS DateTime), CAST(N'2050-03-03T00:00:00.000' AS DateTime), N'AZUL', N'BLANCO', N'REBAJA', null, 2, CAST(2800.00 AS Numeric(18, 2)), 1, N'NO', N'SI', N'NO', N'RESM1979', CAST(N'2019-12-10T11:00:59.330' AS DateTime), N'RESM1979', CAST(N'2019-12-10T11:26:43.080' AS DateTime), NULL, NULL, N'RESM1979', CAST(N'2019-12-10T12:38:36.387' AS DateTime), N'RESM1979', CAST(N'2019-12-10T12:38:36.387' AS DateTime), NULL, NULL, 1, 1)
GO
INSERT [dbo].[promocionesagrupaciones] ([idpromocion], [idagrupacioncompra], [idagrupacionpromo], [renglon], [idusuario], [fum]) VALUES (24, 30, 30, 1, N'RESM1979', CAST(N'2019-10-17T10:50:09.663' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (24, N'EF', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-11-12T11:44:44.490' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (24, N'EF', 2, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-11-12T11:44:44.490' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (24, N'TO', 3, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(100.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'15', CAST(N'2019-11-12T11:44:44.490' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (24, N'VA', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-11-12T11:44:44.490' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (24, N'VA', 2, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-11-12T11:44:44.490' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesplazas] ([idpromocion], [plaza], [sucursal], [idusuario], [fum]) VALUES (24, N'01', N'', N'RESM1979', CAST(N'2019-10-17T10:51:04.010' AS DateTime))
GO

--28	CHALECO $99 x $1800 CALZADO
--INSERT [dbo].[agrupaciones] ([idagrupacion], [nombre], [fecha], [idusuario], [fum]) VALUES (3, N'CALZADO 20% CREDITO 30% CONTAD', CAST(N'2019-10-17' AS Date), N'RESM1979', CAST(N'2019-10-17T11:09:57.383' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (3, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'', N'', N'División', 1, N'RESM1979', CAST(N'2019-10-17T11:10:19.247' AS DateTime))
--GO
INSERT [dbo].[promociones] ([idpromocion], [nombre], [fecha], [tipo], [estatus], [iniciopromo], [finpromo], [preciero], [senalizador], [clasificacion], [imagen], [minunicompra], [minimpcompra], [unipromo], [acumulable], [paresunicos], [clinoregis], [idusuariocaptura], [fumcaptura], [idusuarioaplica], [fumaplica], [idusuariopausa], [fumpausa], [idusuariocancela], [fumcancela], [idusuario], [fum], [promosrequeridas], [duplicados], [clienterequerido], [importeticket]) VALUES (28, N'CHALECO $99 x $1800 CALZADO', CAST(N'2019-12-27' AS Date), N'AxB', N'ACTIVO', CAST(N'2019-12-27T00:00:00.000' AS DateTime), CAST(N'2050-03-31T00:00:00.000' AS DateTime), N'AZUL', N'BLANCO', N'REBAJA', null, 0, CAST(2000.00 AS Numeric(18, 2)), 1, N'NO', N'SI', N'SI', N'RESM1979', CAST(N'2019-12-27T10:32:09.857' AS DateTime), N'RESM1979', CAST(N'2019-12-27T14:11:34.260' AS DateTime), NULL, NULL, NULL, NULL, N'RESM1979', CAST(N'2019-12-27T14:11:34.260' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[promocionesagrupaciones] ([idpromocion], [idagrupacioncompra], [idagrupacionpromo], [renglon], [idusuario], [fum]) VALUES (28, 3, 3, 1, N'RESM1979', CAST(N'2019-10-17T12:03:24.117' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (28, N'EF', 1, N'COMPRA', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(10.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.917' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (28, N'TO', 2, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(100.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.917' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesplazas] ([idpromocion], [plaza], [sucursal], [idusuario], [fum]) VALUES (28, N'01', N'', N'RESM1979', CAST(N'2019-12-10T12:41:36.517' AS DateTime))
GO

--14	20% DSCTO CALZADO CABALLERO -> min 3,500, imp ticket
--INSERT [dbo].[agrupaciones] ([idagrupacion], [nombre], [fecha], [idusuario], [fum]) VALUES (13, N'AGRUPACION CALZADO CABALLERO', CAST(N'2019-12-10' AS Date), N'RESM1979', CAST(N'2019-12-10T12:28:31.520' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (13, 1, 2, 0, 0, 0, 0, 0, 0, 0, 0, N'', N'', N'Departamento', 1, N'RESM1979', CAST(N'2019-12-10T12:28:43.643' AS DateTime))
--GO
INSERT [dbo].[promociones] ([idpromocion], [nombre], [fecha], [tipo], [estatus], [iniciopromo], [finpromo], [preciero], [senalizador], [clasificacion], [imagen], [minunicompra], [minimpcompra], [unipromo], [acumulable], [paresunicos], [clinoregis], [idusuariocaptura], [fumcaptura], [idusuarioaplica], [fumaplica], [idusuariopausa], [fumpausa], [idusuariocancela], [fumcancela], [idusuario], [fum], [promosrequeridas], [duplicados], [clienterequerido], [importeticket]) VALUES (114, N'20% DSCTO CALZADO CABALLERO', CAST(N'2019-12-10' AS Date), N'DIRECTA', N'ACTIVO', CAST(N'2019-12-10T00:00:00.000' AS DateTime), CAST(N'2050-04-30T00:00:00.000' AS DateTime), N'AZUL', N'BLANCO', N'REBAJA', null, 0, CAST(3500.00 AS Numeric(18, 2)), 0, N'NO', N'SI', N'SI', N'RESM1979', CAST(N'2019-12-10T12:28:07.797' AS DateTime), N'RESM1979', CAST(N'2019-12-10T12:37:31.980' AS DateTime), NULL, NULL, NULL, NULL, N'RESM1979', CAST(N'2019-12-10T12:37:31.980' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[promocionesagrupaciones] ([idpromocion], [idagrupacioncompra], [idagrupacionpromo], [renglon], [idusuario], [fum]) VALUES (114, 0, 13, 1, N'RESM1979', CAST(N'2019-12-10T12:29:24.753' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (114, N'EF', 0, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(20.00 AS Numeric(18, 2)), CAST(5.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.917' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesexclusiones] ([idpromocion], [marca], [estilon], [idusuario], [fum]) VALUES (114, N'FFF', N'', N'RESM1979', CAST(N'2019-12-10T12:30:04.183' AS DateTime))
GO
INSERT [dbo].[promocionesplazas] ([idpromocion], [plaza], [sucursal], [idusuario], [fum]) VALUES (114, N'01', N'', N'RESM1979', CAST(N'2019-12-10T12:30:13.793' AS DateTime))
GO


--200 - CALZADO 20% CREDITO, 30% CONTA
--INSERT [dbo].[agrupaciones] ([idagrupacion], [nombre], [fecha], [idusuario], [fum]) VALUES (3, N'CALZADO 20% CREDITO 30% CONTAD', CAST(N'2019-10-17' AS Date), N'RESM1979', CAST(N'2019-10-17T11:09:57.383' AS DateTime))
--GO
--INSERT [dbo].[agrupacionesdet] ([idagrupacion], [iddivisiones], [iddepto], [idfamilia], [idlinea], [idl1], [idl2], [idl3], [idl4], [idl5], [idl6], [marca], [estilon], [nivel], [renglon], [idusuario], [fum]) VALUES (3, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'', N'', N'División', 1, N'RESM1979', CAST(N'2019-10-17T11:10:19.247' AS DateTime))
--GO
INSERT [dbo].[promociones] ([idpromocion], [nombre], [fecha], [tipo], [estatus], [iniciopromo], [finpromo], [preciero], [senalizador], [clasificacion], [imagen], [minunicompra], [minimpcompra], [unipromo], [acumulable], [paresunicos], [clinoregis], [idusuariocaptura], [fumcaptura], [idusuarioaplica], [fumaplica], [idusuariopausa], [fumpausa], [idusuariocancela], [fumcancela], [idusuario], [fum], [promosrequeridas], [duplicados], [clienterequerido], [importeticket]) VALUES (200, N'CALZADO 20% CREDITO, 30% CONTA', CAST(N'2019-10-17' AS Date), N'DIRECTA', N'ACTIVO', CAST(N'2019-10-18T00:00:00.000' AS DateTime), CAST(N'2020-11-30T00:00:00.000' AS DateTime), N'AZUL', N'BLANCO', N'REBAJA', null, 1, CAST(0.00 AS Numeric(18, 2)), 0, N'NO', N'SI', N'SI', N'RESM1979', CAST(N'2019-10-17T11:11:41.653' AS DateTime), N'RESM1979', CAST(N'2019-10-17T12:09:09.680' AS DateTime), N'RESM1979', CAST(N'2019-11-16T10:16:44.550' AS DateTime), NULL, NULL, N'RESM1979', CAST(N'2019-11-16T10:16:44.550' AS DateTime), NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[promocionesagrupaciones] ([idpromocion], [idagrupacioncompra], [idagrupacionpromo], [renglon], [idusuario], [fum]) VALUES (200, 0, 3, 1, N'RESM1979', CAST(N'2019-10-17T11:11:56.313' AS DateTime))
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (200, N'CP', 0, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(20.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.930' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (200, N'CS', 0, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(20.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.930' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (200, N'CV', 0, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(20.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.927' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (200, N'EF', 0, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(30.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.910' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (200, N'MD', 0, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(30.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.917' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (200, N'TC', 0, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(30.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.917' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (200, N'TD', 0, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(30.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.913' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (200, N'TO', 0, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.907' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (200, N'VA', 0, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(20.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.920' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesdet] ([idpromocion], [formapago], [numunidad], [tipo], [impfijo], [descdirecto], [porcdinelec], [impbono], [idusuario], [fum], [descfijo]) VALUES (200, N'VS', 0, N'PROMO', CAST(0.00 AS Numeric(18, 2)), CAST(20.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), CAST(0.00 AS Numeric(18, 2)), N'RESM1979', CAST(N'2019-10-17T11:17:01.923' AS DateTime), NULL)
GO
INSERT [dbo].[promocionesexclusiones] ([idpromocion], [marca], [estilon], [idusuario], [fum]) VALUES (200, N'FFF', N'', N'RESM1979', CAST(N'2019-10-17T11:12:23.020' AS DateTime))
GO
INSERT [dbo].[promocionesexclusiones] ([idpromocion], [marca], [estilon], [idusuario], [fum]) VALUES (200, N'OZO', N'', N'RESM1979', CAST(N'2019-10-17T11:12:55.407' AS DateTime))
GO
INSERT [dbo].[promocionesplazas] ([idpromocion], [plaza], [sucursal], [idusuario], [fum]) VALUES (200, N'01', N'', N'RESM1979', CAST(N'2019-10-17T12:08:57.990' AS DateTime))
GO
