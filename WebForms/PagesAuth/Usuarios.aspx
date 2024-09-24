<%@ Page Title="" Language="C#" MasterPageFile="~/PagesAuth/PageAuth.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="WebForms.PagesAuth.WebForm1" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="body" runat="server">
    <form runat="server" style="margin-top: 80px">
            <div class="row p-3 d-flex">
                <div class="col-1">
                    <asp:Button runat="server" CssClass="btn btn-sm btn-primary form-control" Text="Crear" OnClick="Crear_Click"/>
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
</asp:Content>
