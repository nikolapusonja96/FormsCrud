using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST5.BusinessLayer.Operations
{
    public class OpGetSuppliers : EfOperation
    {
        public override OperationResult Execute()
        {
            var suppliers = Db.Suppliers.Select(s => new SupplierDto
            {
                Id= s.SupplierID,
                Name = s.CompanyName
            });

            return new OperationResult
            {
                Data = suppliers.ToList()
            };

        }
    }
}
