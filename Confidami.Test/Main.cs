using System;
using System.IO;
using Confidami.Common.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Confidami.Test
{
    [TestClass]
    public class Main
    {
        [TestMethod]
        public void TestMethod1()
        {
            string filename = "pippo.txt";

            var res = filename.RemoveExtensionsFilename();

            filename = "pippo";

            res = filename.RemoveExtensionsFilename();

        }
    }
}
