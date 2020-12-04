using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SirCoPOS.Common.Constants;
using SirCoPOS.Common.Entities;

namespace SirCoPOS.Client.ViewModels.Caja
{
    class PagoCreditoDistribuidorViewModel : PagoCreditoViewModel
    {
        public override FormaPago FormaPago => FormaPago.CD;
        public override string Title => "Pago Credito Distribuidor";
        protected override async Task<Distribuidor> Find(string id)
        {
            //return await _proxy.FindDistribuidorAsync(id);
            throw new NotImplementedException();
        }
    }
}
