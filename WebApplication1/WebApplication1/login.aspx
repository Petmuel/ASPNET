<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>     
    <link href="asset/style.css" rel="stylesheet" type="text/css" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,700&display=swap" rel="stylesheet">
    <style>
        body {
            font-family: 'Roboto', sans-serif;
            background-color: #f1f1f1;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }

        .container {
            width: 310px;
            padding: 20px;
            border-radius: 5px;
            background-color: #ffffff;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        h1 h2{
            text-align: center;
            margin-bottom: 20px;
            color: #333333;
            font-size: 30px;
        }

        form {
            display: flex;
            flex-direction: column;
        }

        label {
            margin-bottom: 5px;
            color: #666666;
            font-weight: 600;
        }

        .input-field {
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #cccccc;
            border-radius: 3px;
            background-color: #f9f9f9;
            color: #333333;
        }

        .login-btn {
            background-color: #4CAF50;
            color: #ffffff;
            font-size: 20px;
            cursor: pointer;
            transition: background-color 0.3s;
        }

            .login-btn:hover {
                background-color: #45a049;
            }
    </style>
</head>
<body>
     <div class="container">
        <form id="loginForm" runat="server">
            <h1>Login</h1>
            <label for="email">Email:</label>
            <input type="email" id="email" class="input-field" name="email" required>
            <label for="password">Password:</label>
            <input type="password" id="password" class="input-field" name="password" required>
            <br><asp:Label ID="Label1" runat="server" ForeColor="#d20000"></asp:Label><br><br>
          <asp:Button runat="server" UseSubmitBehavior="false" Class="login-btn" Text="Sign In" onClick="btnLogin_Click"/>
        </form>
    </div>
</body>
</html>
