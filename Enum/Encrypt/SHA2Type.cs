using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Enum
{
    public partial class Encrypt
    {
        public enum SHA2Type : short
        {
            Use256 = 1,
            Use384 = 2,
            Use512 = 3
        }
    }
}
