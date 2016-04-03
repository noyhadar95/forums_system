using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumsSystem.Server.ForumManagement.DomainLayer;

namespace UnitTests.ServerUnitTests.DomainLayer
{
    [TestClass]
    public class PolicyTests
    {
        private ForumsSystem.Server.ForumManagement.DomainLayer.Policy policy;
        [TestInitialize()]
        public void Initialize()
        {
            
        }
        [TestCleanup()]
        public void Cleanup() { policy = null; }
        [TestMethod]
        public void TestPasswordPolicy()
        {
            policy = new PasswordPolicy(Policies.password, 8);
            PolicyParametersObject param = new PolicyParametersObject(Policies.password);
            param.setPassword("12345678");
            Assert.IsTrue(policy.CheckPolicy(param));
            param.setPassword("1234567");
            Assert.IsFalse(policy.CheckPolicy(param));

        }

        [TestMethod]
        public void TestAddPolicy()
        {
            policy = new UsersLoadPolicy(Policies.UsersLoad, 400);
            policy.AddPolicy(new PasswordPolicy(Policies.password, 8));
            Assert.IsTrue(policy.CheckIfPolicyExists(Policies.password));

        }

        [TestMethod]
        public void TestRemovePolicy()
        {
            policy = new UsersLoadPolicy(Policies.UsersLoad, 400);
            policy.AddPolicy(new PasswordPolicy(Policies.password, 8));
            Assert.IsTrue(policy.CheckIfPolicyExists(Policies.password));
            policy = policy.RemovePolicy(Policies.password);//test removal from end
            Assert.IsFalse(policy.CheckIfPolicyExists(Policies.password));
            policy.AddPolicy(new MinimumAgePolicy(Policies.MinimumAge ,16));
            policy.AddPolicy(new PasswordPolicy(Policies.password, 8));
            policy = policy.RemovePolicy(Policies.MinimumAge);//test removal from middle
            Assert.IsFalse(policy.CheckIfPolicyExists(Policies.MinimumAge));
            policy = policy.RemovePolicy(Policies.UsersLoad);//test removal from head
            Assert.IsFalse(policy.CheckIfPolicyExists(Policies.UsersLoad));
            policy = policy.RemovePolicy(Policies.password);//test removal of all policies
            Assert.IsNull(policy);
        }

        [TestMethod]
        public void TestDoubleAddPolicy()
        {
            policy = new UsersLoadPolicy(Policies.UsersLoad, 400);
            policy.AddPolicy(new PasswordPolicy(Policies.password, 8));
            Assert.IsTrue(policy.CheckIfPolicyExists(Policies.password));
            Assert.IsFalse(policy.AddPolicy(new PasswordPolicy(Policies.password, 12)));
            
        }

        [TestMethod]
        public void TestMissingPasswordPolicy()
        {
            policy = new UsersLoadPolicy(Policies.UsersLoad, 120);
            PolicyParametersObject param = new PolicyParametersObject(Policies.password);
            param.setPassword("12345678");
            Assert.IsTrue(policy.CheckPolicy(param));
            param.setPassword("1");
            Assert.IsTrue(policy.CheckPolicy(param));

        }
    }
}
