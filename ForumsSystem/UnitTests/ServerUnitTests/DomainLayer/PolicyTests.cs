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
        public void TestAddPolicy()//1
        {
            policy = new UsersLoadPolicy(Policies.UsersLoad, 400);
            policy.AddPolicy(new PasswordPolicy(Policies.password, 8));
            Assert.IsTrue(policy.CheckIfPolicyExists(Policies.password));

        }

        [TestMethod]
        public void TestRemovePolicy()//2
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
        public void TestDoubleAddPolicy()//3 test AddPolicy with a policy that already exists
        {
            policy = new UsersLoadPolicy(Policies.UsersLoad, 400);
            policy.AddPolicy(new PasswordPolicy(Policies.password, 8));
            Assert.IsTrue(policy.CheckIfPolicyExists(Policies.password));
            Assert.IsFalse(policy.AddPolicy(new PasswordPolicy(Policies.password, 12)));
            
        }

        [TestMethod]
        public void TestMissingPasswordPolicy()//4
        {
            policy = new UsersLoadPolicy(Policies.UsersLoad, 120);
            PolicyParametersObject param = new PolicyParametersObject(Policies.password);
            param.SetPassword("12345678");
            Assert.IsTrue(policy.CheckPolicy(param));
            param.SetPassword("1");
            Assert.IsTrue(policy.CheckPolicy(param));

        }

        [TestMethod]
        public void TestPasswordPolicy()//5
        {
            policy = new PasswordPolicy(Policies.password, 8);
            PolicyParametersObject param = new PolicyParametersObject(Policies.password);
            param.SetPassword("12345678");
            Assert.IsTrue(policy.CheckPolicy(param));
            param.SetPassword("1234567");
            Assert.IsFalse(policy.CheckPolicy(param));

        }
    }
}
