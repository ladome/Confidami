using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlTypes;
using Confidami.Common.Utility;

namespace Confidami.Common
{
    public class Config
    {
        public static string UploadsFolder
        {
            get { return ConfigurationManager.AppSettings["UploadsFolder"];}
        }

        public static string UploadsTempFolder
        {
            get { return ConfigurationManager.AppSettings["UploadsTempFolder"]; }
        }


        public static string UploadThumbFormatExtension
        {
            get { return ConfigurationManager.AppSettings["UploadThumbFormatExtension"]; }
        }


        public static Dictionary<string, string> UploadThumbSettings
        {
            get
            {
                 var settings = ConfigurationManager.AppSettings["UploadThumbSettings"];
                var res = new Dictionary<string, string>();

                if(string.IsNullOrEmpty(settings))
                    throw new Exception("unable to read settings upload formats");
                var split = settings.CleanFromSpaceAndNewLine().Split('|');
                foreach (var s in split)
                {
                    var idx = s.IndexOf(':');
                    if(idx <= 0)
                        throw new Exception("uploads format bad format");
                    var format = s.Substring(0, idx);
                    var set = s.Substring(idx+1, (s.Length - idx -1));
                    res.Add(format, set);
                }

                return res;
            }
        }


        public static string UploadAcceptedFile
        {
            get { return ConfigurationManager.AppSettings["UploadAcceptedFile"]; }
        }

        public static string[] UploadImageExtensions
        {
            get
            {
                var conf = ConfigurationManager.AppSettings["UploadImageExtensions"];
                conf.CannotBeNull("extensions file format");
                return conf.Split(',');
            }
        }

        public static string[] AcceptedExtensions
        {
            get
            {
                var conf = ConfigurationManager.AppSettings["AcceptedExtensions"];
                conf.CannotBeNull("extensions file format");
                return conf.Split(',');
            }
        }

        public static string RandomStringChars
        {
            get { return ConfigurationManager.AppSettings["RandomStringChars"]; }
        }

        public static int RandomStringLenght
        {
            get { return Convert.ToInt16(ConfigurationManager.AppSettings["RandomStringLenght"]); }
        }
        

        
    }
}