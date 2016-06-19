﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication.Communication;
using WebApplication.Resources.UserManagement.DomainLayer;

namespace WebApplication
{
    public partial class ForumPage : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string forumName = Request.QueryString["forumName"];
                Label myLabel = this.FindControl("Label1") as Label;
                myLabel.Text = forumName;
                ListBox1.Items.Clear();
                ICL cl = new CL();
                List<string> items = cl.GetSubForumsList(forumName);
                for (int i = 0; i < items.Count; i++)
                {
                    ListBox1.Items.Add(new ListItem(items.ElementAt(i)));
                }
                LabelLogin.Visible = false;
                BtnLogout.Visible = false;
                BtnLogout.Enabled = false;
                if(Session["Data"]== null)
                    Session["Data"] = "";
            }
            if (Session["Data"].Equals(""))
            {
                LabelLogin.Visible = false;
                BtnLogout.Visible = false;
                BtnLogout.Enabled = false;
            }
            else
            {
                LabelUserName.Visible = false;
                LabelPassword.Visible = false;
                TextBox1.Visible = false;
                TextBox2.Visible = false;
                Button1.Visible = false;
                Button1.Enabled = false;
                LabelLogin.Text = "logged in as " + Session["Data"];
                LabelLogin.Visible = true;
                BtnLogout.Visible = true;
                BtnLogout.Enabled = true;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string forumName = Request.QueryString["forumName"];
            string userName = TextBox1.Text;
            string password = TextBox2.Text;
            if (userName == "" || password == "")
            {
                userName = "";
                displayMessage("user name or password inncorrect");
                return;
            }
            ICL cl = new CL();
            User user = cl.MemberLogin(forumName, userName, password);
            if(user == null)
            {
                userName = "";
                displayMessage("user name or password inncorrect");
                return;
            }
            LabelUserName.Visible = false;
            LabelPassword.Visible = false;
            TextBox1.Visible = false;
            TextBox2.Visible = false;
            Button1.Visible = false;
            Button1.Enabled = false;
            LabelLogin.Text = "logged in as " + userName;
            LabelLogin.Visible = true;
            BtnLogout.Visible = true;
            BtnLogout.Enabled = true;

            Session["Data"] = userName;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string forumName = Request.QueryString["forumName"];
            if (ListBox1.SelectedItem != null)
                Response.Redirect("SubForumPage.aspx?forumName="+forumName+"&subforumName=" + ListBox1.SelectedItem.Text);
        }

        private void displayMessage(string Message)
        {
            Page.ClientScript.RegisterStartupScript(
             this.GetType(),
             "Scripts",
             "<script language='javascript'>alert('" + Message + "');</script>");
        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            ICL cl = new CL();
            cl.MemberLogout(Request.QueryString["forumName"], (string)Session["Data"]);
            LabelUserName.Visible = true;
            LabelPassword.Visible = true;
            TextBox1.Visible = true;
            TextBox2.Visible = true;
            Button1.Visible = true;
            Button1.Enabled = true;
            LabelLogin.Visible = false;
            BtnLogout.Visible = false;
            BtnLogout.Enabled = false;

            Session["Data"] = "";
        }
    }
}