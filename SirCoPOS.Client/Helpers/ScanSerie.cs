using SirCoPOS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Client.Helpers
{
    public class ScanSerie
    {
        private static Boolean Evaluando = false;
        private static DateTime? startTime = null;

        public static bool PorScanner(string serie, int depto)
        {
            if (serie == null || serie == "" || depto == (int)Common.Constants.Departamento.SIS || depto == (int)Common.Constants.Departamento.ADM)
            {
                Evaluando = false;
                return true;
            }
            if (!Evaluando && serie.Length > 0)
            {
                startTime = DateTime.Now;
                Evaluando = true;
            }
            else
            {
                if (serie.Length > 0)
                {
                    TimeSpan runLength = DateTime.Now.Subtract(startTime.Value);
                    int TotalMiliSecs = ((runLength.Seconds * 1000) + runLength.Milliseconds);
                    if (TotalMiliSecs >= 250)
                    {
                        Evaluando = false;
                    }
                }
            }
            return Evaluando;
        }
    }
}
