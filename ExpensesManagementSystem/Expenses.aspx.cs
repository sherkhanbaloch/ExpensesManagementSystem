using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace ExpensesManagementSystem
{
    public partial class Expenses : System.Web.UI.Page
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
                LoadCategory();
            }
            lblMessage.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save Record")
            {
                InsertData();
            }
            else if (btnSave.Text == "Update Record")
            {
                UpdateData(expenseID);
            }
            ShowData();
            ClearData();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        public static string expenseID;
        public static string DeleteImageURL;

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                expenseID = e.CommandArgument.ToString();

                if (e.CommandName == "cmdEdit")
                {
                    Label lblDate = (Label)e.Item.FindControl("lblDate");
                    Label lblDescription = (Label)e.Item.FindControl("lblDescription");
                    Label lblAmount = (Label)e.Item.FindControl("lblAmount");
                    Label lblCategory = (Label)e.Item.FindControl("lblCategory");
                    Image img = (Image)e.Item.FindControl("Image2");

                    descriptionTxt.Text = lblDescription.Text;
                    dateTxt.Text = Convert.ToDateTime(lblDate.Text).ToString("yyyy-MM-dd");
                    categoryDDL.SelectedItem.Text = lblCategory.Text;
                    amountTxt.Text = lblAmount.Text;
                    Image1.ImageUrl = img.ImageUrl;

                    btnSave.Text = "Update Record";
                }
                else if (e.CommandName == "cmdDelete")
                {
                    Image img = (Image)e.Item.FindControl("Image2");
                    DeleteImageURL = img.ImageUrl.ToString();
                    DeleteData(expenseID);
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
                string qry = "select * from vw_ViewExpenses";
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

        public void LoadCategory()
        {
            try
            {
                string qry = "select * from vw_ViewCategories";
                SqlDataAdapter da = new SqlDataAdapter(qry, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                categoryDDL.DataSource = dt;
                categoryDDL.DataValueField = "ID";
                categoryDDL.DataTextField = "Category";
                categoryDDL.DataBind();
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
                bool isValidToExecute = false;

                string filePath = Server.MapPath("Images/Expenses/");
                string fileName = Path.GetFileName(FileUpload1.FileName);
                string extension = Path.GetExtension(FileUpload1.FileName);
                HttpPostedFile postedFile = FileUpload1.PostedFile;
                int size = postedFile.ContentLength;

                string imagePath = string.Empty;

                if (FileUpload1.HasFiles)
                {
                    if (extension.ToLower() == ".jpeg" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                    {
                        if (size <= 5000000)
                        {
                            FileUpload1.SaveAs(filePath + fileName);
                            imagePath = "Images/Expenses/" + fileName;
                            isValidToExecute = true;
                        }
                        else
                        {
                            lblMessage.Text = "Image Size Should Not Be More Than 5 MB.";
                            lblMessage.CssClass = "alert alert-danger";
                            lblMessage.Visible = true;
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Image Format Not Supported. Try JPEG, JPG, PNG Only.";
                        lblMessage.CssClass = "alert alert-danger";
                        lblMessage.Visible = true;
                    }
                }
                else
                {
                    imagePath = Image1.ImageUrl.ToString();
                    isValidToExecute = true;
                }

                if (isValidToExecute == true)
                {
                    string qry = "insert into tbl_Expenses values (@date, @description, @amount, @category, @image, @user, getdate())";
                    SqlCommand cmd = new SqlCommand(qry, con);
                    cmd.Parameters.AddWithValue("@date", dateTxt.Text.Trim());
                    cmd.Parameters.AddWithValue("@description", descriptionTxt.Text.Trim());
                    cmd.Parameters.AddWithValue("@amount", amountTxt.Text.Trim());
                    cmd.Parameters.AddWithValue("@category", categoryDDL.SelectedValue);
                    cmd.Parameters.AddWithValue("@image", imagePath);
                    cmd.Parameters.AddWithValue("@user", Session["userid"]);

                    con.Open();
                    int a = cmd.ExecuteNonQuery();

                    if (a > 0)
                    {
                        lblMessage.Text = "Expenses Details Added Successfully.";
                        lblMessage.CssClass = "alert alert-success";
                        lblMessage.Visible = true;

                        ClearData();
                    }
                    else
                    {
                        lblMessage.Text = "Expenses Details Could Not Be Added.";
                        lblMessage.CssClass = "alert alert-danger";
                        lblMessage.Visible = true;
                    }
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
                bool isValidToExecute = false;
                bool isNewImage = false;

                string filePath = Server.MapPath("Images/Expenses/");
                string fileName = Path.GetFileName(FileUpload1.FileName);
                string extension = Path.GetExtension(FileUpload1.FileName);
                HttpPostedFile postedFile = FileUpload1.PostedFile;
                int size = postedFile.ContentLength;

                string imagePath = "Images/Expenses/";

                if (FileUpload1.HasFiles)
                {
                    if (extension.ToLower() == ".jpeg" || extension.ToLower() == ".jpg" || extension.ToLower() == ".png")
                    {
                        if (size <= 5000000)
                        {
                            imagePath = "Images/Expenses/" + fileName;
                            FileUpload1.SaveAs(Server.MapPath(imagePath));
                            isValidToExecute = true;
                            isNewImage = true;
                        }
                        else
                        {
                            lblMessage.Text = "Image Size Should Not Be More Than 5 MB.";
                            lblMessage.CssClass = "alert alert-danger";
                            lblMessage.Visible = true;
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Image Format Not Supported. Try JPEG, JPG, PNG Only.";
                        lblMessage.CssClass = "alert alert-danger";
                        lblMessage.Visible = true;
                    }
                }
                else
                {
                    imagePath = Image1.ImageUrl.ToString();
                    isValidToExecute = true;
                }

                if (isValidToExecute)
                {
                    string qry = "update tbl_Expenses set expenseDate = @date, expenseDescription = @description, expenseAmount = @amount, categoryID = @category, receiptPicture = @image, userID = @user where expenseID = @id";
                    SqlCommand cmd = new SqlCommand(qry, con);
                    cmd.Parameters.AddWithValue("@date", dateTxt.Text.Trim());
                    cmd.Parameters.AddWithValue("@description", descriptionTxt.Text.Trim());
                    cmd.Parameters.AddWithValue("@amount", amountTxt.Text.Trim());
                    cmd.Parameters.AddWithValue("@category", categoryDDL.SelectedValue);
                    cmd.Parameters.AddWithValue("@image", imagePath);
                    cmd.Parameters.AddWithValue("@user", Session["userid"]);
                    cmd.Parameters.AddWithValue("@id", id);

                    con.Open();
                    int a = cmd.ExecuteNonQuery();

                    if (a > 0)
                    {
                        lblMessage.Text = "Expense Details Updated Successfully.";
                        lblMessage.CssClass = "alert alert-success";
                        lblMessage.Visible = true;

                        if (isNewImage == true)
                        {
                            string deletePath = Server.MapPath(Image1.ImageUrl.ToString());

                            if (File.Exists(deletePath) == true)
                            {
                                File.Delete(deletePath);
                            }
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Expense Details Could Not Be Updated.";
                        lblMessage.CssClass = "alert alert-danger";
                        lblMessage.Visible = true;
                    }
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
                string qry = "delete from tbl_Expenses where expenseID = @id";
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    lblMessage.Text = "Expense Details Deleted Successfully.";
                    lblMessage.CssClass = "alert alert-success";
                    lblMessage.Visible = true;

                    string deletePath = Server.MapPath(DeleteImageURL);

                    if (File.Exists(deletePath))
                    {
                        File.Delete(deletePath);
                    }
                }
                else
                {
                    lblMessage.Text = "Expense Details Could Not Be Deleted.";
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
            descriptionTxt.Text = string.Empty;
            dateTxt.Text = string.Empty;
            categoryDDL.ClearSelection();
            amountTxt.Text = string.Empty;
            Image1.ImageUrl = "~/assets/images/no-image.png";
            btnSave.Text = "Save Record";
        }
    }
}