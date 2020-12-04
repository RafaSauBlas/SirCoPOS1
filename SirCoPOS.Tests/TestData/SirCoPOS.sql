INSERT INTO [dbo].[Caja]([Sucursal], [Numero], [Disponible], [Tipo]) VALUES
('01', 1, 0, 0)

INSERT INTO [dbo].[Fondo]([ResponsableId], [Disponible], [Tipo], [AuditorAperturaId], [FechaApertura], [CajaSucursal], [CajaNumero]) VALUES
(0, 0, 0, 0, '2020-01-01', '01', 1)