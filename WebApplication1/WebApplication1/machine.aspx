<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="machine.aspx.cs" Inherits="WebApplication1.machine" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Machine</title>
    <link href="asset/style.css" rel="stylesheet" type="text/css" />
    <link href="asset/Content/bootstrap.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,700&display=swap" rel="stylesheet"/>
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.3/umd/popper.min.js" integrity="sha384-vFJXuSJphROIrBnz7yo7oB41mKfc8JzQZiCq4NCceLEaO4IHwicKwpJf9c9IpFgh" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/js/bootstrap.min.js" integrity="sha384-alpBpkh1PFOepccYVYDB4do5UnbKysX5WZXm3XxPqe5iKTfUKjNkCk9SaVuEZflJ" crossorigin="anonymous"></script>
</head>
<body>
    <div class="container">
        <form id="formRegister" runat="server" novalidate>
            <p>
                <%--<input type="button" runat="server" value="Sign Out" class="btn btn-warning" ID="btnSignOut" onServerClick="SignOut_Click" />--%>
                <asp:Button ID="btnBackToUser" UseSubmitBehavior="false" runat="server" class="btn btn-warning" Text="Back" OnClick="Back_Click"/>
                <h1>Machine Management Page</h1>
            </p>
            <!-- Open machine create modal -->
            <button type="button" style="background-color:#009999; color:white" class="btn" data-toggle="modal" data-target="#createMachineModal">Create New Machine</button>
            <asp:Label ID="lblCreateResult" ForeColor="#006600" runat="server"></asp:Label><br><br>
            <!-- Create Machine Modal -->
            <div class="modal fade" id="createMachineModal" tabindex="-1" role="dialog" aria-labelledby="createMachineModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="container">
                            <div class="modal-header">
                                <h3 class="modal-title" id="createMachineModalLabel">Create New Machine</h3>
                            </div>
                            <div class="modal-body">
                                <label>Mahine Name:</label>
                                <input type="text" id="txtCreateMachineName" class="input-field" name="mName" required/>
                            </div>
                            <asp:Label ID="lblError" ForeColor="#d20000" runat="server"></asp:Label>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                                <asp:Button type="submit" ID="btnCreate" runat="server" class="btn btn-success" OnClick="btnCreateMachine_Click" Text="Create"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Machine Edit Modal -->
            <div class="modal fade" id="EditMachineModal"  tabindex="-1" role="dialog" aria-labelledby="EditMachineModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="container">
                            <div class="modal-header">
                                <h3 class="modal-title" id="EditMachineModalLabel">Machine Edit Form</h3>
                                <h5>Machine ID: <asp:Label ID="lblMachineId" runat="server"></asp:Label></h5>
                            </div>
                            <div class="modal-body">
                                <label>MachineName:</label>
                                <asp:TextBox ID="txtEditMachineName" type="text" runat="server" class="input-field" name="mName" required/>
                            </div>
                            <asp:Label ID="lblError1" ForeColor="#d20000" runat="server"></asp:Label>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                                <asp:Button ID="btnEditMachineSave" runat="server" class="btn btn-success" OnClick="EditMachine_Click" Text="Save"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Delete Machine Modal -->
            <div class="modal fade" id="DeleteMachineModal"  tabindex="-1" role="dialog" aria-labelledby="DeleteMachineModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="container">
                            <div class="modal-header">
                                <h3 class="modal-title" id="DeleteMachineModalLabel">Machine Delete</h3>
                                <h5>Machine ID: <asp:Label ID="lblDeleteMachineId" runat="server"></asp:Label></h5>
                            </div>
                            <div class="modal-body">
                                <label>Are you sure you want to delete this machine?</label>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                                <asp:Button type="button" ID="btnDeleteMachine" runat="server" class="btn btn-danger" OnClick="DeleteMachine_Click" Text="Confirm"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
             <!-- GridView Machines -->
            <h3><asp:Label ID="lblTableHeader" runat="server"></asp:Label></h3>
            
            <asp:GridView ID="MachineGridView" HeaderStyle-ForeColor="White" BorderColor="#000000" BorderWidth="1.5"
                runat="server" AutoGenerateColumns="false" DataKeyNames="MachineID" OnRowCommand="machineTable_OnRowCommand" HorizontalAlign="Center" CssClass="center-header">
                <Columns>
                    <asp:BoundField DataField="MachineID" HeaderText="Id" ItemStyle-BorderWidth="1.5" HeaderStyle-BackColor="#009999" HeaderStyle-BorderWidth="1.5"/>
                    <asp:BoundField DataField="MachineName" HeaderText="Name" ItemStyle-BorderWidth="1.5" HeaderStyle-BackColor="#009999" HeaderStyle-BorderWidth="1.5"/>
                    <asp:BoundField DataField="MachineStatus" HeaderText="Status" ItemStyle-BorderWidth="1.5" HeaderStyle-BackColor="#009999" HeaderStyle-BorderWidth="1.5"/>
                    <asp:TemplateField ShowHeader="False" HeaderText="Actions" ItemStyle-BorderWidth="1.5" HeaderStyle-BackColor="#009999" HeaderStyle-BorderWidth="1.5">
                        <ItemTemplate>
                            <asp:Button ID="btnEditMachine" runat="server" ButtonType="Link" UseSubmitBehavior="false" Class="btn btn-primary" CommandName="EditMachine" Text="Edit" CommandArgument='<%#Container.DataItemIndex%>'/>
                            <asp:Button ID="btnDeleteMachine" runat="server" ButtonType="Link" UseSubmitBehavior="false" Class="btn btn-danger" CommandName="DeleteMachine" Text="Delete" CommandArgument="<%# Container.DataItemIndex %>"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </form>
    
    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="asset/Script/bootstrap.min.js"></script>
    <script src="asset/Script/bootstrap.bundle.min.js"></script>
       
</body>
</html><%--  --%>
