<%@ Page Title="" Language="C#" MasterPageFile="~/PagesAuth/PageAuth.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="WebForms.PagesAuth.WebForm1" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="body" runat="server">
    <div style="margin-top: 50px" class="p-3  bg-secondary">
        <asp:Label runat="server" Class="fs-5 fw-bold text-light" Text=" Gestión de usuarios"></asp:Label>
        <form runat="server" class="form-control " style="background-color:lightgray" >
            <div class="row">
                <div class="col-2">
                    <asp:Button runat="server" CssClass="btn btn-sm btn-primary form-control" Text="Crear" OnClick="Crear_Click" />
                </div>
            </div>
            <br />

            <div class="container-row">
                <div class="col-12">
                    <asp:GridView ID="GVUsuarios" runat="server" CssClass="table table-borderless table-hover" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="#" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:BoundField DataField="Activo" HeaderText="Activo" />
                            <asp:BoundField DataField="IntentosAcceso" HeaderText="Intentos" />
                            <asp:BoundField DataField="Bloqueado" HeaderText="Bloqueado" />
                            <asp:BoundField DataField="UltimoAcceso" HeaderText="Ultimo Acceso" />
                            <asp:BoundField DataField="FechaCreacion" HeaderText="Fecha Creacion" />
                            <asp:TemplateField HeaderText="Opciones">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("Id") %>' OnClick="Editar_Click" CssClass="btn btn-sm btn-primary">Editar</asp:LinkButton>
                                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("Id") %>' OnClick="Eliminar_Click" CssClass="btn btn-sm btn-secondary">Eliminar</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </form>
    </div>
</asp:Content>
