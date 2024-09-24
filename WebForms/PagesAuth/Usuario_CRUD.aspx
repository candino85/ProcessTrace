<%@ Page Title="" Language="C#" MasterPageFile="~/PagesAuth/PageAuth.Master" AutoEventWireup="true" CodeBehind="Usuario_CRUD.aspx.cs" Inherits="WebForms.PagesAuth.Usuario_CRUD" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="body" runat="server">
    <div style="margin-top: 80px" class="p-4">
        <asp:Label ID="lblTitulo" runat="server" Class="fs-4 fw-bold mb-2"></asp:Label>
        <form runat="server" class="form-control">
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
                <div class="col-6 mb-2 mt-2">
                    <label class="form-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" Class="form-control" />
                </div>
            <div class="d-flex mb-2 mt-2">
                <div class="col-3 me-2  mb-2 mt-2">
                    <asp:CheckBox ID="chkActivo" runat="server" Class="form-control" Text="Activo" />
                </div>
                <div class="col-3 me-2 mb-2 mt-2">
                    <asp:CheckBox ID="chkBloqueado" runat="server" Class="form-control" Text="Bloqueado" />
                </div>
                </div>
            <div class="container-btn">
                <asp:button class="'btn btn-secondary col-3 me-2" runat="server" PostBackUrl="/PagesAuth/Usuarios.aspx" Text="Volver"/>
                <asp:button class="'btn btn-primary col-3 me-2" ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />                
            </div>
        </form>
    </div>
</asp:Content>
