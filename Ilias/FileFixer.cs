using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IliasSync.Ilias
{
    public static class FileFixer
    {
        public static string RemoveIlegalPathCharacters(this string s)
        {
            var invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            foreach (var c in invalid)
            {
                s = s.Replace(c.ToString(), "");
            }

            return s;
        }
    }
}
