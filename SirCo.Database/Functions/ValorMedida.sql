CREATE FUNCTION [dbo].[ValorMedida]
(
	@medida VARCHAR(3)
)
RETURNS VARCHAR(3)
AS
BEGIN
	DECLARE @res VARCHAR(3)
	SET @res = (
		SELECT
		CASE 
			WHEN @medida = 'XCH' THEN 'R01'
			WHEN @medida = 'CH'  THEN 'R02'
			WHEN @medida = 'MED' THEN 'R03'
			WHEN @medida = 'GDE' THEN 'R04'
			WHEN @medida = 'XGE' THEN 'R05'
			WHEN @medida = 'XXG' THEN 'R06'
			WHEN @medida = 'XXX' THEN 'R07'
			WHEN @medida = 'XX4' THEN 'R08'
			WHEN @medida = 'XX5' THEN 'R09'
			ELSE @medida
		END
	)
	RETURN @res
END
