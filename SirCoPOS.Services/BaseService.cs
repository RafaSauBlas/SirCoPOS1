using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.Services
{
    public class BaseService
    {
        private string GetHeader(string name)
        {
            var request = (HttpRequestMessageProperty)OperationContext.Current.IncomingMessageProperties[HttpRequestMessageProperty.Name];
            return request.Headers[name];
        }
        protected int GetUserId()
        {
            var id = int.Parse(this.GetHeader("UserId"));
            return id;
        }
        protected int? GetUserIdOrDefault()
        {
            var uid = this.GetHeader("UserId");
            if (!string.IsNullOrWhiteSpace(uid))
            {
                var id = int.Parse(uid);
                return id;
            }
            return null;
        }
        protected string GetSucursal()
        {
            var id = this.GetHeader("Sucursal");
            return id;
        }
    }
}
