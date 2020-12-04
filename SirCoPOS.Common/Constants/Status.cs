using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Constants
{
    public enum Status
    {
        TR,//Traspaso
        AC,//Activo
        BA,//Baja
        ZC,
        IF//Inventario Fisico
        
        //nuevos
        ,CA//TODO nuevo Caja
        ,AB//TODO nuevo Cancelado Activo
        ,NA//TODO nuevo no articulo
    }
}
