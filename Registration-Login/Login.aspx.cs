﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Registration_Login
{
    public partial class Login : System.Web.UI.Page
    {
        static string ConString = ConfigurationManager.ConnectionStrings["CS"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConString);
            string email = TextBox1.Text;
            string password = TextBox2.Text;

            try
            {
                SqlDataAdapter sda = new SqlDataAdapter("Select * from Table_reg where Email = @Email and Password = @Password", con);
                sda.SelectCommand.Parameters.AddWithValue("@Email", email);
                sda.SelectCommand.Parameters.AddWithValue("@Password", password);
                DataTable dt = new DataTable();

                con.Open();
                sda.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    Label3.Text = "Login successful";
                    Label3.ForeColor = System.Drawing.Color.Green;

                    Response.Redirect("UserListPage.aspx");
                }
                else
                {
                    Label3.Text = "Login unsuccessful";
                    Label3.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                Label3.Text = "An error occurred: " + ex.Message;
            }
            finally
            {
                con.Close();
            }

        }
    }
}