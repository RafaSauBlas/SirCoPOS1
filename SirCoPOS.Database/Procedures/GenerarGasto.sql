CREATE PROCEDURE [dbo].[GenerarGasto]
	@gid UNIQUEIDENTIFIER,
	@importe MONEY,
	@idcajero INT,
	@entrada BIT
AS
BEGIN
	DECLARE @id INT
	SET @id = (SELECT [Id] FROM [dbo].[Fondo] WHERE [ResponsableId] = @idcajero AND [FechaCierre] IS NULL)

	INSERT INTO [dbo].[FondoMovimiento] ([FondoId], [Importe], [Entrada], [Fecha], [Referencia], [Tipo])
	VALUES (@id, @importe, @entrada, GETDATE(), @gid, 'Gasto')
END
GO
