using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Constants
{
    public enum FormaPago
    {
        EF = 12 //EFECTIVO  
        ,TC = 18 //T. CREDITO
        ,TD = 19 //T. DEBITO
        ,DV = 11 //DEV./VENTA
        ,VA = 21 //VALE        
        ,CP = 5 //C. PERSONA
        ,CV = 7 //CONTRAVALE
        ,MD = 15 //DINERO ELE

        ,CI = 4	//CARGO INV 

        ,VS = 100 //VA - sin promocion

        , VD = 99 //VALE DIGITAL - ID???
        , CD = 98 //C. DISTRIBUIDOR - ID???
        , VE = 97 //VALE EXTERNO - ID???
    }
}
