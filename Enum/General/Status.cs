using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Enum
{
    public partial class General
    {
        public enum Status : short
        {
            Delete = -1,
            Disabled = 0,
            Enabled = 1
        }
    }
}
