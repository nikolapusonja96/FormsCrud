using CST5.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST5.BusinessLayer
{
    public abstract class Operation
    {
        public abstract OperationResult Execute();
    }

    public abstract class EfOperation : Operation
    {
        private NorthwindContext context { get; } = new NorthwindContext();

        protected NorthwindContext Db => context;
    }
}
