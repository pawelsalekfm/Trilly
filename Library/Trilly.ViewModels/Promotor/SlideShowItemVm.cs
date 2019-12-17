using System;
using System.Collections.Generic;
using System.Text;

namespace Trilly.ViewModels.Promotor
{
    public class SlideShowItemVm
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
    }
}
