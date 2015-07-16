using System.Configuration;

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

        
    }
}