using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForms.PagesAuth
{
    public partial class Bitacora : System.Web.UI.Page
    {
        Bitacora_CN bitacoraCN = new Bitacora_CN();
        Usuario_CN usuario_CN = new Usuario_CN();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                MostrarBitacora();
                CargarDDL();
            }
            
        }

        private void CargarDDL()
        {
            ddlUsuario.DataSource = usuario_CN.Listar();
            ddlUsuario.DataValueField = "Id";
            ddlUsuario.DataTextField = "Email";            
            ddlUsuario.DataBind();
            ddlUsuario.Items.Insert(0, new ListItem("", "0"));

            ddlModulo.AppendDataBoundItems = true;
            ddlModulo.Items.Insert(0, new ListItem("", "0"));
            var modulo = bitacoraCN.Listar().Select(m => m.Modulo).Distinct().ToList();
            ddlModulo.DataSource = modulo;
            ddlModulo.DataBind();

            ddlOperacion.AppendDataBoundItems = true;
            ddlOperacion.Items.Insert(0, new ListItem("", "0"));
            var operacion = bitacoraCN.Listar().Select(o => o.Operacion).Distinct().ToList();
            ddlOperacion.DataSource = operacion;
            ddlOperacion.DataBind();

        }
        private void MostrarBitacora()
        {
            List<Evento_CE> eventos = bitacoraCN.Listar();

            GVBitacora.DataSource = eventos;
            GVBitacora.DataBind();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            Filtro filtro = new Filtro
            {
                IdUsuario = int.Parse(ddlUsuario.SelectedValue.ToString()),
                FechaHoraDesde = !string.IsNullOrEmpty(txtFechaDesde.Text) ? (DateTime?)DateTime.Parse(txtFechaDesde.Text) : null,
                FechaHoraHasta = !string.IsNullOrEmpty(txtFechaHasta.Text) ? (DateTime?)DateTime.Parse(txtFechaHasta.Text) : null,
                Modulo = ddlModulo.SelectedValue != "0" ? ddlModulo.SelectedValue : null,
                Operacion = ddlOperacion.SelectedValue != "0" ? ddlOperacion.SelectedValue : null,
                Criticidad = new Dictionary<string, bool>{
                    { "1", chkInformacion.Checked },
                    { "2", chkAdvertencia.Checked },
                    { "3", chkError.Checked }
                },
                Mensaje = txtMensaje.Text
            };

            List<Evento_CE> eventos = bitacoraCN.Listar(filtro);

            GVBitacora.DataSource = eventos;
            GVBitacora.DataBind();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            ddlUsuario.SelectedValue = "0";
            txtFechaDesde.Text = "";
            txtFechaHasta.Text = "";
            ddlModulo.SelectedValue = "0";
            ddlOperacion.SelectedValue = "0";
            chkInformacion.Checked = false;
            chkAdvertencia.Checked = false;
            chkError.Checked = false;
            txtMensaje.Text = "";

            MostrarBitacora();
        }
    }
}