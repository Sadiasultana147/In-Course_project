using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Exam
{
    public partial class WebUserControl2 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ListView1_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            FileUpload fu = ListView1.EditItem.FindControl("FileUpload1") as FileUpload;
            if (fu.HasFile)
            {
                if (fu.PostedFile.ContentLength > 0)
                {
                    string ext = Path.GetExtension(fu.PostedFile.FileName);
                    string f = Guid.NewGuid() + ext;
                    fu.PostedFile.SaveAs(Server.MapPath("~/Uploads/") + f);
                    e.NewValues["Picture"] = f;
                }
            }
        }

        protected void ListView1_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            FileUpload fu = e.Item.FindControl("FileUpload1") as FileUpload;
            if (fu.HasFile)
            {
                if (fu.PostedFile.ContentLength > 0)
                {
                    string ext = Path.GetExtension(fu.PostedFile.FileName);
                    string f = Guid.NewGuid() + ext;
                    fu.PostedFile.SaveAs(Server.MapPath("~/Uploads/") + f);
                    e.Values["Picture"] = f;
                }
            }
        }
    }
}