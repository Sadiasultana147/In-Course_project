<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControl1.ascx.cs" Inherits="Exam.WebUserControl1" %>
<h1 style="color:rebeccapurple;">CategoryList</h1>
    <asp:ListView ID="ListView1" runat="server" DataKeyNames="CategoryId" DataSourceID="sdsCategory" InsertItemPosition="LastItem">
        
        <EditItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="UpdateButton" ValidationGroup="ge" CssClass="btn btn-primary" runat="server" CommandName="Update" Text="Update" />
                    <asp:Button ID="CancelButton" CausesValidation="false" CssClass="btn btn-secondary" runat="server" CommandName="Cancel" Text="Cancel" />
                </td>
                <td>
                    <asp:Label ID="CategoryIdLabel1" runat="server" Text='<%# Eval("CategoryId") %>' />
                </td>
                <td>
                    <asp:TextBox ID="CategoryNameTextBox" ValidationGroup="ge" runat="server" Text='<%# Bind("CategoryName") %>' />
                    <asp:RequiredFieldValidator ValidationGroup="ge" ControlToValidate="CategoryNameTextBox" ID="RequiredFieldValidator1" runat="server" ErrorMessage="CategoryName required"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table runat="server" style="">
                <tr>
                    <td>No data was returned.</td>
                    
                </tr>
            </table>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="InsertButton" ValidationGroup="gi" CssClass="btn btn-info" runat="server" CommandName="Insert" Text="Insert" />
                    <asp:Button ID="CancelButton" CausesValidation="false" CssClass="btn btn-secondary" runat="server" CommandName="Cancel" Text="Clear" />
                </td>
                <td>&nbsp;</td>
                <td>
                    <asp:TextBox ID="CategoryNameTextBox" ValidationGroup="gi" runat="server" Text='<%# Bind("CategoryName") %>' />
                    <asp:RequiredFieldValidator ValidationGroup="gi" ControlToValidate="CategoryNameTextBox" ID="RequiredFieldValidator1" runat="server" ErrorMessage="CategoryName required"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="DeleteButton" CssClass="btn btn-danger" runat="server" CommandName="Delete" Text="Delete" />
                    <asp:Button ID="EditButton" runat="server" CssClass="btn btn-primary" CommandName="Edit" Text="Edit" />
                </td>
                <td>
                    <asp:Label ID="CategoryIdLabel" runat="server" Text='<%# Eval("CategoryId") %>' />
                </td>
                <td>
                    <asp:Label ID="CategoryNameLabel" runat="server" Text='<%# Eval("CategoryName") %>' />
                </td>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server" class="table">
                <tr runat="server">
                    <td runat="server">
                        <table id="itemPlaceholderContainer" class="table table-bordered" runat="server" border="0" style="">
                            <tr runat="server" style="">
                                <th runat="server"></th>
                                <th runat="server">CategoryId</th>
                                <th runat="server">CategoryName</th>
                            </tr>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style=""></td>
                </tr>
            </table>
        </LayoutTemplate>
        
    </asp:ListView>


    <asp:SqlDataSource ID="sdsCategory" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" DeleteCommand="DELETE FROM [Category] WHERE [CategoryId] = @CategoryId" InsertCommand="INSERT INTO [Category] ([CategoryName]) VALUES (@CategoryName)" SelectCommand="SELECT * FROM [Category]" UpdateCommand="UPDATE [Category] SET [CategoryName] = @CategoryName WHERE [CategoryId] = @CategoryId">
        <DeleteParameters>
            <asp:Parameter Name="CategoryId" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="CategoryName" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="CategoryName" Type="String" />
            <asp:Parameter Name="CategoryId" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>