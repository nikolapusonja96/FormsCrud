using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST5.BusinessLayer.Operations
{
    public class OpGetProduct : EfOperation
    {
        private int id;

        public OpGetProduct(int id)
        {
            this.id = id;
        }

        public override OperationResult Execute()
        {
            var product = Db.Products.Find(id);

            var result = new OperationResult();

            return new OperationResult
            {
                Data = new List<CreateProductDto>
                {
                    new CreateProductDto
                    {
                        CategoryId = product.CategoryID.Value,
                        SupplierId = product.SupplierID.Value,
                        ProductName = product.ProductName,
                        Price = product.UnitPrice.Value,
                        Quantity = product.UnitsInStock.Value
                    }
                }
            };
                
            
        }
    }
}
