using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confidami.Common.Utility
{
    public static class ExtensionsMethods
    {
        public static void CannotBeNull(this object obj, string parameter)
        {
            if(obj == null)
                throw new ArgumentException("Cannot be null", parameter);
        }
    }
}
