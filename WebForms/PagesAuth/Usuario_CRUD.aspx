<%@ Page Title="" Language="C#" MasterPageFile="~/PagesAuth/PageAuth.Master" AutoEventWireup="true" CodeBehind="Usuario_CRUD.aspx.cs" Inherits="WebForms.PagesAuth.Usuario_CRUD" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="body" runat="server">
    <section class="default align-items-stretch p-3  bg-secondary">
        <div style="margin-top: 50px ; width: 100%" class="container-fluid">
            <asp:Label ID="lblTitulo" runat="server" Class="fs-5 fw-bold text-light"></asp:Label>
            <form runat="server" class="form-control " style="background-color: lightgray">
                <div class="d-flex mb-2 mt-2">
                    <div class="col-3 me-2">
                        <label class="form-label">Nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server" Class="form-control" />
                    </div>
                    <div class="col-3 me-2">
                        <label class="form-label">Apellido</label>
                        <asp:TextBox ID="txtApellido" runat="server" Class="form-control" />
                    </div>
                </div>
                <div class="mt-2">
                    <div class="col-6 me-2">
                        <label class="form-label">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" Class="form-control" />
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
                <div class="container-btn mb-4">
                    <asp:Button class="btn btn-sm btn-secondary col-3 me-2" runat="server" PostBackUrl="/PagesAuth/Usuarios.aspx" Text="Volver" />
                    <asp:LinkButton class="btn btn-sm btn-primary col-3 me-2" ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
                </div>
            </form>
        </div>
    </section>
</asp:Content>
