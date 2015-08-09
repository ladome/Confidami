using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confidami.Model
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class BaseResponseExtended : BaseResponse
    {
        public int ErrorCode { get; set; }
    }

    public class CheckEditCodeResponse : BaseResponse
    {
        public PostLight PostLigh { get; set; }
    }
}
