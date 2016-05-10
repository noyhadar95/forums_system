using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumsSystem.Server.UserManagement.DomainLayer;
using ForumsSystem.Server.ForumManagement.Data_Access_Layer;
using System.Data;
using System.Runtime.Serialization;


namespace ForumsSystem.Server.ForumManagement.DomainLayer
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(AdminAppointmentPolicy))]
    [KnownType(typeof(AuthenticationPolicy))]
    [KnownType(typeof(ConfidentialityPolicy))]
    [KnownType(typeof(MaxModeratorsPolicy))]
    [KnownType(typeof(MemberSuspensionPolicy))]
    [KnownType(typeof(MinimumAgePolicy))]
    [KnownType(typeof(ModeratorAppointmentPolicy))]
    [KnownType(typeof(ModeratorSeniorityPolicy))]
    [KnownType(typeof(ModeratorSuspensionPolicy))]
    [KnownType(typeof(PasswordPolicy))]
    [KnownType(typeof(UsersLoadPolicy))]
    [Serializable]
    public abstract class Policy
    {
        /*
                public Policies type { get; set; }
                public Policy nextPolicy { get; set; }
                public int id { get; set; }
                DAL_Policy dal_policy = new DAL_Policy();
        */
        [DataMember]
        protected Policies type;
        [DataMember]
        private Policy nextPolicy;
        private int id;
        [NonSerialized()]
        DAL_Policy dal_policy = new DAL_Policy();
        [NonSerialized()]
        protected DAL_PolicyParameter dal_policyParameter = new DAL_PolicyParameter();


        protected Policy()
        {

        }
        public Policy(Policies type)
        {
            this.type = type;
            this.nextPolicy = null;
            id = dal_policy.createPolicy((int)type, -1);
        }

        public static Policy populatePolicy(int id)
        {
            if (id < 0)
                return null;
            Policy policy = null;
            DAL_Policy dp = new DAL_Policy();
            DataTable policyTBL = dp.getPolicy(id);
            foreach (DataRow policyRow in policyTBL.Rows) //should be one!
            {
                
                int type = (int)policyRow["Type"];

                var nextPolicyId = policyRow["NextPolicyId"];
               
                  


                //------Create
                DAL_PolicyParameter dpp = new DAL_PolicyParameter();
                DataTable policyParameterTBL = dpp.GetPolicyParameter(id);
                foreach (DataRow policyParameterRow in policyParameterTBL.Rows)//should also be one!!
                {
                    var seniorityInDays = policyParameterRow["seniorityInDays"]; 
                    var numOfMessages = policyParameterRow["numOfMessages"];
                    var numOfComplaints=policyParameterRow["numOfComplaints"]; 
                    bool blockPassword = (bool)policyParameterRow["blockPassword"];
                    var maxModerators = policyParameterRow["maxModerators"];
                    var minAge = policyParameterRow["minAge"];
                    var minSeniority = policyParameterRow["minSeniority"];
                    var requiredLength = policyParameterRow["requiredLength"];
                    var passwordValidity = policyParameterRow["passwordValidity"];
                    var maxNumOfUsers = policyParameterRow["maxNumOfUsers"];
                    bool moderatorDeletePermission = (bool)policyParameterRow["moderatorDeletePermission"];

                     switch ((Policies)type)
                     {
                         case Policies.Password:
                             policy = PasswordPolicy.createPasswordPolicyForInit((int)requiredLength, (int)passwordValidity);
                             break;
                         case Policies.Authentication:
                            policy = AuthenticationPolicy.createAuthenticationPolicyForInit();
                             break;
                         case Policies.ModeratorSuspension:
                            policy = ModeratorSuspensionPolicy.createModeratorSuspensionPolicyForInit((int)numOfComplaints);
                             break;
                         case Policies.Confidentiality:
                            policy = ConfidentialityPolicy.createConfidentialityPolicyForInit(blockPassword);
                             break;
                         case Policies.ModeratorAppointment:
                            policy = ModeratorAppointmentPolicy.createModeratorAppointmentPolicyForInit((int)seniorityInDays, (int)numOfMessages, (int)numOfComplaints);
                             break;
                         case Policies.AdminAppointment:
                            policy = AdminAppointmentPolicy.createAdminAppointmentPolicyForInit((int)seniorityInDays, (int)numOfMessages, (int)numOfComplaints);
                            break;
                         case Policies.MemberSuspension:
                            policy = MemberSuspensionPolicy.createMemberSuspensionPolicyForInit((int)numOfComplaints);
                             break;
                         case Policies.UsersLoad:
                            policy = UsersLoadPolicy.createUsersLoadPolicyForInit((int)maxNumOfUsers);
                             break;
                         case Policies.MinimumAge:
                            policy = MinimumAgePolicy.createMinimumAgePolicyForInit((int)minAge);
                             break;
                         case Policies.MaxModerators:
                            policy = MaxModeratorsPolicy.createMaxModeratorsPolicyForInit((int)maxModerators);
                             break;
                         case Policies.ModeratorSeniority:
                            policy = ModeratorSeniorityPolicy.createModeratorSeniorityPolicyForInit((int)minSeniority);
                             break;
                         case Policies.ModeratorPermissionToDelete:
                            policy = ModeratorDeletePermissionPolicy.createmoderatorDeletePermissionForInit(moderatorDeletePermission);
                             break;
                         default:
                             break;
                     }

                    policy.type = (Policies)type;
                    policy.id = id;

                    if (nextPolicyId != DBNull.Value)
                        policy.nextPolicy = populatePolicy((int)nextPolicyId);

                     


                }
            }

            return policy;


        }
        public Policies Type { get { return type; } set { this.type = value; } }
        public Policy NextPolicy { get { return nextPolicy; } set { this.nextPolicy = value; } }

        public int ID { get { return id; } }
        /// <summary>
        /// Check the params given according to the forum policies
        /// </summary>
        public virtual bool CheckPolicy(PolicyParametersObject param)
        {
            if(nextPolicy!=null)
               return nextPolicy.CheckPolicy(param);
            return true;//TODO: check this - no policy specified
        }
        

        /// <summary>
        /// Add new policy to the end of the chain
        /// </summary>
        /// <param name="newPolicy"></param>
        public bool AddPolicy(Policy newPolicy)
        {
            if (CheckIfPolicyExists(newPolicy.type))
                return false;
            AddPolicyHelper(newPolicy);
            return true;
        }

        private void AddPolicyHelper(Policy newPolicy)
        {
            if (nextPolicy != null)
                nextPolicy.AddPolicyHelper(newPolicy);
            else
            {
                this.nextPolicy = newPolicy;
                dal_policy.SetNextPolicy(id, newPolicy.id);
            }
        }

        public bool CheckIfPolicyExists(Policies type)
        {
            if (this.type == type)
                return true;
            if (this.nextPolicy == null)
                return false;
            return this.nextPolicy.CheckIfPolicyExists(type);
        }
        /// <summary>
        /// Removes the policy of the given type from the chain
        /// </summary>
        /// <param name="type"></param>
        /// <returns>the new head of the chain. returns NULL if the new chain is empty</returns>
        public Policy RemovePolicy(Policies type)
        {
            if (this.type == type)//if the first node is the one to be removed
            {
                dal_policy.DeletePolicy(id);
                return this.nextPolicy;  
            }
            RemovePolicyHelper(type);
            return this;
        }

        private void RemovePolicyHelper(Policies type)
        {
            if (this.NextPolicy.Type == type)
            {
                int idToRemove = this.NextPolicy.id;
                Policy temp = this.NextPolicy.NextPolicy;
                if(temp == null)
                    dal_policy.SetNextPolicy(id, -1);
                else
                    dal_policy.SetNextPolicy(id, temp.id);
                dal_policy.DeletePolicy(idToRemove);
                this.NextPolicy = temp;
                return;
            }
            if (nextPolicy == null)
                return;

                //return 
                this.nextPolicy.RemovePolicyHelper(type);
        }
    }
}
