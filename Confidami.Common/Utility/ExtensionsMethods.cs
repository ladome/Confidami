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
    }
}
