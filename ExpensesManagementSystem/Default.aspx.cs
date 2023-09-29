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
    public partial class Default : System.Web.UI.Page
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
                TotalIncome();
                TotalExpenses();
                ProfitAndLoss();
                MonthlyIncome();
                MonthlyExpenses();
            }
        }

        public void TotalIncome()
        {
            string qry = "select isnull(SUM(Amount), 0) as Total from vw_ViewIncome";
            SqlCommand cmd = new SqlCommand(qry, con);
            con.Open();
            int value = Convert.ToInt32(cmd.ExecuteScalar());
            lblTotalIncome.Text = value.ToString();
            con.Close();
        }

        public void TotalExpenses()
        {
            string qry = "select isnull(SUM(Amount), 0) as Total from vw_ViewExpenses";
            SqlCommand cmd = new SqlCommand(qry, con);
            con.Open();
            int value = Convert.ToInt32(cmd.ExecuteScalar());
            lblTotalExpenses.Text = value.ToString();
            con.Close();
        }

        public void ProfitAndLoss()
        {
            double income = Convert.ToDouble(lblTotalIncome.Text);
            double expenses = Convert.ToDouble(lblTotalExpenses.Text);

            if (income > expenses)
            {
                double profit = income - expenses;
                lblTotalProfit.Text = profit.ToString();
                lblTotalLoss.Text = "0";
            }
            else if (income < expenses)
            {
                double loss = expenses - income;
                lblTotalLoss.Text = loss.ToString();
                lblTotalProfit.Text = "0";
            }
        }

        public void MonthlyIncome()
        {
            string qry = "select isnull(SUM(Amount), 0) as Total from vw_ViewIncome where Format(Date, 'yyyy-MM') = Format(GETDATE(), 'yyyy-MM')";
            SqlCommand cmd = new SqlCommand(qry, con);
            con.Open();
            int value = Convert.ToInt32(cmd.ExecuteScalar());
            lblMonthlyIncome.Text = value.ToString();
            con.Close();
        }

        public void MonthlyExpenses()
        {
            string qry = "select isnull(SUM(Amount), 0) as Total from vw_ViewExpenses where Format(Date, 'yyyy-MM') = Format(GETDATE(), 'yyyy-MM')";
            SqlCommand cmd = new SqlCommand(qry, con);
            con.Open();
            int value = Convert.ToInt32(cmd.ExecuteScalar());
            lblMonthlyExpenses.Text = value.ToString();
            con.Close();
        }
    }
}