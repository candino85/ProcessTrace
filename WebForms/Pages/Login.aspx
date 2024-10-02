<%@ Page Title="" Language="C#" MasterPageFile="Page.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebForms.Pages.Login" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/master.css" rel="stylesheet" />
    <script src="../Js/script.js"></script>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="body" runat="server">
        <form id="form" class="form form-control" runat="server">
            <div class="row justify-content-center">
                <div class="col-md-6 mt-3 mb-3">
                    <h2 class="text-center">Inicio de sesión</h2>
                    <div class="card">
                        <div class="card-body">
                            <div class="form-group mt-3">
                                <label for="txtEmail">Correo Electrónico</label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                                <asp:RequiredFieldValidator ControlToValidate="txtEmail" ErrorMessage="El correo electrónico es requerido" Display="Dynamic" runat="server" CssClass="text-danger" />
                            </div>
                            <div class="form-group mt-3">
                                <label for="txtPassword">Contraseña</label>
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />
                                <asp:RequiredFieldValidator ControlToValidate="txtPassword" ErrorMessage="La contraseña es requerida" Display="Dynamic" runat="server" CssClass="text-danger" />
                            </div>
                            <div class="text-center mt-3">
                                <asp:Button ID="btnLogin" runat="server" Text="Ingresar" CssClass="btn btn-primary" OnClick="btnLogin_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>

</asp:Content>
