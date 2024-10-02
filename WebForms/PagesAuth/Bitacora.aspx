<%@ Page Title="" Language="C#" MasterPageFile="~/PagesAuth/PageAuth.Master" AutoEventWireup="true" CodeBehind="Bitacora.aspx.cs" Inherits="WebForms.PagesAuth.Bitacora" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="body" runat="server">
    <section class="default align-items-stretch p-3  bg-secondary">
        <asp:Label ID="lblTitulo" runat="server" Class="fs-5 fw-bold text-light">Consultar Bitácora</asp:Label>
        <form runat="server" class="form form-control">
            <div class="container-fluid  row ">
                <div class="accordion" id="accordionPanelsStayOpenExample">
                    <div class="accordion-item">
                        <h2 class="accordion-header">
                            <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#panelsStayOpen-collapseOne" aria-expanded="true" aria-controls="panelsStayOpen-collapseOne">
                                Filtros de búsqueda
                            </button>
                        </h2>
                        <div id="panelsStayOpen-collapseOne" class="accordion-collapse collapse show">
                            <div class="accordion-body">

                                <div class="row mb-2 mt-2">
                                    <div class="col-12  d-flex">
                                        <div class=" col-3 me-2">
                                            <label class="form-label">Usuario</label>
                                            <asp:DropDownList ID="ddlUsuario" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="col-2 me-2">
                                            <label class="form-label">Fecha Desde</label>
                                            <asp:TextBox ID="txtFechaDesde" runat="server" class="form-control" TextMode="Date"></asp:TextBox>
                                        </div>
                                        <div class="col-2 me-2">
                                            <label class="form-label">Fecha Hasta</label>
                                            <asp:TextBox ID="txtFechaHasta" runat="server" class="form-control" TextMode="Date"></asp:TextBox>
                                        </div>
                                        <div class="col-3 me-2 ">
                                            <label class="form-label">Mensaje</label>
                                            <asp:TextBox ID="txtMensaje" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row mb-2 mt-2">
                                    <div class="col-12  d-flex">
                                        <div class="col-3 me-2">
                                            <label class="form-label">Módulo</label>
                                            <asp:DropDownList ID="ddlModulo" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="col-3 me-2">
                                            <label class="form-label">Operación</label>
                                            <asp:DropDownList ID="ddlOperacion" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                        <div class="col-3 me-2">
                                            <label class="form-label">Criticidad</label>
                                            <div class="d-flex">
                                                <asp:CheckBox ID="chkInformacion" runat="server" Class="form-control" Text="Info" Style="background-color: yellow" />
                                                <asp:CheckBox ID="chkAdvertencia" runat="server" Class="form-control" Text="Adver" Style="background-color: orange" />
                                                <asp:CheckBox ID="chkError" runat="server" Class="form-control" Text="Error" Style="background-color: red" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row ">
                <div class="container-btn text-center mb-2 mt-2">
                    <asp:Button class="btn btn-secondary col-3" ID="btnLimpiar" runat="server" Text="Limpiar filtros" OnClick="btnLimpiar_Click" />
                    <asp:Button class="btn btn-primary col-3" ID="btnFiltrar" runat="server" Text="Aplicar filtros" OnClick="btnFiltrar_Click" />
                </div>
            </div>
            <br />
            <div class="container-row mb-5 mt-2">
                <div class="col-12">
                    <asp:GridView ID="GVBitacora" runat="server" class="table table-borderless table-hover overflow-auto" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="Criticidad" HeaderText="Criticidad" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:BoundField DataField="FechaHora" HeaderText="Fecha Hora" />
                            <asp:BoundField DataField="Modulo" HeaderText="Módulo" />
                            <asp:BoundField DataField="Operacion" HeaderText="Operación" />
                            <asp:BoundField DataField="Mensaje" HeaderText="Mensaje" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </form>
    </section>
</asp:Content>
