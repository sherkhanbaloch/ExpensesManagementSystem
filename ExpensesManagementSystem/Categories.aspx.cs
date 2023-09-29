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
    public partial class Categories : System.Web.UI.Page
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
                UpdateData(categoryID);
                ShowData();
                btnSave.Text = "Save Record";
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        public static string categoryID;

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                categoryID = e.CommandArgument.ToString();

                if (e.CommandName == "cmdEdit")
                {
                    Label lblCategory = (Label)e.Item.FindControl("lblCategory");

                    nameTxt.Text = lblCategory.Text;

                    btnSave.Text = "Update Record";
                }
                else if (e.CommandName == "cmdDelete")
                {
                    DeleteData(categoryID);
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
                string qry = "select * from vw_ViewCategories";
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
                string qry = "insert into tbl_Categories values (@name, 1)";
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@name", nameTxt.Text.Trim());

                con.Open();
                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    lblMessage.Text = "Category Added Successfully.";
                    lblMessage.CssClass = "alert alert-success";
                    lblMessage.Visible = true;

                    ClearData();
                }
                else
                {
                    lblMessage.Text = "Category Could Not Be Added.";
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
                string qry = "update tbl_Categories set categoryName = @name where categoryID = @id";
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@name", nameTxt.Text.Trim());
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    lblMessage.Text = "Category Updated Successfully.";
                    lblMessage.CssClass = "alert alert-success";
                    lblMessage.Visible = true;

                    ClearData();
                }
                else
                {
                    lblMessage.Text = "Category Could Not Be Updated.";
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
                string qry = "update tbl_Categories set categoryStatus = 0 where categoryID = @id";
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    lblMessage.Text = "Category Deleted Successfully.";
                    lblMessage.CssClass = "alert alert-success";
                    lblMessage.Visible = true;
                }
                else
                {
                    lblMessage.Text = "Category Could Not Be Deleted.";
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
            btnSave.Text = "Save Record";
        }
    }
}