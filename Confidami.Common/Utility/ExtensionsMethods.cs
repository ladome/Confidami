using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public static void CannotBeNullEmptyOrWithespace(this string obj, string parameter)
        {
            if (string.IsNullOrEmpty(obj.Trim()))
                throw new ArgumentException("Cannot be null or empty", parameter);
        }

        public static void FileExistsOrThrowException(this string filename )
        {
            if (!File.Exists(filename))
                throw new Exception("Impossbile trovare file: " + filename);
        }

        public static string RemoveExtensionsFilename(this string filename)
        {
            var extension = Path.GetExtension(filename);
            if (!string.IsNullOrEmpty(extension))
                return filename.Substring(0, filename.Length - extension.Length);
            return filename;
        }

        public static string GetExtension(this string filename)
        {
            var extension = Path.GetExtension(filename);
            if (!string.IsNullOrEmpty(extension))
                return extension;
            return filename;
        }

        public static string CleanFromSpaceAndNewLine(this string text)
        {
            text = text.Replace("\r", "");
            text = text.Replace("\t", "");
            text = text.Replace("\n", "");
            return Regex.Replace(text, @"\s+", "");

        }

        public static string ComposeUri(this string baseUrl, string relativePath)
        {
            return new Uri(new Uri(baseUrl, UriKind.Absolute), relativePath).ToString();
        }

        public static string RemoveExtensionPoint(this string extension)
        {
            extension.CannotBeNull("extension");
            extension = Path.GetExtension(extension.ToLower());
            if (!string.IsNullOrEmpty(extension) && extension.StartsWith("."))
            {
                extension = extension.Substring(1);
            }
            return extension;
        }

        public static bool IsNumeric(this string expression)
        {
            double retNum;

            bool isNum = Double.TryParse(Convert.ToString(expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        public static IEnumerable<int> AllIndexesOf(this string str, string value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", "value");
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index, System.StringComparison.InvariantCultureIgnoreCase);
                if (index == -1)
                    break;
                yield return index;
            }
        }


    }
}
