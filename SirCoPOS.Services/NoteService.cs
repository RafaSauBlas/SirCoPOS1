using SirCoPOS.BusinessLogic;
using SirCoPOS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Services
{
    public class NoteService : Common.ServiceContracts.INoteService
    {
        private readonly DataAccess.SirCoPOSDataContext _ctx;
        public NoteService()
        {
            _ctx = new DataAccess.SirCoPOSDataContext();
        }

        public int SaveNote(NoteRequest request)
        {
            var nitem = new DataAccess.SirCoPOS.Nota
            {
                Date = DateTime.Now,
                Sucursal = request.Sucursal,
                CajeroId = 0,
                VendedorId = request.VendedorId
            };
            _ctx.Notas.Add(nitem);
            nitem.Items = new HashSet<DataAccess.SirCoPOS.NotaDetalle>();
            nitem.Pagos = new HashSet<DataAccess.SirCoPOS.NotaPago>();
            foreach (var item in request.Items)
            {
                nitem.Items.Add(new DataAccess.SirCoPOS.NotaDetalle 
                { 
                    Serie = item.Serie, 
                    Amount = item.Amount,
                    Coments = item.Comments
                });
            }
            _ctx.SaveChanges();

            return nitem.Id;
        }
    }
}
