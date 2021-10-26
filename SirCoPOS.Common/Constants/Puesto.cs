using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Common.Constants
{
    public enum Puesto
    {
        //3	TDA	TIENDA
        VEN = 4 //VENDEDORA
        ,ENC = 5 //ENCARGADO DE TIENDA
        ,SUP = 6 //SUPLENTE DE ENCARGADO
        ,CJA = 7 //CAJERA

        //2	ADM	ADMINISTRACIÓN
        ,JFA = 1 //JEFE DE ADMINISTRACIÓN
        ,GEA = 10 //GERENTE ADMINISTRATIVO
        ,AUD = 13 //AUDITOR
        ,EGR = 17 //EGRESOS
        ,AXC = 35 //AUXILIAR CONTABLE
        ,AUA = 52 //AUXILIAR ADMINISTRACION

        ,MEN = 14 //MENSAJERO

        //7	OPE	OPERACIONES 
        ,OSU = 23 //SUPERVISOR
    }
    public class Puestos
    {
        public static readonly int[] Admin = new int[]
        {
            (int)Puesto.JFA
            ,(int)Puesto.GEA
            ,(int)Puesto.AUD
            ,(int)Puesto.EGR
            ,(int)Puesto.AXC
            ,(int)Puesto.AUA
            ,(int)Puesto.MEN
        };
        public static readonly int[] Gerentes = new int[]
        {
            (int)Puesto.ENC,
            (int)Puesto.SUP
        };
    }
}
