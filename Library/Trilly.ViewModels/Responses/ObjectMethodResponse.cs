using System;
using System.Collections.Generic;
using System.Text;

namespace Trilly.ViewModels.Responses
{
    public class ObjectMethodResponse : MethodResponse
    {
        public object ObjectResponse { get; set; }

        public ObjectMethodResponse(Object obj = null) : base()
        {
            ObjectResponse = obj;
        }

        public ObjectMethodResponse(MethodResponseTypeEnum status, Object obj = null) : base(status)
        {
            ObjectResponse = obj;
        }
    }
}
