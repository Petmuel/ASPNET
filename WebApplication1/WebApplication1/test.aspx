<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="WebApplication1.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        <form id="formRegister2" runat="server" novalidate>
         
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="updatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    <asp:Timer ID="timer" runat="server" Interval="1000" OnTick="WebAppTimerTick" />
                    <!-- GridView Machines -->
                    <h3><asp:Label ID="lblTableHeader" runat="server">All Machines</asp:Label></h3>
             
                    <asp:GridView ID="MachineGridView" HeaderStyle-ForeColor="White" BorderColor="#000000" BorderWidth="1.5"
                        runat="server" AutoGenerateColumns="false" DataKeyNames="MachineID" HorizontalAlign="Center" CssClass="center-header">
                        <Columns>
                            <asp:BoundField DataField="MachineID" HeaderText="Id" ItemStyle-BorderWidth="1.5" HeaderStyle-BackColor="#009999" HeaderStyle-BorderWidth="1.5"/>
                            <asp:BoundField DataField="MachineName" HeaderText="Name" ItemStyle-BorderWidth="1.5" HeaderStyle-BackColor="#009999" HeaderStyle-BorderWidth="1.5"/>
                            <asp:BoundField DataField="MachineStatus" HeaderText="Status" ItemStyle-BorderWidth="1.5" HeaderStyle-BackColor="#009999" HeaderStyle-BorderWidth="1.5"/>
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
