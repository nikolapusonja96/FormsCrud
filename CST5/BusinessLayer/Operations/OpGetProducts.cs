using CST5.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST5.BusinessLayer.Operations
{
    public class OpGetProducts : EfOperation
    {
        private OpProductSearchCriteria criteria;

        public OpGetProducts()
        {
            //inicijalizacija inace bi polje bilo null
            criteria = new OpProductSearchCriteria();
        }
        public OpGetProducts(OpProductSearchCriteria criteria)
        {
            this.criteria = criteria;
        }
        public override OperationResult Execute()
        {

            //kad je proizvod prvi put kupljen
            //p.Order_Details.OrderBy(od => od.Order.OrderDate).First().Order.OrderDate 
            //za svaki proizvod vracamo objekat ProductDto


            //gradjenje upita
            var query = Db.Products.AsQueryable();

            if (criteria.Name != null)
            {
                query = query.Where(p => p.ProductName.ToLower().Contains(criteria.Name));
            }

            var products = query.Select(p => new ProductDto
            {
                Id = p.ProductID,
                Name = p.ProductName,
                CategoryName = p.Category.CategoryName,
                UnitPrice = p.UnitPrice ?? 0, 
                TimesBeenInOrder = p.Order_Details.Count,
                TotalMoneyMade = p.Order_Details.Any() ? p.Order_Details.Sum(od => od.Quantity * od.UnitPrice) : 0
            });

            return new OperationResult
            {
                Data = products.ToList()
            };
        }
    }

    public class OpProductSearchCriteria
    {
        public string Name { get; set; }

    }
}
