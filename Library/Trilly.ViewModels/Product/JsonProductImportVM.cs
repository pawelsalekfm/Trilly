using System;
using System.Collections.Generic;
using System.Text;

namespace Trilly.ViewModels.Product
{
    public class JsonProductImportVM
    {
        public int Id { get; set; }
        public int TfoProductId { get; set; }
        public int? XlId { get; set; }
        public string Name { get; set; }
        public string MarketingDescriptionSmall { get; set; }
        public string MarketingDescriptionHTML { get; set; }
        public bool Novelty { get; set; }
        public string EanCode { get; set; }
        public string ProductCode { get; set; }
        public string StockStatus { get; set; }
        public string TeletoriumProductId { get; set; }
        public string BuyOption { get; set; }
        public double WPriceGross { get; set; }
        public double WPricePromotion { get; set; }
        public double PPriceGross { get; set; }
        public double PPricePromotion { get; set; }
        public int WAvailable { get; set; }
        public int PAvailable { get; set; }
        public double OnlineStockQuantity { get; set; }
        public string Slug { get; set; }
        public bool Imported { get; set; }
        public string ImageUrl { get; set; }
    }
}
