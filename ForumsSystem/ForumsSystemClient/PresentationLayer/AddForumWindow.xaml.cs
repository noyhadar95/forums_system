using ForumsSystemClient.CommunicationLayer;
using ForumsSystemClient.Resources.ForumManagement.DomainLayer;
using ForumsSystemClient.Resources.UserManagement.DomainLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AddForumWindow.xaml
    /// </summary>
    public partial class AddForumWindow : Window
    {
        // password
        public static int MAX_PASS_LENGTH = 12;
        public static int MAX_PASS_VALIDITY = 30; // in days
        // mod appointment
        public static int MAX_MOD_SENIORITY = 30;
        public static int MAX_MOD_MSGS = 12;
        public static int MAX_MOD_APP_COMPLAINTS = 5;
        // admin appointment
        public static int MAX_ADMIN_SENIORITY = 30;
        public static int MAX_ADMIN_MSGS = 12;
        public static int MAX_ADMIN_COMPLAINTS = 5;
        // mod suspension
        public static int MAX_MOD_SUSP_COMPLAINTS = 10;
        // member suspension
        public static int MAX_MEMBER_SUSP_COMPLAINTS = 10;
        // users load
        public static int MAX_USERS = 100;
        // minimum age
        public static int MIN_AGE = 21;
        // max moderators
        public static int MAX_MODS = 5;


        private CL cl;
        private ObservableCollection<string> adminsLVItems;
        private List<User> admins;
        private Dictionary<CheckBox, Grid> policiesCBGridDict;

        public AddForumWindow()
        {
            InitializeComponent();
            WindowHelper.SetWindowBGImg(this);

            cl = new CL();
            adminsLVItems = new ObservableCollection<string>();
            admins = new List<User>();
            adminsListView.ItemsSource = adminsLVItems;

            // handle policies
            HidePoliciesGrids();
            policiesCBGridDict = new Dictionary<CheckBox, Grid>();
            InitPoliciesDict();
            InitPoliciesCB();

        }

        private void InitPoliciesCB()
        {
            // password
            InitComboBox(passwordLengthCB, MAX_PASS_LENGTH);
            InitComboBox(passwordValidityCB, MAX_PASS_VALIDITY);

            // mod appointment
            InitComboBox(modSeniorityCB, MAX_MOD_SENIORITY);
            InitComboBox(modNumOfMessagesCB, MAX_MOD_MSGS);
            InitComboBox(modNumOfComplaintsCB, MAX_MOD_APP_COMPLAINTS);

            // admin appointment
            InitComboBox(adminSeniorityCB, MAX_ADMIN_SENIORITY);
            InitComboBox(adminNumOfMessagesCB, MAX_ADMIN_MSGS);
            InitComboBox(adminNumOfComplaintsCB, MAX_ADMIN_COMPLAINTS);

            // mod suspension
            InitComboBox(modSuspNumOfComplCB, MAX_MOD_SUSP_COMPLAINTS);

            // member suspension
            InitComboBox(memberSuspNumOfComplCB, MAX_MEMBER_SUSP_COMPLAINTS);

            // users load
            InitComboBox(maxUsersCB, MAX_USERS);

            // minimum age
            InitComboBox(minAgeCB, MIN_AGE);

            // max moderators
            InitComboBox(maxModsCB, MAX_MODS);

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
        }

        public void AddAdmin(User admin)
        {
            admins.Add(admin);
            adminsLVItems.Add(((User)admin).Username);
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            string forumName = nameTB.Text;

            if (forumName == "")
            {
                MessageBox.Show("please enter the name of the forum");
                return;
            }
            if (admins.Count == 0)
            {
                MessageBox.Show("please add at least 1 admin for the forum");
                return;
            }
            if (!WindowHelper.IsLoggedSuperAdmin())
            {
                MessageBox.Show("error: super admin is not logged in");
                return;
            }
            SuperAdmin creator = WindowHelper.GetLoggedSuperAdmin();

            Policy policy = GetForumPolicy();
           
            Forum forum = cl.CreateForum(creator.userName, creator.password, forumName, policy, admins);
            if (forum != null)
                WindowHelper.SwitchWindow(this, new MainWindow());
            else
                MessageBox.Show("forum creation was unsuccessfull, please try again");
        }

        private Policy GetForumPolicy()
        {
            List<Policy> policyList = new List<Policy>();

            if (cbPassword.IsChecked == true)
            {
                policyList.Add(new PasswordPolicy(Policies.Password, (int)passwordLengthCB.SelectedItem, (int)passwordValidityCB.SelectedItem));
            }
            if (cbAuthentication.IsChecked == true)
            {
                policyList.Add(new AuthenticationPolicy(Policies.Authentication));
            }
            if (cbConfidentiality.IsChecked == true)
            {
                policyList.Add(new ConfidentialityPolicy(Policies.Confidentiality, (bool)confidentialityBlockPassCB.SelectedItem));
            }
            if (cbModeratorAppointment.IsChecked == true)
            {
                policyList.Add(new ModeratorAppointmentPolicy(Policies.ModeratorAppointment, (int)modSeniorityCB.SelectedItem, (int)modNumOfMessagesCB.SelectedItem,
                    (int)modNumOfComplaintsCB.SelectedItem));
            }
            if (cbAdminAppointment.IsChecked == true)
            {
                policyList.Add(new AdminAppointmentPolicy(Policies.AdminAppointment, (int)adminSeniorityCB.SelectedItem, (int)adminNumOfMessagesCB.SelectedItem,
                    (int)adminNumOfComplaintsCB.SelectedItem));
            }
            if (cbModeratorSuspension.IsChecked == true)
            {
                policyList.Add(new ModeratorSuspensionPolicy(Policies.ModeratorSuspension, (int)modSuspNumOfComplCB.SelectedItem));
            }
            if (cbMemberSuspension.IsChecked == true)
            {
                policyList.Add(new MemberSuspensionPolicy(Policies.MemberSuspension, (int)memberSuspNumOfComplCB.SelectedItem));
            }
            if (cbUsersLoad.IsChecked == true)
            {
                policyList.Add(new UsersLoadPolicy(Policies.UsersLoad, (int)maxUsersCB.SelectedItem));
            }
            if (cbMinimumAge.IsChecked == true)
            {
                policyList.Add(new MinimumAgePolicy(Policies.MinimumAge, (int)minAgeCB.SelectedItem));
            }
            if (cbMaxModerators.IsChecked == true)
            {
                policyList.Add(new MaxModeratorsPolicy(Policies.MaxModerators, (int)maxModsCB.SelectedItem));
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


        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowHelper.SwitchWindow(this, new MainWindow());
        }

        private void addAdminBtn_Click(object sender, RoutedEventArgs e)
        {
            // open a new window without closing this one
            WindowHelper.ShowWindow(this, new AddAdminWindow(this));
        }

        private void removeAdminBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = adminsListView.SelectedItems;
            List<string> selectedItemsCopy = new List<string>();
            foreach (string item in selectedItems)
            {
                selectedItemsCopy.Add(item);
            }
            foreach (string selectedItem in selectedItemsCopy)
            {
                adminsLVItems.Remove(selectedItem);
                // remove admin from admins list
                foreach (User a in admins)
                {
                    if (a.Username == selectedItem)
                    {
                        admins.Remove(a);
                        break;
                    }
                }
            }
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

    }
}
