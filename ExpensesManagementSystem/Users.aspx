<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="ExpensesManagementSystem.Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Danger-color Breadcrumb card start -->
    <div class="card borderless-card">
        <div class="card-block danger-breadcrumb">
            <div class="page-header-breadcrumb">
                <ul class="breadcrumb-title">
                    <li class="breadcrumb-item">
                        <a href="#!">
                            <i class="icofont icofont-home"></i>
                        </a>
                    </li>
                    <li class="breadcrumb-item"><a href="Default.aspx">Dashboard</a>
                    </li>
                    <li class="breadcrumb-item"><a href="#">Users</a>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <!-- Danger-color Breadcrumb card end -->

     <div class="row">
        <div class="col-12">
            <asp:Label ID="lblMessage" Visible="false" runat="server"></asp:Label>
        </div>
    </div>

    <div class="pcoded-inner-content">
        <div class="main-body">
            <h2 class="text-center">Users Details</h2>
            <div class="page-wrapper">
                <div class="page-body">
                    <div class="row">
                        <div class="col-md-4 my-2">
                            <label class="col-form-label">User Name <span class="text-danger">*</span> </label>
                            <asp:TextBox ID="nameTxt" CssClass="form-control" placeholder="User Name" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-4 my-2">
                            <label class="col-form-label">Password <span class="text-danger">*</span></label>
                            <asp:TextBox ID="passwordTxt" placeholder="Password" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-4 my-2">
                            <br />
                            <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="Save Record" OnClick="btnSave_Click" />
                            <asp:Button ID="btnClear" CssClass="btn btn-primary" runat="server" Text="Clear Data" OnClick="btnClear_Click" />
                        </div>
                    </div>
                </div>

                <hr />

                <!-- Repeater and Data Tabel Starts -->
                <div class="row">
                    <div class="col-md-12 my-2">
                        <div class="card card-block table-border-style">
                            <div class="card-body table-responsive">
                                <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                                    <HeaderTemplate>
                                        <table id="example1" class="table table-bordered table-striped">
                                            <thead>
                                                <tr>
                                                    <th>ID</th>
                                                    <th>User Name</th>
                                                    <th>Password</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblID" Text='<%# Eval("ID") %>' runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblName" Text='<%# Eval("User Name") %>' runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPassword" Text='<%# Eval("Password") %>' runat="server" />
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="editLink" Text="Edit" CssClass="text-primary mx-2" CommandName="cmdEdit" CommandArgument='<%# Eval("ID") %>' runat="server"> <i class="fa fa-pen"></i> </asp:LinkButton>
                                                <asp:LinkButton ID="deleteLink" Text="Delete" CssClass="text-danger mx-2" CommandName="cmdDelete" CommandArgument='<%# Eval("ID") %>' runat="server"> <i class="fa fa-trash"></i> </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>    
                                    </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Repeater and data table ends -->
            </div>
        </div>
    </div>
    </div>
</asp:Content>
