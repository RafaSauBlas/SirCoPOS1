IF EXISTS(SELECT 1
    FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
    WHERE CONSTRAINT_NAME='PK_planpagosdet_sucursal' )
BEGIN

alter table [dbo].[planpagosdet]
drop CONSTRAINT [PK_planpagosdet_sucursal] 

END
go

alter table [dbo].[planpagosdet]
alter column [negocio] [char](2) NULL
go

alter table [dbo].[planpagosdet]
alter column [vale] [char](10) NULL
go

alter table [dbo].[planpagosdet]
alter column [distrib] [char](6) NULL
go