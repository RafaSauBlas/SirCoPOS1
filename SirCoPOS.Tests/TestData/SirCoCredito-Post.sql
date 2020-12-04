update pd
set negocio = p.negocio,
vale = p.vale,
distrib = p.distrib
from [dbo].[planpagosdet] pd
inner join [dbo].[planpagos] p
on pd.[sucursal] = p.sucursal
	and pd.[nota] = p.nota
go


alter table [dbo].[planpagosdet]
alter column [negocio] [char](2) not NULL
go

alter table [dbo].[planpagosdet]
alter column [vale] [char](10) not NULL
go

alter table [dbo].[planpagosdet]
alter column [distrib] [char](6) not NULL
go

alter table [dbo].[planpagosdet]
add CONSTRAINT [PK_planpagosdet_sucursal] PRIMARY KEY CLUSTERED 
(
	[sucursal] ASC,
	[nota] ASC,
	[fechaaplicar] ASC,
	[pago] ASC,

	[negocio] ASC,--nuevo
	[vale] ASC,--nuevo
	[distrib] ASC -- nuevo
)
go
