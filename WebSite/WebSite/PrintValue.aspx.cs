using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSite
{
    public partial class PrintValue : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string value = Request.QueryString["value"];
                if (!string.IsNullOrEmpty(value))
                {
                    form1.InnerText = value;
                    int waitTime = 500;

                    ClientScript.RegisterStartupScript(this.GetType(), "AutoRefresh", "setTimeout(function(){ window.location.href = 'https://localhost:{port}/api/values'; }, " + waitTime + ");", true);
                    ClientScript.RegisterStartupScript(this.GetType(), "AutoRefresh", "setTimeout(function(){ window.close(); window.open('https://localhost:{port}/api/values', '_blank'); }, " + waitTime + ");", true);
                }
                else
                {
                    // Handle case where value is not provided
                    Response.Redirect("https://localhost:{port}/api/values");
                }
            }
        }

    }
}