<%@ Page Title="" Language="C#" MasterPageFile="~/PagesAuth/PageAuth.Master" AutoEventWireup="true" CodeBehind="CambiarClave.aspx.cs" Inherits="WebForms.PagesAuth.CambiarClave" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="body" runat="server">

    <section class="default align-items-stretch p-3  bg-secondary">
        <div class="container-fluid">
            <asp:Label ID="lblTitulo" runat="server" Class="fs-5 fw-bold text-light"></asp:Label>
            <form runat="server" class="form form-control">
                <div class="mb-2 mt-2">
                    <div class="col-6">
                    <div class="col">
                        <label class="form-label">Clave actual</label>
                        <asp:TextBox ID="txtClaveActual" runat="server" CssClass="form-control" TextMode="Password"/>
                        <asp:RequiredFieldValidator
                            ID="rfvClaveActual"
                            runat="server"
                            ControlToValidate="txtClaveActual"
                            ErrorMessage="La clave actual es requerida."
                            CssClass="text-danger" />
                    </div>

                    <div class="col">
                        <label class="form-label">Clave nueva</label>
                        <asp:TextBox ID="txtClaveNueva" runat="server" CssClass="form-control" TextMode="Password" />
                        <asp:RequiredFieldValidator
                            ID="rfvClaveNueva"
                            runat="server"
                            ControlToValidate="txtClaveNueva"
                            ErrorMessage="La clave nueva es requerida."
                            CssClass="text-danger" />
                        <asp:RegularExpressionValidator
                            ID="revClaveNueva"
                            runat="server"
                            ControlToValidate="txtClaveNueva"
                            ErrorMessage="La clave debe tener al menos 10 caracteres, con 1 letra mayúscula, 1 letra minúscula, 1 número y 1 carácter especial (!@#$%&)."
                            CssClass="text-danger"
                            ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%&])[A-Za-z\d!@#$%&]{10,}$" />
                    </div>

                    <div class="col">
                        <label class="form-label">Repetir clave nueva</label>
                        <asp:TextBox ID="txtClaveNueva2" runat="server" CssClass="form-control" TextMode="Password" />
                        <asp:RequiredFieldValidator
                            ID="rfvClaveNueva2"
                            runat="server"
                            ControlToValidate="txtClaveNueva2"
                            ErrorMessage="Debe repetir la nueva clave."
                            CssClass="text-danger" />
                        <asp:CompareValidator
                            ID="cvClaveNueva"
                            runat="server"
                            ControlToValidate="txtClaveNueva2"
                            ControlToCompare="txtClaveNueva"
                            ErrorMessage="Las claves no coinciden."
                            CssClass="text-danger" />
                    </div>
                        </div>

                    <div class="row">
                        <p>
                            La nueva clave deberá contener al menos 10 caracteres y debe incluir 1 letra mayúscula, 1 letra minúscula, 1 número y 1 carácter especial (!@#$%&).
                        </p>
                    </div>
                    <div class="row">
                        <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger" />
                    </div>
                    <div class="container-btn mb-4">
                        <asp:LinkButton class="btn btn-sm btn-primary col-3 me-2" ID="btnGuardar" runat="server" Text="Cambiar Clave" OnClick="btnGuardar_Click" />
                    </div>
                </div>
            </form>
        </div>
    </section>

</asp:Content>
