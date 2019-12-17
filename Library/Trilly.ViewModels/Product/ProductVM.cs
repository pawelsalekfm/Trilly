using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Trilly.ViewModels.Product
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public bool Novelty { get; set; }
        public string EanCode { get; set; }
        public double Price { get; set; }
        public double PricePromotion { get; set; }
        public string ImageUrl { get; set; }

        public bool IsCorrect()
        {
            if (Id == 0)
                return false;

            if (string.IsNullOrEmpty(Name))
                return false;

            if (string.IsNullOrEmpty(Slug))
                return false;

            if (string.IsNullOrEmpty(EanCode))
                return false;

            if (Price <= 0)
                return false;

            if (PricePromotion <= 0)
                return false;

            if (string.IsNullOrEmpty(ImageUrl))
                return false;

            return true;
        }
    }
}
