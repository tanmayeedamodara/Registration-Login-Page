using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Registration_Login
{
    public partial class UserListPage : System.Web.UI.Page
    {
        static string ConString = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindUserData();
        }
        protected void BindUserData()
        {
            using (SqlConnection con = new SqlConnection(ConString))
            {
                try
                {
                    SqlDataAdapter sda = new SqlDataAdapter("GetRegisteredUsers", con);
                    sda.SelectCommand.CommandType = CommandType.StoredProcedure;

                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }
            }
        }
        protected void RptUsers_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ViewMail")
            {
                string EmailID = e.CommandArgument.ToString();

                UserDAL userDAL = new UserDAL();
                User user = userDAL.GetUsers(EmailID);

                if (user != null)
                {
                    string queryString = $"Email={user.Email}";

                    string redirectUrl = $"IndividualDeets.aspx?{queryString}";

                    Session["Fname2"] = user.Fname;
                    Session["Lname2"] = user.Lname;
                    //Session["Email2"] = user.Email;
                    Session["Gender2"] = user.Gender;
                    Session["Address2"] = user.Address;
                    Session["Phone2"] = user.Phone;
                    Response.Redirect(redirectUrl);
                    return;
                }
            }
        }
    }
}