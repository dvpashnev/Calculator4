using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator
{
    static class Verification
    {
        
        public static bool Check (string str)
        {
             int number;
             return Int32.TryParse(str, out number);
        }
    }
}
