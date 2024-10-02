using Servicios;
using Servicios.Composite;
using System;
using System.Security.Policy;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Xml.Linq;

namespace WebForms.PagesAuth
{
    public partial class PageAuth : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    // Redirigir a la página de login si no está autenticado
                    Response.Redirect("Pages/Login.aspx");
                }
                else
                {
                    lblUsuario.Text = HttpContext.Current.User.Identity.Name;
                    lblPerfil.Text = SessionManager.GetInstance.usuario.Perfil.Name;
                    lblUsuarioRol.Text = $"Bienvenido {SessionManager.GetInstance.usuario.Nombre} {SessionManager.GetInstance.usuario.Apellido}";

                    CargarMenu();
                //    string script = "<script type='text/javascript'>";
                //    foreach (Component permiso in SessionManager.GetInstance.usuario.Perfil.GetChild)
                //    {
                //        script += $"addItemToNavbar('{permiso.Name}', '{permiso.Permission}');";
                //    }
                //    script += "</script>";
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "AddNavbarItem", script, false);
                }
            }
        }

        private void CargarMenu()
        {
            var listado = SessionManager.GetInstance.usuario.Perfil.GetChild;
            RptMenu.DataSource = listado;
            RptMenu.DataBind();
        }
    }
}