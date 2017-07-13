using System;

namespace AverageNegative
{
	public class BasePage : System.Web.UI.Page
	{
		protected virtual void Page_Load(object sender, EventArgs e)
		{
            //if (Session[AverageNegative.User.KUserSessionKey] == null) {
            //    Response.Redirect ("~/ErrorPage.aspx");
            //}
		}
	}
}

