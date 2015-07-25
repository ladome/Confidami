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
                var split = settings.CleanFromSpaceAndNewLine().Replace("{format}", UploadThumbFormatExtension).Split('|');
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

        
    }
}