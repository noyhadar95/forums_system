using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcceptanceTestsBridge;

namespace AcceptanceTests.ServerTests
{
    [TestClass]
    public class UseCaseTestSuite
    {
        protected Bridge bridge;

        public UseCaseTestSuite()
        {
            bridge = new ProxyBridge();

        }
    }
}
