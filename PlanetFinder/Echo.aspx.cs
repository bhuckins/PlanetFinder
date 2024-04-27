using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PlanetFinder
{
    public partial class Echo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEcho_Click(object sender, EventArgs e)
        {
            txtEchoOutput.Text = txtEchoInput.Text;
        }
    }
}