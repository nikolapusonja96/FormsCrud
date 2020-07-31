using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST5.BusinessLayer
{
    public class OperationResult
    {
        public IEnumerable<BaseDto> Data { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public bool IsSuccess => ErrorMessages.Count() == 0;
    }
}
