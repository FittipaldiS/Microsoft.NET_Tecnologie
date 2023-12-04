using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendEmailService.Helper
{
    public class VariusHelper
    {
        public double ByteToGb(ulong value)
        {
            return Math.Round(value / 1024d / 1024d / 1024d, 2);
        }
    }

}
