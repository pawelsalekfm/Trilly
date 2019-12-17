using System;
using System.Collections.Generic;
using System.Text;

namespace Trilly.ViewModels.Promotor
{
    public class SlideShowVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public List<SlideShowItemVm> Slides { get; set; }

        public SlideShowVm()
        {
            Slides = new List<SlideShowItemVm>();
        }
    }
}
