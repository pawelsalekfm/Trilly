using System;
using System.Collections.Generic;
using System.Text;

namespace Trilly.ViewModels.Promotor
{
    public class CreateSlideShowRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ValidFrom { get; set; }
    }
}
