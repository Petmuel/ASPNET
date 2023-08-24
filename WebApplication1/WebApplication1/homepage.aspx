<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="homepage.aspx.cs" Inherits="WebApplication1.homePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Homepage</title>
    <link href="asset/style.css" rel="stylesheet" type="text/css" />
    <link href="asset/Content/bootstrap.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,700&display=swap" rel="stylesheet"/>
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.3/umd/popper.min.js" integrity="sha384-vFJXuSJphROIrBnz7yo7oB41mKfc8JzQZiCq4NCceLEaO4IHwicKwpJf9c9IpFgh" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/js/bootstrap.min.js" integrity="sha384-alpBpkh1PFOepccYVYDB4do5UnbKysX5WZXm3XxPqe5iKTfUKjNkCk9SaVuEZflJ" crossorigin="anonymous"></script>
<style>
        .modal {
            display: none;
            position: fixed;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.5);
        }
        
        .modal-content {
            background-color: #f7f7f7;
            border-radius: 5px;
            padding: 20px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
            width: 50%;
            max-width: 400px;
        }
        .modal-content {
            background-color: white;
            margin: 15% auto;
            padding: 20px;
            border: 1px solid #888;
            width: 50%;
        }
        .modal-header {
            background-color:darkblue;
            color: white;
            padding: 10px;
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
        }

        /* Style the modal footer */
        .modal-footer {
            padding: 10px;
            text-align: right;
            border-bottom-left-radius: 5px;
            border-bottom-right-radius: 5px;
        }
    </style>
</head>
<body>
    <div class="container">
        <form id="formRegister" runat="server" novalidate>
            <p>
                <asp:Button ID="btnSignOut" UseSubmitBehavior="false" runat="server" class="btn btn-warning" Text="Sign Out" OnClick="SignOut_Click"/>
                <asp:Button ID="btnGoMachine" UseSubmitBehavior="false" runat="server" class="btn btn-primary" Text="Machines" OnClick="MachineNavigate_Click"/>
                <h1>User Management Page</h1>
            </p>
            <!-- Open user registration modal -->
            <asp:Button ID="btnShowPopup" runat="server" Text="Create New User" class="btn btn-primary" OnClick="btnShowPopup_Click" />
            <asp:Label ID="lblRegisterResult" ForeColor="#006600" runat="server"></asp:Label><br><br>
            <!-- User registration Modal -->
            <div runat="server" id="createUserModal" class="modal">
                <div class="modal-content">
                    <div class="container">
                        <div class="modal-header">
                            <h3 class="modal-title" id="createUserModalLabel">Create New User</h3>
                        </div>
                        <div class="modal-body">
                            <label>Email:</label>
                            <input type="email" id="email" class="input-field" name="email" required/>
                            <br>
                            <label>Password:</label>
                            <input type="password" id= "password" class="input-field" name="password" required>
                            
                            <asp:Label ID="lblCreateUserError" ForeColor="#d20000" runat="server"></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnCancel" runat="server" class="btn btn-secondary" Text="Cancel" OnClick="btnCloseCreateUserPopup_Click" />
                            <asp:Button buttontype="submit" ID="btnRegister" runat="server" class="btn btn-success" Text="Register" OnClick="btnRegisterUser_Click" />
                        </div>
                    </div>
                </div>
            </div>

             <%--Edit user modal--%>
             <div runat="server" id="EditUserModal" class="modal">
                <div class="modal-content">
                    <div class="container">
                        <div class="modal-header">
                            <h3 class="modal-title" id="EditUserModalLabel">Edit User</h3>
                            <h5>Machine ID: <asp:Label ID="lblUserId" runat="server"></asp:Label></h5>
                        </div>
                        <div class="modal-body">
                            <label>Email: </label>
                            <asp:TextBox ID="txtEditUserEmail" type="email" runat="server" class="input-field" name="email" required/>
                            <br><br>
                            <label>Password:</label>
                            <asp:TextBox ID="txtEditUserPassword" type="text" runat="server" class="input-field" name="password" required />
                            <asp:Label ID="Label1" ForeColor="#d20000" runat="server"></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnCancel1" runat="server" class="btn btn-secondary" Text="Cancel" OnClick="btnCloseEditUserPopup_Click" />
                            <asp:Button ID="Button3" runat="server" class="btn btn-success" OnClick="EditRow" Text="Save"/>
                        </div>
                    </div>
                </div>
            </div>

            <!-- User Delete Modal -->
            <div runat="server" id="DeleteUserModal" class="modal">
                <div class="modal-content">
                    <div class="container">
                        <div class="modal-header">
                            <h3 class="modal-title" id="DeleteUserModalLabel">Delete User</h3>
                            <h5>User ID: <asp:Label ID="lblDeleteUserId" runat="server"></asp:Label></h5>
                        </div>
                       <div class="modal-body">
                            <label>Are you sure you want to delete this user?</label>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnCancelDeleteUser" runat="server" class="btn btn-secondary" Text="Cancel" OnClick="btnCloseDeleteUserPopup_Click" />
                            <asp:Button ID="btnDelete" runat="server" class="btn btn-success" OnClick="DeleteRow" Text="Confirm"/>
                        </div>
                    </div>
                </div>
            </div>
            
             <!-- GridView Users -->
            <h3><asp:Label ID="lblTableHeader" runat="server"></asp:Label></h3>
            
            <asp:GridView ID="GridView1" HeaderStyle-ForeColor="White"  BorderColor="#000000" BorderWidth="1.5"
                runat="server" AutoGenerateColumns="false" DataKeyNames="id" OnRowCommand="userTable_OnRowCommand" HorizontalAlign="Center" CssClass="center-header">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-BorderWidth="1.5" HeaderStyle-BorderWidth="1.5"/>
                    <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-BorderWidth="1.5" HeaderStyle-BorderWidth="1.5"/>
                    <asp:BoundField DataField="Password" HeaderText="Password" ItemStyle-BorderWidth="1.5" HeaderStyle-BorderWidth="1.5"/>
                    <asp:TemplateField ShowHeader="False" HeaderText="Actions" ItemStyle-BorderWidth="1.5" HeaderStyle-BorderWidth="1.5">
                        <ItemTemplate>
                            
                            <asp:Button ID="btnEdit" runat="server" ButtonType="Link" UseSubmitBehavior="false" Class="btn btn-primary" CommandName="EditUser" Text="Edit" CommandArgument='<%#Container.DataItemIndex%>'/>
                            <asp:Button ID="btnDelete" runat="server" ButtonType="Link" UseSubmitBehavior="false" Class="btn btn-danger" CommandName="DeleteUser" Text="Delete" CommandArgument="<%# Container.DataItemIndex %>"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </form>
    
    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="asset/Script/bootstrap.min.js"></script>
    <script src="asset/Script/bootstrap.bundle.min.js"></script>
       
</body>
</html>