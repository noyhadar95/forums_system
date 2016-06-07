using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System;

namespace WebApplication
{
    public class MyTreeNode : TreeNode
    {
        private static int nextId;
        private int id;
        private int postId;
        private TextBox pTextBoxTitle;
        private TextBox pTextBoxUser;
        private TextBox pTextBoxBody;
        private Button BtnReply;
        #region Constructors

        private TextBox CreateTextBox(string text)
        {
            TextBox pTextBox = new TextBox();
            pTextBox.ID = String.Format("TextBox_{0}", this.DataPath);
            pTextBox.Text = text;
            return pTextBox;
        }

        public MyTreeNode()
        {
            nextId = 1;
        }


        public MyTreeNode(string title,string body,string userName,int id,bool v) : base()
        {
            this.postId = id;
            this.id = nextId;
            nextId++;

            pTextBoxTitle=CreateTextBox(title);
            pTextBoxTitle.Enabled = false;
            pTextBoxBody = CreateTextBox(body);
            pTextBoxBody.Enabled = false;
            pTextBoxUser = CreateTextBox("by " + userName);
            pTextBoxUser.Enabled = false;
            pTextBoxTitle.BorderWidth = 1;
            pTextBoxBody.BorderWidth = 1;
            pTextBoxUser.BorderWidth = 1;
            pTextBoxTitle.BorderColor = System.Drawing.Color.AliceBlue;
            pTextBoxBody.BorderColor = System.Drawing.Color.White;
            pTextBoxUser.BorderColor = System.Drawing.Color.AliceBlue;
            pTextBoxUser.BackColor = System.Drawing.Color.LightBlue;
            pTextBoxTitle.BackColor = System.Drawing.Color.AliceBlue;
            pTextBoxTitle.BackColor = System.Drawing.Color.CadetBlue;
            pTextBoxTitle.ForeColor = System.Drawing.Color.Black;
            pTextBoxUser.ForeColor = System.Drawing.Color.Black;
            pTextBoxBody.ForeColor = System.Drawing.Color.Black;
            pTextBoxUser.Width = 600;
            pTextBoxTitle.Width = 600;
            pTextBoxBody.Width = 600;
            pTextBoxUser.Font.Size = FontUnit.XSmall;
            pTextBoxTitle.Font.Bold = true;

            BtnReply = CreateButton("reply",v);
            BtnReply.Visible = v;
            this.SelectAction = TreeNodeSelectAction.Select;
        }
        public void SetId(int id)
        {
            this.postId = id;
        }

        #endregion


        #region Protected methods

        protected override void RenderPostText(HtmlTextWriter writer)
        {
            Page page = HttpContext.Current.CurrentHandler as Page;
            ClientScriptManager csm = page.ClientScript;

            base.RenderPostText(writer);
            if (pTextBoxTitle != null)
                pTextBoxTitle.RenderControl(writer);
            Label l = new Label();
            l.Text = "<br/>";
            l.RenderControl(writer);
            pTextBoxUser.RenderControl(writer);
            l.RenderControl(writer);
            if (pTextBoxBody != null)
                pTextBoxBody.RenderControl(writer);
            l.RenderControl(writer);
          
          
            BtnReply.Attributes.Add("Onclick", csm.GetPostBackEventReference(page, "Reply@" + postId + ""));
            BtnReply.RenderControl(writer);

            base.RenderPostText(writer);
         
        }

        public int GetPostId()
        {
            return postId;
        }

        public int GetId()
        {
            return id;
        }
        #endregion

        public Button GetReplyBtn()
        {
            return BtnReply;
        }

        #region event handlers


        private Button CreateButton(string text,bool v)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.BackColor = System.Drawing.Color.Transparent;
            btn.Font.Underline = true;
            btn.ForeColor = System.Drawing.Color.Blue;
            btn.BorderWidth = 0;
            btn.Visible = v;
            btn.Enabled = v;
            return btn;
        }
        #endregion


        public string GetTitle()
        {
            return pTextBoxTitle.Text;
        }

        public string GetBody()
        {
            return pTextBoxBody.Text;
        }

        public TextBox GetTitleTextBox()
        {
            return pTextBoxTitle;
        }

        public TextBox GetBodyTextBox()
        {
            return pTextBoxBody;
        }
    }

}