using System;
using System.Collections.Generic;
using System.Text;

namespace Trilly.ViewModels.Responses
{
    public enum MethodResponseTypeEnum
    {
        Ok = 0,
        Failure = 1,
        Exception = 2,
        Forbidden = 3
    }

    public class MethodResponse
    {
        public MethodResponse()
        {
            Status = MethodResponseTypeEnum.Ok;
        }

        public MethodResponse(MethodResponseTypeEnum status)
        {
            Status = status;
        }

        //public int Status { get; set; }
        public MethodResponseTypeEnum Status { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}
