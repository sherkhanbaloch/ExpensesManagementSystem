using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ExpensesManagementSystem
{
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from tbl_Users where userName = @username and userPassword = @password and userStatus = 1";
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@username", nameTxt.Text.Trim());
                cmd.Parameters.AddWithValue("@password", passwordTxt.Text.Trim());

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Session["userid"] = reader["userID"].ToString();
                    Session["username"] = reader["userName"].ToString();
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    lblMessage.Text = "Invalid User Name or Password.";
                    lblMessage.CssClass = "alert alert-danger";
                    lblMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.CssClass = "alert alert-danger";
                lblMessage.Visible = true;
            }
            finally
            {
                con.Close();
            }
        }
    }
}