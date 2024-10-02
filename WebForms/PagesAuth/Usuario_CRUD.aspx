<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/PagesAuth/PageAuth.Master" AutoEventWireup="true" CodeBehind="Usuario_CRUD.aspx.cs" Inherits="WebForms.PagesAuth.Usuario_CRUD" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="body" runat="server">
    <section class="default align-items-stretch p-3  bg-secondary">
        <div class="container-fluid">
            <asp:Label ID="lblTitulo" runat="server" Class="fs-5 fw-bold text-light"></asp:Label>
            <form runat="server" class="form form-control">
                <div class="row d-flex mb-2 mt-2">

                    <div class="col-3 me-2">
                        <label class="form-label">Nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server" Class="form-control" />
                        <asp:RequiredFieldValidator
                            ID="rfvNombre"
                            runat="server"
                            ControlToValidate="txtNombre"
                            ErrorMessage="El nombre es obligatorio."
                            CssClass="text-danger" />
                    </div>
                    <div class="col-3 me-2">
                        <label class="form-label">Apellido</label>
                        <asp:TextBox ID="txtApellido" runat="server" Class="form-control" />
                        <asp:RequiredFieldValidator
                            ID="rfvApellido"
                            runat="server"
                            ControlToValidate="txtApellido"
                            ErrorMessage="El apellido es obligatorio."
                            CssClass="text-danger" />
                    </div>
                        <div class="col-3 me-2">
                            <label class="form-label">Perfil</label>
                            <asp:DropDownList ID="ddlPerfil" runat="server" Class="form-control">
                                <asp:ListItem Text="Seleccione una opción" Value="" />
                                <asp:ListItem Text="Webmaster" Value="1" />
                                <asp:ListItem Text="Adminstrador" Value="2" />
                                <asp:ListItem Text="Usuario" Value="3" />
                            </asp:DropDownList>
                        </div>

                </div>
                <div class="mt-2">
                    <div class="col-6 me-2">
                        <label class="form-label">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" Class="form-control" TextMode="Email" />
                        <asp:RequiredFieldValidator
                            ID="rfvEmail"
                            runat="server"
                            ControlToValidate="txtEmail"
                            ErrorMessage="El email es obligatorio."
                            Display="Dynamic"
                            CssClass="text-danger" />
                    </div>
                </div>
                <div class=" d-flex mb-4 mt-4">
                    <div class="col-3 me-2">
                        <asp:CheckBox ID="chkActivo" runat="server" Class="form-control" Text="Activo" />
                    </div>
                    <div class="col-3 me-2">
                        <asp:CheckBox ID="chkBloqueado" runat="server" Class="form-control" Text="Bloqueado" />
                    </div>
                </div>
                <div class="container-btn d-flex justify-content-end mb-4">
                    <asp:LinkButton class="btn btn-sm btn-primary col-3 me-2" ID="btnGuardar" runat="server" Text="Crear usaurio" OnClick="btnGuardar_Click" />
                </div>
            </form>
        </div>
    </section>
</asp:Content>
