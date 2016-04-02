using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcceptanceTestsBridge;

namespace AcceptanceTests.ServerTests
{
    [TestClass]
    public class UseCaseTest
    {
        protected Bridge bridge;

        public UseCaseTest()
        {
            bridge = new ProxyBridge();

        }


    }
}
