using CST5.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST5.BusinessLayer.Operations
{
    public class OpCreateProduct : EfOperation
    {
        //da se proslede podaci iz guija, kroz ctor
        private readonly CreateProductDto _dto;

        public OpCreateProduct(CreateProductDto dto)
        {
            _dto = dto;
        }

        public override OperationResult Execute()
        {
            var result = new OperationResult();

            if(Db.Products.Any(p=>p.ProductName == _dto.ProductName))
            {
                result.ErrorMessages.Add("Proizvod istog imena vec postoji");
            }

            if (!result.IsSuccess)
            {
                return result;
            }

            Db.Products.Add(new Product
            {
                CategoryID = _dto.CategoryId,
                SupplierID = _dto.SupplierId,
                ProductName = _dto.ProductName,
                UnitPrice = _dto.Price,
                UnitsInStock = (short)_dto.Quantity
            });

            Db.SaveChanges();
            return result;
        }
    }
}
