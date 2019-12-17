using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trilly.ViewModels.Product.Queries
{
    public class ProductQueryResponse
    {
        public int Counter { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public List<ProductVM> Products { get; set; }

        public ProductQueryResponse()
        {
            Counter = 0;
            MinPrice = 0;
            MaxPrice = 0;
            Products = new List<ProductVM>();
        }
    }
}
