using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYPM.Models
{
    public class MobileVerfication
    {
        public string phone { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
        public bool IsVarified { get; set; }
    }
}
