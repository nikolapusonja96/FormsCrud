using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST5.BusinessLayer.Operations
{
    public class OpGetCategories : EfOperation
    {
        public override OperationResult Execute()
        {
            var categories = Db.Categories.Select(c => new CategoryDto
            {
                Id = c.CategoryID,
                Name = c.CategoryName
            });

            return new OperationResult
            {
                Data = categories.ToList()
            };
        }
    }
}
