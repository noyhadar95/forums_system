using ForumsSystemClient.CommunicationLayer;
using ForumsSystemClient.Resources.ForumManagement.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ForumsSystemClient.PresentationLayer
{
    /// <summary>
    /// Interaction logic for EditForumPoliciesWindow.xaml
    /// </summary>
    public partial class EditForumPoliciesWindow : Window
    {
        private CL cl;
        private Dictionary<CheckBox, Grid> policiesCBGridDict;

        public EditForumPoliciesWindow()
        {
            InitializeComponent();

            WindowHelper.SetWindowBGImg(this);

            cl = new CL();

            // initialize forums list
            List<string> items = cl.GetForumsList();
            forumsListView.ItemsSource = items;

            // handle policies
            HidePoliciesGrids();
            policiesCBGridDict = new Dictionary<CheckBox, Grid>();
            InitPoliciesDict();
            InitPoliciesCB();
        }

        private void InitPoliciesCB()
        {
            // password
            InitComboBox(passwordLengthCB, AddForumWindow.MAX_PASS_LENGTH);
            InitComboBox(passwordValidityCB, AddForumWindow.MAX_PASS_VALIDITY);

            // mod appointment
            InitComboBox(modSeniorityCB, AddForumWindow.MAX_MOD_SENIORITY);
            InitComboBox(modNumOfMessagesCB, AddForumWindow.MAX_MOD_MSGS);
            InitComboBox(modNumOfComplaintsCB, AddForumWindow.MAX_MOD_APP_COMPLAINTS);

            // admin appointment
            InitComboBox(adminSeniorityCB, AddForumWindow.MAX_ADMIN_SENIORITY);
            InitComboBox(adminNumOfMessagesCB, AddForumWindow.MAX_ADMIN_MSGS);
            InitComboBox(adminNumOfComplaintsCB, AddForumWindow.MAX_ADMIN_COMPLAINTS);

            // mod suspension
            InitComboBox(modSuspNumOfComplCB, AddForumWindow.MAX_MOD_SUSP_COMPLAINTS);

            // member suspension
            InitComboBox(memberSuspNumOfComplCB, AddForumWindow.MAX_MEMBER_SUSP_COMPLAINTS);

            // users load
            InitComboBox(maxUsersCB, AddForumWindow.MAX_USERS);

            // minimum age
            InitComboBox(minAgeCB, AddForumWindow.MIN_AGE);

            // max moderators
            InitComboBox(maxModsCB, AddForumWindow.MAX_MODS);

        }

        private void InitComboBox(ComboBox cb, int maxVal)
        {
            for (int i = 1; i <= maxVal; i++)
            {
                cb.Items.Add(i);
            }
            cb.SelectedIndex = 0;
        }

        private void InitPoliciesDict()
        {
            policiesCBGridDict.Add(cbPassword, gridPassword);
            policiesCBGridDict.Add(cbConfidentiality, gridConfidentiality);
            policiesCBGridDict.Add(cbModeratorAppointment, gridModeratorAppointment);
            policiesCBGridDict.Add(cbAdminAppointment, gridAdminAppointment);
            policiesCBGridDict.Add(cbModeratorSuspension, gridModeratorSuspension);
            policiesCBGridDict.Add(cbMemberSuspension, gridMemberSuspension);
            policiesCBGridDict.Add(cbUsersLoad, gridUsersLoad);
            policiesCBGridDict.Add(cbMinimumAge, gridMinimumAge);
            policiesCBGridDict.Add(cbMaxModerators, gridMaxModerators);
            policiesCBGridDict.Add(cbModeratorPermissionToDelete, gridModeratorPermissionToDelete);
            policiesCBGridDict.Add(cbInteractivePolicy, gridInteractivePolicy);
        }

        private void HidePoliciesGrids()
        {
            gridPolicies.Children.Remove(gridPassword);
            gridPolicies.Children.Remove(gridConfidentiality);
            gridPolicies.Children.Remove(gridModeratorAppointment);
            gridPolicies.Children.Remove(gridAdminAppointment);
            gridPolicies.Children.Remove(gridModeratorSuspension);
            gridPolicies.Children.Remove(gridMemberSuspension);
            gridPolicies.Children.Remove(gridUsersLoad);
            gridPolicies.Children.Remove(gridMinimumAge);
            gridPolicies.Children.Remove(gridMaxModerators);
            gridPolicies.Children.Remove(gridModeratorPermissionToDelete);
            gridPolicies.Children.Remove(gridInteractivePolicy);
        }

        private void HandleCheckedPolicy(CheckBox cb, Grid grid)
        {
            int cb_index = sp_policies.Children.IndexOf(cb);
            gridMain.Children.Remove(grid);
            sp_policies.Children.Insert(cb_index + 1, grid);
            sp_policies.Height = sp_policies.Height + grid.Height;
            grid.Visibility = Visibility.Visible;
            grid.Margin = new Thickness(1);
        }

        private void cbPolicy_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            Grid grid = policiesCBGridDict[cb];
            HandleCheckedPolicy(cb, grid);
        }

        private void cbPolicy_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            Grid grid = policiesCBGridDict[cb];
            sp_policies.Children.Remove(grid);
            sp_policies.Height = sp_policies.Height - grid.Height;
        }

        private void forumsListView_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                //LoadForumPolicies((string)item);
            }
        }

        private void LoadForumPolicies(string forumName)
        {
            Forum forum = cl.GetForum(forumName);
            Policy policy = forum.GetPolicy();

            while (policy != null)
            {
                MessageBox.Show(policy.Type.ToString());

                switch (policy.Type)
                {
                    case Policies.Password:
                        cbPassword.IsChecked = true;
                        cbPolicy_Checked(cbPassword, null);
                        passwordLengthCB.SelectedItem = ((PasswordPolicy)policy).RequiredLength;
                        passwordValidityCB.SelectedItem = ((PasswordPolicy)policy).PasswordValidity;
                        break;
                    case Policies.Authentication:
                        cbAuthentication.IsChecked = true;
                        break;
                    case Policies.ModeratorSuspension:
                        cbModeratorSuspension.IsChecked = true;
                        cbPolicy_Checked(cbModeratorSuspension, null);
                        modSuspNumOfComplCB.SelectedItem = ((ModeratorSuspensionPolicy)policy).NumOfComplaints;
                        break;
                    case Policies.Confidentiality:
                        cbConfidentiality.IsChecked = true;
                        cbPolicy_Checked(cbConfidentiality, null);
                        confidentialityBlockPassCB.SelectedItem = ((ConfidentialityPolicy)policy).BlockPassword;
                        break;
                    case Policies.ModeratorAppointment:
                        cbModeratorAppointment.IsChecked = true;
                        cbPolicy_Checked(cbModeratorAppointment, null);
                        modSeniorityCB.SelectedItem = ((ModeratorAppointmentPolicy)policy).SeniorityInDays;
                        modNumOfMessagesCB.SelectedItem = ((ModeratorAppointmentPolicy)policy).NumOfMessages;
                        modNumOfComplaintsCB.SelectedItem = ((ModeratorAppointmentPolicy)policy).NumOfComplaints;
                        break;
                    case Policies.AdminAppointment:
                        break;
                    case Policies.MemberSuspension:
                        break;
                    case Policies.UsersLoad:
                        break;
                    case Policies.MinimumAge:
                        break;
                    case Policies.MaxModerators:
                        break;
                    default:
                        break;
                }
                policy = policy.NextPolicy;
            }

        }

        private Policy GetForumPolicy()
        {
            List<Policy> policyList = new List<Policy>();

            if (cbPassword.IsChecked == true)
            {
                policyList.Add(new PasswordPolicy(Policies.Password, (int)passwordLengthCB.SelectionBoxItem, (int)passwordValidityCB.SelectionBoxItem));
            }
            if (cbAuthentication.IsChecked == true)
            {
                policyList.Add(new AuthenticationPolicy(Policies.Authentication));
            }
            if (cbConfidentiality.IsChecked == true)
            {
                policyList.Add(new ConfidentialityPolicy(Policies.Confidentiality, WordToBool((string)confidentialityBlockPassCB.SelectionBoxItem)));
            }
            if (cbModeratorAppointment.IsChecked == true)
            {
                policyList.Add(new ModeratorAppointmentPolicy(Policies.ModeratorAppointment, (int)modSeniorityCB.SelectionBoxItem, (int)modNumOfMessagesCB.SelectionBoxItem,
                    (int)modNumOfComplaintsCB.SelectionBoxItem));
            }
            if (cbAdminAppointment.IsChecked == true)
            {
                policyList.Add(new AdminAppointmentPolicy(Policies.AdminAppointment, (int)adminSeniorityCB.SelectionBoxItem, (int)adminNumOfMessagesCB.SelectionBoxItem,
                    (int)adminNumOfComplaintsCB.SelectionBoxItem));
            }
            if (cbModeratorSuspension.IsChecked == true)
            {
                policyList.Add(new ModeratorSuspensionPolicy(Policies.ModeratorSuspension, (int)modSuspNumOfComplCB.SelectionBoxItem));
            }
            if (cbMemberSuspension.IsChecked == true)
            {
                policyList.Add(new MemberSuspensionPolicy(Policies.MemberSuspension, (int)memberSuspNumOfComplCB.SelectionBoxItem));
            }
            if (cbUsersLoad.IsChecked == true)
            {
                policyList.Add(new UsersLoadPolicy(Policies.UsersLoad, (int)maxUsersCB.SelectionBoxItem));
            }
            if (cbMinimumAge.IsChecked == true)
            {
                policyList.Add(new MinimumAgePolicy(Policies.MinimumAge, (int)minAgeCB.SelectionBoxItem));
            }
            if (cbMaxModerators.IsChecked == true)
            {
                policyList.Add(new MaxModeratorsPolicy(Policies.MaxModerators, (int)maxModsCB.SelectionBoxItem));
            }
            if (cbModeratorPermissionToDelete.IsChecked == true)
            {
                policyList.Add(new ModeratorDeletePermissionPolicy(Policies.ModeratorPermissionToDelete, WordToBool((string)modPerToDeleteCB.SelectionBoxItem)));
            }
            if (cbInteractivePolicy.IsChecked == true)
            {
                int notifMode = 0;
                switch ((string)interactivePolicyCB.SelectionBoxItem)
                {
                    case "online only":
                        notifMode = 0;
                        break;
                    case "offline and online":
                        notifMode = 1;
                        break;
                    case "selective":
                        notifMode = 2;
                        break;
                    default:
                        break;
                }
                policyList.Add(new InteractivePolicy(Policies.InteractivePolicy, notifMode));
            }

            // check if no policy has been chosen
            if (policyList.Count == 0)
                return null;

            Policy policyHead = policyList[0];
            Policy policyTail = policyHead;
            int i = 1;
            while (i < policyList.Count)
            {
                policyTail.NextPolicy = policyList[i];
                policyTail = policyTail.NextPolicy;
                i++;
            }

            return policyHead;
        }

        private bool WordToBool(string word)
        {
            if (word == "yes")
                return true;
            else return false;
        }


        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            string forumName = null;
            var item = forumsListView.SelectedItem;
            if (item != null)
            {
                forumName = (string)item;
            }

            if (forumName == null)
            {
                MessageBox.Show("please choose a forum to edit");
                return;
            }
            if (!WindowHelper.IsLoggedSuperAdmin())
            {
                MessageBox.Show("error: super admin is not logged in");
                return;
            }
            SuperAdmin sa = WindowHelper.GetLoggedSuperAdmin();
            Policy policy = GetForumPolicy();
            cl.ChangeForumProperties(sa.userName, forumName, policy);

            WindowHelper.SwitchWindow(this, new MainWindow());
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new MainWindow());
        }

    }
}
