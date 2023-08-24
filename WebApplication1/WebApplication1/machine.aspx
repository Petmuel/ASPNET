<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="machine.aspx.cs" Inherits="WebApplication1.machine" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Machine</title>
    <link href="asset/style.css" rel="stylesheet" type="text/css" />
    <link href="asset/Content/bootstrap.min.css" rel="stylesheet" />
    
    <style>
        .modal {
            display: none;
            position: fixed;
            z-index: 1;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            overflow: auto;
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
            background-color: #009688;
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
    <div class="container">
        <form id="formRegister" runat="server" novalidate>
            <p>
                <%--<input type="button" runat="server" value="Sign Out" class="btn btn-warning" ID="btnSignOut" onServerClick="SignOut_Click" />--%>
                <asp:Button ID="btnBackToUser" UseSubmitBehavior="false" runat="server" class="btn btn-warning" Text="Back" OnClick="Back_Click"/>
                <h1>Machine Management Page</h1>
            </p>
            <!-- Create Machine Modal -->
            <div runat="server" id="createMachineModal" class="modal">
                <div class="modal-content">
                    <div class="container">
                        <div class="modal-header">
                            <h3 class="modal-title" id="createMachineModalLabel">Create New Machine</h3>
                        </div>
                        <div class="modal-body">
                            <label>Mahine Name: </label>
                            <input type="text" id="txtCreateMachineName" class="input-field" name="mName" required/>
                            <br>
                            <asp:Label ID="lblCreateMachineError" ForeColor="#d20000" runat="server"></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnCancel" runat="server" class="btn btn-secondary" Text="Cancel" OnClick="btnCloseCreateMachinePopup_Click" />
                            <asp:Button ID="btnSubmit" runat="server" class="btn btn-success" Text="Create" OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <%--Edit machine modal--%>
             <div runat="server" id="editMachineModal" class="modal">
                <div class="modal-content">
                    <div class="container">
                        <div class="modal-header">
                            <h3 class="modal-title" id="editMachineModalLabel">Edit Machine</h3>
                            <h5>Machine ID: <asp:Label ID="lblMachineId" runat="server"></asp:Label></h5>
                        </div>
                        <div class="modal-body">
                            <label>Mahine Name: </label>
                            <asp:TextBox ID="txtEditMachineName" type="Text" runat="server" class="input-field" name="mName" required/>
                            <br>
                            <asp:Label ID="lblEditMachineError" ForeColor="#d20000" runat="server"></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="Button2" runat="server" class="btn btn-secondary" Text="Cancel" OnClick="btnCloseEditMachinePopup_Click" />
                            <asp:Button ID="Button1" runat="server" class="btn btn-success" OnClick="EditMachine_Click" Text="Save"/>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Delete Machine Modal -->
            <div runat="server" id="deleteMachineModal" class="modal">
                <div class="modal-content">
                    <div class="container">
                        <div class="modal-header">
                            <h3 class="modal-title" id="DeleteMachineModalLabel">Delete Machine</h3>
                            <h5>Machine ID: <asp:Label ID="lblDeleteMachineId" runat="server"></asp:Label></h5>
                        </div>
                       <div class="modal-body">
                            <label>Are you sure you want to delete this machine?</label>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="Button4" runat="server" class="btn btn-secondary" Text="Cancel" OnClick="btnCloseDeleteMachinePopup_Click" />
                            <asp:Button ID="Button5" runat="server" class="btn btn-success" OnClick="DeleteMachine_Click" Text="Confirm"/>
                        </div>
                    </div>
                </div>
            </div>
            

            <!-- Open machine create modal -->
            <asp:Button ID="btnShowPopup" runat="server" Text="Create New Machine" style="background-color:#009999; color:white" class="btn" OnClick="btnShowPopup_Click" />
            <br>

            <label>Change Timer(Seconds): </label>
            <asp:TextBox ID="txtTimerSet" type="text" runat="server" class="input-field" name="mName"/>
            
            <asp:Button ID="btnTimer" UseSubmitBehavior="false" runat="server" class="btn btn-success" Text="Set" OnClick="timerSet_Click"/>
            <asp:Label ID="lblTimerError" ForeColor="#d20000" runat="server"></asp:Label>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="updatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    <asp:Timer ID="timer" runat="server" Interval="1000" OnTick="WebAppTimerTick" />
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
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="MachineGridView" />
                </Triggers>
            </asp:UpdatePanel>
        </form>
    </div>
    
  
       
</body>
</html>
