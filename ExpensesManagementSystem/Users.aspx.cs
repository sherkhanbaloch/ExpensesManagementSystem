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
    public partial class Users : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                ShowData();
            }
            lblMessage.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save Record")
            {
                InsertData();
                ShowData();
            }
            else if (btnSave.Text == "Update Record")
            {
                UpdateData(userID);
                ShowData();
                btnSave.Text = "Save Record";
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        public static string userID;

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                userID = e.CommandArgument.ToString();

                if (e.CommandName == "cmdEdit")
                {
                    Label lblName = (Label)e.Item.FindControl("lblName");
                    Label lblPassword = (Label)e.Item.FindControl("lblPassword");

                    nameTxt.Text = lblName.Text;
                    passwordTxt.Text = lblPassword.Text;

                    btnSave.Text = "Update Record";
                }
                else if (e.CommandName == "cmdDelete")
                {
                    DeleteData(userID);
                    ShowData();
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

        // Custom Methods
        public void ShowData()
        {
            try
            {
                string qry = "select * from vw_ViewUsers";
                SqlDataAdapter da = new SqlDataAdapter(qry, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                Repeater1.DataSource = dt;
                Repeater1.DataBind();
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

        public void InsertData()
        {
            try
            {
                string qry = "insert into tbl_Users values (@name, @password, 1)";
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@name", nameTxt.Text.Trim());
                cmd.Parameters.AddWithValue("@password", passwordTxt.Text.Trim());

                con.Open();
                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    lblMessage.Text = "User Added Successfully.";
                    lblMessage.CssClass = "alert alert-success";
                    lblMessage.Visible = true;

                    ClearData();
                }
                else
                {
                    lblMessage.Text = "User Could Not Be Added.";
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

        public void UpdateData(string id)
        {
            try
            {
                string qry = "update tbl_Users set userName = @name, userPassword = @password where userID = @id";
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@name", nameTxt.Text.Trim());
                cmd.Parameters.AddWithValue("@password", passwordTxt.Text.Trim());
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    lblMessage.Text = "User Updated Successfully.";
                    lblMessage.CssClass = "alert alert-success";
                    lblMessage.Visible = true;

                    ClearData();
                }
                else
                {
                    lblMessage.Text = "User Could Not Be Updated.";
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

        public void DeleteData(string id)
        {
            try
            {
                string qry = "update tbl_Users set userStatus = 0 where userID = @id";
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    lblMessage.Text = "User Deleted Successfully.";
                    lblMessage.CssClass = "alert alert-success";
                    lblMessage.Visible = true;
                }
                else
                {
                    lblMessage.Text = "User Could Not Be Deleted.";
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

        public void ClearData()
        {
            nameTxt.Text = string.Empty;
            passwordTxt.Text = string.Empty;
            btnSave.Text = "Save Record";
        }
    }
}