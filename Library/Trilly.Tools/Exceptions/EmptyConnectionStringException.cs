using System;
using System.Collections.Generic;
using System.Text;

namespace Trilly.Tools.Exceptions
{
    public class EmptyConnectionStringException : Exception
    {
        public EmptyConnectionStringException()
            : base()
        {
        }

        public EmptyConnectionStringException(string message)
            : base(message)
        {
        }

        public EmptyConnectionStringException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
