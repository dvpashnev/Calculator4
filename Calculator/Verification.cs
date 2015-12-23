using System;

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
