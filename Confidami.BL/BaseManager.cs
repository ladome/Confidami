using System;
using Confidami.Common;
using Confidami.Common.Utility;

namespace Confidami.BL
{
    public class BaseManager
    {
        public static string GetRandomString()
        {
            return Helper.GetRandomString(Config.RandomStringLenght, new Random(), Config.RandomStringChars);
        }
    }
}