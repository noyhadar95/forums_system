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
            policy.AddPolicy(new PasswordPolicy(Policies.Password, 8,100));
            Assert.IsTrue(policy.CheckIfPolicyExists(Policies.Password));

        }

        [TestMethod]
        public void TestRemovePolicy()//2
        {
            policy = new UsersLoadPolicy(Policies.UsersLoad, 400);
            policy.AddPolicy(new PasswordPolicy(Policies.Password, 8,100));
            Assert.IsTrue(policy.CheckIfPolicyExists(Policies.Password));
            policy = policy.RemovePolicy(Policies.Password);//test removal from end
            Assert.IsFalse(policy.CheckIfPolicyExists(Policies.Password));
            policy.AddPolicy(new MinimumAgePolicy(Policies.MinimumAge ,16));
            policy.AddPolicy(new PasswordPolicy(Policies.Password, 8,100));
            policy = policy.RemovePolicy(Policies.MinimumAge);//test removal from middle
            Assert.IsFalse(policy.CheckIfPolicyExists(Policies.MinimumAge));
            policy = policy.RemovePolicy(Policies.UsersLoad);//test removal from head
            Assert.IsFalse(policy.CheckIfPolicyExists(Policies.UsersLoad));
            policy = policy.RemovePolicy(Policies.Password);//test removal of all policies
            Assert.IsNull(policy);
        }

        [TestMethod]
        public void TestDoubleAddPolicy()//3 test AddPolicy with a policy that already exists
        {
            policy = new UsersLoadPolicy(Policies.UsersLoad, 400);
            policy.AddPolicy(new PasswordPolicy(Policies.Password, 8,100));
            Assert.IsTrue(policy.CheckIfPolicyExists(Policies.Password));
            Assert.IsFalse(policy.AddPolicy(new PasswordPolicy(Policies.Password, 12,100)));
            
        }

        [TestMethod]
        public void TestMissingPasswordPolicy()//4
        {
            policy = new UsersLoadPolicy(Policies.UsersLoad, 120);
            PolicyParametersObject param = new PolicyParametersObject(Policies.Password);
            param.SetPassword("12345678");
            Assert.IsTrue(policy.CheckPolicy(param));
            param.SetPassword("1");
            Assert.IsTrue(policy.CheckPolicy(param));

        }

        [TestMethod]
        public void TestPasswordPolicy()//5
        {
            policy = new PasswordPolicy(Policies.Password, 8,100);
            PolicyParametersObject param = new PolicyParametersObject(Policies.Password);
            param.SetPassword("12345678");
            Assert.IsTrue(policy.CheckPolicy(param));
            param.SetPassword("1234567");
            Assert.IsFalse(policy.CheckPolicy(param));

        }

        [TestMethod]
        public void TestMaxModeratorsPolicy()//6
        {
            policy = new MaxModeratorsPolicy(Policies.MaxModerators, 4);
            PolicyParametersObject param = new PolicyParametersObject(Policies.MaxModerators);
            param.CurrNumOfModerators = 0;
            param.NumOfModeratorsToAdd = 1;
            Assert.IsTrue(policy.CheckPolicy(param));
            param.NumOfModeratorsToAdd = 5;
            Assert.IsFalse(policy.CheckPolicy(param));

        }

        [TestMethod]
        public void TestMinAgePolicy()//7
        {
            policy = new MinimumAgePolicy(Policies.MinimumAge, 16);
            PolicyParametersObject param = new PolicyParametersObject(Policies.MinimumAge);
            param.SetAgeOfUser(18);
            Assert.IsTrue(policy.CheckPolicy(param));
            param.SetAgeOfUser(5);
            Assert.IsFalse(policy.CheckPolicy(param));

        }

        [TestMethod]
        public void TestUsersLoadPolicy()//8
        {
            policy = new UsersLoadPolicy(Policies.UsersLoad, 70);
            PolicyParametersObject param = new PolicyParametersObject(Policies.UsersLoad);
            param.SetPolicy(Policies.UsersLoad);
            param.SetNumOfUsers(26);
            Assert.IsTrue(policy.CheckPolicy(param));
            param.SetNumOfUsers(70);
            Assert.IsFalse(policy.CheckPolicy(param));

        }
    }
}
