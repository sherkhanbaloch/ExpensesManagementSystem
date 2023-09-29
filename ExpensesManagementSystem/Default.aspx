<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ExpensesManagementSystem.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-12">
            <asp:Label ID="lblMessage" Visible="false" runat="server"></asp:Label>
        </div>
    </div>

    <div class="pcoded-inner-content">
        <div class="main-body">
            <div class="page-wrapper">

                <div class="page-body">
                    <div class="row">
                        <!-- card1 start -->
                        <div class="col-md-6 col-xl-3">
                            <div class="card widget-card-1">
                                <div class="card-block-small">
                                    <i class="icofont icofont-money bg-c-blue card1-icon"></i>
                                    <span class="text-c-blue f-w-600">Total Income</span>
                                    <h4>
                                        <asp:Label ID="lblTotalIncome" runat="server" Text="0"></asp:Label>
                                    </h4>
                                </div>
                            </div>
                        </div>
                        <!-- card1 end -->
                        <!-- card1 start -->
                        <div class="col-md-6 col-xl-3">
                            <div class="card widget-card-1">
                                <div class="card-block-small">
                                    <i class="icofont icofont-money-bag bg-c-pink card1-icon"></i>
                                    <span class="text-c-pink f-w-600">Total Expenses</span>
                                    <h4>
                                        <asp:Label ID="lblTotalExpenses" runat="server" Text="0"></asp:Label>
                                    </h4>
                                </div>
                            </div>
                        </div>
                        <!-- card1 end -->
                        <!-- card1 start -->
                        <div class="col-md-6 col-xl-3">
                            <div class="card widget-card-1">
                                <div class="card-block-small">
                                    <i class="icofont icofont-chart-histogram-alt bg-c-green card1-icon"></i>
                                    <span class="text-c-green f-w-600">Total Profit</span>
                                     <h4>
                                        <asp:Label ID="lblTotalProfit" runat="server" Text="0"></asp:Label>
                                    </h4>
                                </div>
                            </div>
                        </div>
                        <!-- card1 end -->
                        <!-- card1 start -->
                        <div class="col-md-6 col-xl-3">
                            <div class="card widget-card-1">
                                <div class="card-block-small">
                                    <i class="icofont icofont-chart-line bg-c-yellow card1-icon"></i>
                                    <span class="text-c-yellow f-w-600">Total Loss</span>
                                    <h4>
                                        <asp:Label ID="lblTotalLoss" runat="server" Text="0"></asp:Label>
                                    </h4>
                                </div>
                            </div>
                        </div>
                        <!-- card1 end -->
                    </div>

                    <hr />

                    <div class="row">
                        <!-- card1 start -->
                        <div class="col-md-6 col-xl-3">
                            <div class="card widget-card-1">
                                <div class="card-block-small">
                                    <i class="icofont icofont-money bg-info card1-icon"></i>
                                    <span class="text-info f-w-600">Income</span>
                                     <h4>
                                        <asp:Label ID="lblMonthlyIncome" runat="server" Text="0"></asp:Label>
                                    </h4>
                                    <div>
                                        <span class="f-left m-t-10 text-muted">
                                            <i class="text-c-blue f-16 icofont icofont-ui-calendar m-r-10"></i>Current Month
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- card1 end -->
                        <!-- card1 start -->
                        <div class="col-md-6 col-xl-3">
                            <div class="card widget-card-1">
                                <div class="card-block-small">
                                    <i class="icofont icofont-coins bg-dark card1-icon"></i>
                                    <span class="text-dark f-w-600">Expenses</span>
                                     <h4>
                                        <asp:Label ID="lblMonthlyExpenses" runat="server" Text="0"></asp:Label>
                                    </h4>
                                    <div>
                                        <span class="f-left m-t-10 text-muted">
                                            <i class="text-c-blue f-16 icofont icofont-ui-calendar m-r-10"></i>Current Month 
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- card1 end -->
                    </div>
                </div>
            </div>

            <div id="styleSelector">
            </div>
        </div>
    </div>

</asp:Content>
