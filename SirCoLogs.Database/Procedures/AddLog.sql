CREATE PROCEDURE [dbo].[AddLog]
	@application VARCHAR(50), 
    @machineName VARCHAR(50), 	
    @level VARCHAR(50), 
	@logger VARCHAR(250), 
	@module VARCHAR(50),
	@type VARCHAR(250),
    @callsite VARCHAR(MAX), 
	@message VARCHAR(MAX),
    @exception VARCHAR(MAX)
AS
BEGIN
	
	INSERT INTO [dbo].[Log](
		[Date], 
		[Application],
		[MachineName],
		[Level],
		[Logger],
		[Type],
		[Module],
		[Callsite],
		[Message],
		[Exception] 
	) VALUES (
		GETDATE(),
		@application,
		@machineName,
		@level,
		@logger,
		@type,
		@module,
		@callsite,
		@message,
		@exception
	)

END
