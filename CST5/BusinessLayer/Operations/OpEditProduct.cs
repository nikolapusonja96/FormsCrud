using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST5.BusinessLayer.Operations
{
    public class OpEditProduct : EfOperation
    {
        private CreateProductDto dto;
        private int idToUpdate;

        public OpEditProduct(CreateProductDto dto, int idToUpdate)
        {
            this.dto = dto;
            this.idToUpdate = idToUpdate;
        }

        public override OperationResult Execute()
        {
            //mora provera uvek, ovde je kao za insert
            var product = Db.Products.Find(idToUpdate);

            product.CategoryID = dto.CategoryId;
            product.ProductName = dto.ProductName;
            product.SupplierID = dto.SupplierId;
            product.UnitsInStock = (short)dto.Quantity;
            product.UnitPrice = dto.Price;

            Db.SaveChanges();

            return new OperationResult();

        }
    }
}
