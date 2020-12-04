using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Win.Messages
{
    class UpdateStatus
    {
        public Guid GID { get; set; }
        public bool Complete { get; set; }
    }
}
