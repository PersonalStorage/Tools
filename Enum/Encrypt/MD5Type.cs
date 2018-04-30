using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Enum
{
    public partial class Encrypt
    {
        public enum MD5Type : short
        {
            Use16Char = 1,
            Use32Char = 2,
            Use64Char = 3
        }
    }
}
