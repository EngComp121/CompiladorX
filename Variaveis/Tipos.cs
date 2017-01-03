using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorX
{
    public struct Tipos
    {
        public static string Dec
        {
            get
            {
                return "Decimal";
            }
        }

        public static string Hex
        {
            get
            {
                return "Hexadecimal";
            }
        }

        public static string Bin
        {
            get
            {
                return "Binario";
            }
        }

        public static string Txt
        {
            get
            {
                return "Texto";
            }
        }
    }
}
