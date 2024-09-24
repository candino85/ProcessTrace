<%@ Page Title="" Language="C#" MasterPageFile="Page.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebForms.Pages.Login" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Styles.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="body" runat="server" >
    <form id="form1" runat="server">
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-md-6 mt-3 mb-3">
                    <h2 class="text-center">Inicio de sesión</h2>
                    <div class="card">
                        <div class="card-body">
                            <div class="form-group mt-3">
                                <label for="emailInput">Correo Electrónico</label>
                                <asp:TextBox ID="emailInput" runat="server" CssClass="form-control" TextMode="Email" />
                                <asp:RequiredFieldValidator ControlToValidate="emailInput" ErrorMessage="El correo electrónico es requerido" Display="Dynamic" runat="server" CssClass="text-danger" />
                            </div>
                            <div class="form-group mt-3">
                                <label for="passwordInput">Contraseña</label>
                                <asp:TextBox ID="passwordInput" runat="server" CssClass="form-control" TextMode="Password" />
                                <asp:RequiredFieldValidator ControlToValidate="passwordInput" ErrorMessage="La contraseña es requerida" Display="Dynamic" runat="server" CssClass="text-danger" />
                            </div>
                            <div class="text-center mt-3">
                                <asp:Button ID="loginButton" runat="server" Text="Ingresar" CssClass="btn btn-primary" OnClick="LoginButton_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
