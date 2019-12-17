using System;
using System.Collections.Generic;
using System.Text;

namespace Trilly.ViewModels.Product
{
    public class JsonProductAttributeImportVm
    {
        public int Id { get; set; }
        public int AttributeId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public int ProductId { get; set; }
    }
}
