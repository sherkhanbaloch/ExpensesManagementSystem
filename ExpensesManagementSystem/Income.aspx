<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="Income.aspx.cs" Inherits="ExpensesManagementSystem.Income" %>

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
                    <li class="breadcrumb-item"><a href="#">Income</a>
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
            <h2 class="text-center">Income Details</h2>
            <div class="page-wrapper">
                <div class="page-body">
                    <div class="row">
                        <div class="col-md-8">
                            <div class="row">
                                <div class="col-md-12 my-2">
                                    <label class="col-form-label">Income Description <span class="text-danger">*</span> </label>
                                    <asp:TextBox ID="descriptionTxt" CssClass="form-control" placeholder="Description of Income" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6 my-2">
                                    <label class="col-form-label">Income Date <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="dateTxt" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6 my-2">
                                    <label class="col-form-label">Income Category</label>
                                    <asp:DropDownList ID="categoryDDL" CssClass="form-control" AppendDataBoundItems="true" runat="server">
                                        <asp:ListItem Text="Select Category" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6 my-2">
                                    <label class="col-form-label">Income  Amount <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="amountTxt" CssClass="form-control" placeholder="Income Amount" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6 my-2">
                                    <label class="col-form-label">Receipt/Slip</label>
                                    <br />
                                    <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6 my-2">
                                    <asp:Button ID="btnSave" CssClass="btn btn-success btn-block" runat="server" Text="Save Record" OnClick="btnSave_Click" />
                                </div>
                                <div class="col-md-6 my-2">
                                    <asp:Button ID="btnClear" CssClass="btn btn-danger btn-block" runat="server" Text="Clear Data" OnClick="btnClear_Click" />

                                </div>
                            </div>
                        </div>

                        <div class="col-md-4 text-center my-auto">
                            <asp:Image ID="Image1" CssClass="border w-75 h-75" ImageUrl="~/assets/images/No_image.png" runat="server" />
                        </div>
                    </div>

                    <hr />

                    <!-- Repeater and Data Tabel Starts -->
                    <div class="row">
                        <div class="col-md-12 my-2">
                            <div class="card card-block table-border-style">
                                <div class="card-body table-responsive">
                                    <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand" OnItemDataBound="Repeater1_ItemDataBound">
                                        <HeaderTemplate>
                                            <table id="example1" class="table table-bordered table-striped">
                                                <thead>
                                                    <tr>
                                                        <th>ID</th>
                                                        <th>Date</th>
                                                        <th>Description</th>
                                                        <th>Amount</th>
                                                        <%--<th>Category ID</th>--%>
                                                        <th>Category</th>
                                                        <th>Receipt</th>
                                                        <th>User</th>
                                                        <th>Entry Date</th>
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
                                                    <asp:Label ID="lblDate" Text='<%# Convert.ToDateTime(Eval("Date")).ToString("dd/MM/yyyy") %>' runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDescription" Text='<%# Eval("Description") %>' runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblAmount" Text='<%# Eval("Amount") %>' runat="server" />
                                                </td>
                                                <%--<td>
                                                    <asp:Label ID="lblCategoryID" Text='<%# Eval("CategoryID") %>' runat="server" />
                                                </td>--%>
                                                <td>
                                                    <asp:Label ID="lblCategory" Text='<%# Eval("Category") %>' runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Image ID="Image2" ImageUrl='<%# Eval("Receipt") %>' Width="50" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblUser" Text='<%# Eval("User") %>' runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblEntryDate" Text='<%# Convert.ToDateTime(Eval("Entry Date")).ToString("dd/MM/yyyy") %>' runat="server" />
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
