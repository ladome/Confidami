using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confidami.Common.Utility
{
    public class Helper
    {
        public static string GetRandomString(int length, Random random, string chars, bool randomCapitalize=true)
        {
            chars.CannotBeNull("chars");
            var result = new string(
                Enumerable.Repeat(chars, length)
                          .Select(s => random.Next(2) % 2 == 0 ? Char.ToUpper(s[random.Next(s.Length)]) : Char.ToLower(s[random.Next(s.Length)]))
                          .ToArray());

            return result;
        }

    }
}
