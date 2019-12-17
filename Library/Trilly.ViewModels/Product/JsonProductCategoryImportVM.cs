using System;
using System.Collections.Generic;
using System.Text;

namespace Trilly.ViewModels.Product
{
    public class JsonProductCategoryImportVM
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
        public string Path { get; set; }
    }
}
