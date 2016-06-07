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
        private TextBox pTextBoxBody;
        private Button BtnReply;
        private Button BtnSave;
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


        public MyTreeNode(string title,string body,int id) : base()
        {
            this.postId = id;
            this.id = nextId;
            nextId++;

            pTextBoxTitle=CreateTextBox(title);
            pTextBoxTitle.Enabled = false;
            pTextBoxBody = CreateTextBox(body);
            pTextBoxBody.Enabled = false;
            pTextBoxTitle.BorderWidth = 1;
            pTextBoxBody.BorderWidth = 1;
            pTextBoxTitle.BorderColor = System.Drawing.Color.AliceBlue;
            pTextBoxBody.BorderColor = System.Drawing.Color.White;
            pTextBoxTitle.BackColor = System.Drawing.Color.AliceBlue;
            pTextBoxTitle.BackColor = System.Drawing.Color.CadetBlue;
            pTextBoxTitle.ForeColor = System.Drawing.Color.Black;
            pTextBoxBody.ForeColor = System.Drawing.Color.Black;
            pTextBoxTitle.Width = 500;
            pTextBoxBody.Width = 500;
            pTextBoxTitle.Font.Bold = true;

            BtnReply = CreateButton("reply");
            BtnSave = CreateButton("save");
            BtnSave.Enabled = false;
            BtnSave.Visible = false;

            this.SelectAction = TreeNodeSelectAction.Select;
        }

        public MyTreeNode(string title, string body) : base()
        {
            this.id = nextId;
            nextId++;

            pTextBoxTitle = CreateTextBox(title);
            pTextBoxTitle.Enabled = false;
            pTextBoxBody = CreateTextBox(body);
            pTextBoxBody.Enabled = false;
            pTextBoxTitle.BorderWidth = 1;
            pTextBoxBody.BorderWidth = 1;
            pTextBoxTitle.BorderColor = System.Drawing.Color.AliceBlue;
            pTextBoxBody.BorderColor = System.Drawing.Color.White;
            pTextBoxTitle.BackColor = System.Drawing.Color.AliceBlue;
            pTextBoxTitle.BackColor = System.Drawing.Color.CadetBlue;
            pTextBoxTitle.ForeColor = System.Drawing.Color.Black;
            pTextBoxBody.ForeColor = System.Drawing.Color.Black;
            pTextBoxTitle.Width = 500;
            pTextBoxBody.Width = 500;
            pTextBoxTitle.Font.Bold = true;

            BtnReply = CreateButton("reply");
            BtnSave = CreateButton("save");
            BtnSave.Enabled = false;
            BtnSave.Visible = false;

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
            if (pTextBoxBody != null)
                pTextBoxBody.RenderControl(writer);
            l.RenderControl(writer);    

            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, csm.GetPostBackEventReference(page, "Reply@"+postId+ ""));
            BtnReply = CreateButton("reply");
            BtnReply.RenderControl(writer);

            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, csm.GetPostBackEventReference(page, "Save@" + postId + ""));
            BtnSave = CreateButton("save");
            BtnSave.RenderControl(writer);

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


        private Button CreateButton(string text)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.BackColor = System.Drawing.Color.Transparent;
            btn.Font.Underline = true;
            btn.ForeColor = System.Drawing.Color.Blue;
            btn.BorderWidth = 0;
            return btn;
        }
        #endregion

        public void enableTextBoxes()
        {
            pTextBoxBody.Enabled = true;
            pTextBoxTitle.Enabled = true;
                       
        }

        public void enableButtonSave(bool b)
        {
            BtnSave.Enabled = b;
            BtnSave.Visible = b;
        }

        public string GetTitle()
        {
            return pTextBoxTitle.Text;
        }

        public string GetBody()
        {
            return pTextBoxBody.Text;
        }

    }

}