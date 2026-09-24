using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string? s)
        {
            return s.IsNullOrEmpty();
        }
        public static bool IsEmail(this string? s) 
        { 
            if(string.IsNullOrWhiteSpace(s)) return false;
            if (s.Count(i => i == '@') != 1) return false;
            return true;
        }
    }
}
