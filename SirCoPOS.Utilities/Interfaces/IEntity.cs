using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Utilities.Interfaces
{
    interface IEntity
    {
        void CommitEdit();
        void CancelEdit();
        bool IsDirty();
        bool IsValid();
    }
}
