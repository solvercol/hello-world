using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ASP.NETCLIENTE.UI;

namespace Modules.Reclamos.UserControls
{
    public partial class WucRenderMessagesError : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void RenderMessages(IEnumerable<string> items)
        {
            var mainList = new HtmlGenericControl("ul");
            mainList.Attributes.Add("class", "validator");
            foreach (var item in items)
            {
                var listItem = new HtmlGenericControl("li");
                var lit = new Literal() { Text = item };
                listItem.Controls.Add(lit);
                mainList.Controls.Add(listItem);
            }

            phlError.Controls.Add(mainList);
        }
    }
}