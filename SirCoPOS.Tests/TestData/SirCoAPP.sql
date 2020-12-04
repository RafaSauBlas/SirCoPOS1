SET IDENTITY_INSERT [dbo].[valedigital] ON 
GO
INSERT [dbo].[valedigital] ([idvaledigital], [distrib], [idauxiliar], [idcliente], [idvale], [codigoqr], [vigencia], [estatus], [imppedido], [impotorgado], [disponible], [electronica], [promocion], [comentarios], [fum]) VALUES (2, N'003763', 0, 810374, 1, N'123', NULL, N'1', NULL, NULL, CAST(2000.00 AS Decimal(18, 2)), 1, 1, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[valedigital] OFF
GO

INSERT [dbo].[dinero] ([idsucursal], [cliente], [vigencia], [importe], [saldo], [idusuario], [fum]) VALUES (1, N'144666', NULL, NULL, CAST(300.00 AS Decimal(18, 2)), NULL, NULL)
GO
INSERT [dbo].[dinerodet] ([idsucursal], [cliente], [sucnota], [nota], [descrip], [vigencia], [importe], [saldo], [tipo], [estatus], [idusuario], [fum]) VALUES (1, N'144666', N'01', N'1', NULL, NULL, 100,  CAST(100.00 AS Decimal(18, 2)), NULL, N'AC', NULL, NULL)
GO
INSERT [dbo].[dinerodet] ([idsucursal], [cliente], [sucnota], [nota], [descrip], [vigencia], [importe], [saldo], [tipo], [estatus], [idusuario], [fum]) VALUES (1, N'144666', N'01', N'2', NULL, NULL, 100, CAST(100.00 AS Decimal(18, 2)), NULL, N'AC', NULL, NULL)
GO
INSERT [dbo].[dinerodet] ([idsucursal], [cliente], [sucnota], [nota], [descrip], [vigencia], [importe], [saldo], [tipo], [estatus], [idusuario], [fum]) VALUES (1, N'144666', N'01', N'3', NULL, NULL, 100, CAST(100.00 AS Decimal(18, 2)), NULL, N'AC', NULL, NULL)
GO
