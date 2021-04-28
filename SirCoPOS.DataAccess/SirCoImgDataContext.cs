using SirCoPOS.DataAccess.SirCoImg;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.DataAccess
{
    public class SirCoImgDataContext : DbContext
    {
        public SirCoImgDataContext()
            : base("SirCoImg")
        {

        }
        public virtual DbSet<ArticuloImg> Imagenes { get; set; }
    }
}
