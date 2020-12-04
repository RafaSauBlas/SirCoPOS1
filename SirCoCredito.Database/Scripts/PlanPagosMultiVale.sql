
alter table [dbo].[planpagos]
drop constraint [PK_planpagos_distrib]
go

alter table [dbo].[planpagos]
add CONSTRAINT [PK_planpagos_distrib] PRIMARY KEY CLUSTERED 
(
	[distrib] ASC,
	[sucursal] ASC,
	[nota] ASC,
	[negocio] ASC,--nuevo
	[vale] ASC --nuevo
)
go

alter table [dbo].[planpagosdet]
add [distrib] char(6) null
go

alter table [dbo].[planpagosdet]
add [negocio] [char](2) NULL
go

alter table [dbo].[planpagosdet]
add [vale] [char](10) NULL
go

update pd
set [negocio] = p.negocio,
[vale] = p.vale,
[distrib] = p.distrib
from [dbo].[planpagosdet] pd
inner join [dbo].[planpagos] p
on pd.[sucursal] = p.sucursal
	and pd.[nota] = p.nota
go

/*
--temp fix
delete from [dbo].[planpagosdet]
where [distrib] is null
*/

alter table [dbo].[planpagosdet]
drop constraint [PK_planpagosdet_sucursal]
go

alter table [dbo].[planpagosdet]
alter column [distrib] char(6) not null
go

alter table [dbo].[planpagosdet]
alter column [negocio] [char](2) NOT NULL
go

alter table [dbo].[planpagosdet]
alter column [vale] [char](10) NOT NULL
go

alter table [dbo].[planpagosdet]
add CONSTRAINT [PK_planpagosdet_sucursal] PRIMARY KEY CLUSTERED 
(
	[sucursal] ASC,
	[nota] ASC,
	[fechaaplicar] ASC,
	[pago] ASC,

	[distrib] ASC,--nuevo
	[negocio] ASC,--nuevo
	[vale] ASC--nuevo
)
go
