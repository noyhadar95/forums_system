using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System;

namespace WebApplication
{
    public class MyTreeNode : TreeNode, IPostBackEventHandler
    {


        #region Constructors


        public MyTreeNode()
        {
        }


        public MyTreeNode(string text) : base(text)
        {
            
            this.SelectAction = TreeNodeSelectAction.Select;
        }


        #endregion


        #region Protected methods


        protected override void RenderPostText(HtmlTextWriter writer)
        {
            Page page = HttpContext.Current.CurrentHandler as Page;
            ClientScriptManager csm = page.ClientScript;
            Control mainContent = page.FindControl("TreeView1");

            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, csm.GetPostBackEventReference(mainContent, "Delete"));
            writer.RenderBeginTag(HtmlTextWriterTag.Button);
            writer.Write("Delete");
            writer.RenderEndTag();

            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, csm.GetPostBackEventReference(mainContent, "Edit"));
            writer.RenderBeginTag(HtmlTextWriterTag.Button);
            writer.Write("Edit");
            writer.RenderEndTag();

            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, csm.GetPostBackEventReference(mainContent, "Reply"));
            writer.RenderBeginTag(HtmlTextWriterTag.Button);
            writer.Write("Reply");
            writer.RenderEndTag();
        }


        #endregion


        #region event handlers


        public void RaisePostBackEvent(string eventArgument)
        {
            if ("Delete" == eventArgument)
            {
                OnClickDelete(EventArgs.Empty);
            }
            else if ("Edit" == eventArgument)
            {
                OnClickEdit(EventArgs.Empty);
            }
            else if ("Reply" == eventArgument)
            {
                OnClickReply(EventArgs.Empty);
            }
        }

        private void OnClickReply(EventArgs empty)
        {
            throw new NotImplementedException();
        }

        private void OnClickEdit(EventArgs empty)
        {
            throw new NotImplementedException();
        }

        private void OnClickDelete(EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion


    }

}