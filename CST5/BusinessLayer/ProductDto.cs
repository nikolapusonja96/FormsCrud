using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST5.BusinessLayer
{
    public class ProductDto : BaseDto
    {
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public decimal? UnitPrice { get; set; }
        public int TimesBeenInOrder { get; set; }
        public decimal? TotalMoneyMade { get; set; }
    }

    //klasa za prenos od gui-a do biznis sloja
    public class CreateProductDto : BaseDto
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
    }
}
