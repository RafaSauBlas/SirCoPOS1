using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SirCoPOS.Common.Helpers
{
   public class BaseExcepcion : System.Exception, ISerializable
   {
      public BaseExcepcion()
         : base()
      {
         // Add implementation (if required)
      }

      public BaseExcepcion(string message)
         : base(message)
      {
         // Add implementation (if required)
      }

      public BaseExcepcion(string message, System.Exception inner)
         : base(message, inner)
      { 
         // Add implementation (if required)
      }

      protected BaseExcepcion(SerializationInfo info, StreamingContext context)
         : base(info, context)
      { 
         // Add implementation (if required)
      }
   }
}