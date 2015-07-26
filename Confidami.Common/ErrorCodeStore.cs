using System.Collections.Generic;

namespace Confidami.Common
{
    public class ErrorCodeStore
    {
        public static Dictionary<ErrorCode, string> ErrorStore = new Dictionary<ErrorCode, string>()
        {
            {ErrorCode.FilePresent,"Il file caricato è già presente"},
            {ErrorCode.NotAdmittedExtension,"Il formato caricato non è ammesso. Formati ammessi: " + Config.AcceptedExtensions}
        };

        public string this[ErrorCode index]
        {
            get
            {
                return ErrorStore[index];
            }
        }
    }
}