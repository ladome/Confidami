using System.Configuration;
using System.Data.SqlClient;

namespace Confidami.Data
{
    public class DbUtilities
    {
        public static SqlConnection Connection
        {
            get
            {
                return new SqlConnection(ConfigurationManager.ConnectionStrings["ConfidamiConnection"].ConnectionString);
            }
        }
    }
}