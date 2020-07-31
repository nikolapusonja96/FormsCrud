using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST5.BusinessLayer.Operations
{
    public class OpDeleteProductBatch : EfOperation
    {
        private readonly IEnumerable<int> _idsToDelete;

        public OpDeleteProductBatch(IEnumerable<int> idsToDelete)
        {
            if(idsToDelete == null)
            {
                throw new ArgumentException("ne sme biti null");
            }
            if (!idsToDelete.Any())
            {
                throw new ArgumentException("Prazna kolekcija");
            }
            this._idsToDelete = idsToDelete;
        }

        public override OperationResult Execute()
        {
            var operationResult = new OperationResult();

            foreach(var id in _idsToDelete) 
            {
                var product = Db.Products.Find(id);
                if (product == null) 
                {
                    operationResult.ErrorMessages.Add($"proizvod sa id-em {id} ne postoji");
                }
                Db.Products.Remove(product);
            }
            if (operationResult.IsSuccess)
            {
                Db.SaveChanges();
            }
            return operationResult;
            
        }
    }
}
