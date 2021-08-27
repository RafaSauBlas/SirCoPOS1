using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Constants
{
    public enum FormaPago
    {
        CI = 4,	//CARGO INV 
        CP = 5, //C. PERSONA
        CV = 7, //CONTRAVALE
        DV = 11, //DEV./VENTA
        EF = 12, //EFECTIVO  
        MD = 15, //DINERO ELE
        TC = 18, //T. CREDITO
        TD = 19, //T. DEBITO
        VA = 21, //VALE        
        VE = 25, //VALE EXTERNO 
        CD = 24, //C. DISTRIBUIDOR 
        VD = 23, //VALE DIGITAL 
        VS = 100, //VA - sin promocion
        CS = 101,
        GO = 26
    }
}
