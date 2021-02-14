<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControl2.ascx.cs" Inherits="Exam.WebUserControl2" %>
<h1 style="color:rebeccapurple;">BookList</h1>
    <asp:ListView ID="ListView1" runat="server" DataKeyNames="BookId" DataSourceID="sdsBook" InsertItemPosition="LastItem" OnItemInserting="ListView1_ItemInserting" OnItemUpdating="ListView1_ItemUpdating">
        
        <EditItemTemplate>
            
            <tr style="">
                <td>
                    <asp:Button ID="UpdateButton" ValidationGroup="ge" CssClass="btn btn-primary" runat="server" CommandName="Update" Text="Update" />
                    <asp:Button ID="CancelButton" CausesValidation="false" CssClass="btn btn-secondary" runat="server" CommandName="Cancel" Text="Cancel" />
                </td>
                <td>
                    <asp:Label ID="BookIdLabel1" runat="server" Text='<%# Eval("BookId") %>' />
                </td>
                <td>
                    <asp:TextBox ID="BookNameTextBox" ValidationGroup="ge" runat="server" Text='<%# Bind("BookName") %>' />
                    
                    <asp:RequiredFieldValidator ValidationGroup="ge" ControlToValidate="BookNameTextBox" ID="RequiredFieldValidator1" runat="server" ErrorMessage="BookName required"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Image runat="server" validationGroup="ge" Width="40px" ID="Image1" ImageUrl='<%# Eval("Picture","~/Uploads/{0}") %>'></asp:Image>
                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Bind("Picture") %>'/>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                </td>
                
                <td>
                    <asp:TextBox ID="PublisherEmailTextBox" runat="server" Text='<%# Bind("PublisherEmail") %>' />
                    <asp:RequiredFieldValidator ValidationGroup="ge" ControlToValidate="PublisherEmailTextBox" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Email required"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ValidationGroup="ge" ControlToValidate="PublisherEmailTextBox" ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </td>
                <td>
                   <asp:DropDownList ValidationGroup="ge" SelectedValue='<%#Bind("CategoryId") %>'  ID="DropDownList1" runat="server" DataSourceID="sdsCategory" DataTextField="CategoryName" DataValueField="CategoryId"></asp:DropDownList>
                </td>
            </tr>
        </EditItemTemplate>
        
        <InsertItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="InsertButton" ValidationGroup="gi" CssClass="btn btn-primary" runat="server" CommandName="Insert" Text="Insert" />
                    <asp:Button ID="CancelButton" CausesValidation="false" CssClass="btn btn-secondary" runat="server" CommandName="Cancel" Text="Clear" />
                </td>
                <td>&nbsp;</td>
                <td>
                    <asp:TextBox ID="BookNameTextBox" ValidationGroup="gi" runat="server" Text='<%# Bind("BookName") %>' />
                    <asp:RequiredFieldValidator ValidationGroup="gi" ControlToValidate="BookNameTextBox" ID="RequiredFieldValidator2" runat="server" ErrorMessage="BookName required"></asp:RequiredFieldValidator>
                </td>
                <td>
                   
                    
                    <asp:Image runat="server" validationGroup="gi" Width="40px" ID="Image2" ImageUrl='<%# Eval("Picture","~/Uploads/{0}") %>'></asp:Image>
                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Bind("Picture") %>'/>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    <asp:RequiredFieldValidator ValidationGroup="gi" ControlToValidate="FileUpload1" runat="server" ErrorMessage="Picture required"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:TextBox ID="PublisherEmailTextBox" runat="server" Text='<%# Bind("PublisherEmail") %>' />
                    <asp:RequiredFieldValidator ValidationGroup="gi" ControlToValidate="PublisherEmailTextBox" ID="RequiredFieldValidator4" runat="server" ErrorMessage="Email required"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ValidationGroup="gi" ControlToValidate="PublisherEmailTextBox" ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </td>
                <td>
                    <asp:DropDownList ValidationGroup="gi" SelectedValue='<%#Bind("CategoryId") %>'  ID="DropDownList1" runat="server" DataSourceID="sdsCategory" DataTextField="CategoryName" DataValueField="CategoryId"></asp:DropDownList>
                </td>
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="DeleteButton" CssClass="btn btn-danger" runat="server" CommandName="Delete" Text="Delete" />
                    <asp:Button ID="EditButton" CssClass="btn btn-info" runat="server" CommandName="Edit" Text="Edit" />
                </td>
                <td>
                    <asp:Label ID="BookIdLabel" runat="server" Text='<%# Eval("BookId") %>' />
                </td>
                <td>
                    <asp:Label ID="BookNameLabel" runat="server" Text='<%# Eval("BookName") %>' />
                </td>
                <td>
                    <asp:Image runat="server" validationGroup="gi" Width="40px" ID="Image2" ImageUrl='<%# Eval("Picture","~/Uploads/{0}") %>'></asp:Image>
                </td>
                <td>
                    <asp:Label ID="PublisherEmailLabel" runat="server" Text='<%# Eval("PublisherEmail") %>' />
                </td>
                <td>
                    <asp:Label ID="CategoryIdLabel" runat="server" Text='<%# Eval("CategoryId") %>' />
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
                                <th runat="server">BookId</th>
                                <th runat="server">BookName</th>
                                <th runat="server">Picture</th>
                                <th runat="server">PublisherEmail</th>
                                <th runat="server">CategoryId</th>
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
    <asp:SqlDataSource ID="sdsBook" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" DeleteCommand="DELETE FROM [Book] WHERE [BookId] = @BookId" InsertCommand="INSERT INTO [Book] ([BookName], [Picture], [PublisherEmail], [CategoryId]) VALUES (@BookName, @Picture, @PublisherEmail, @CategoryId)" SelectCommand="SELECT * FROM [Book]" UpdateCommand="UPDATE [Book] SET [BookName] = @BookName, [Picture] = @Picture, [PublisherEmail] = @PublisherEmail, [CategoryId] = @CategoryId WHERE [BookId] = @BookId">
        <DeleteParameters>
            <asp:Parameter Name="BookId" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="BookName" Type="String" />
            <asp:Parameter Name="Picture" Type="String" />
            <asp:Parameter Name="PublisherEmail" Type="String" />
            <asp:Parameter Name="CategoryId" Type="Int32" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="BookName" Type="String" />
            <asp:Parameter Name="Picture" Type="String" />
            <asp:Parameter Name="PublisherEmail" Type="String" />
            <asp:Parameter Name="CategoryId" Type="Int32" />
            <asp:Parameter Name="BookId" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    


    <asp:SqlDataSource ID="sdsCategory" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Category]"></asp:SqlDataSource>