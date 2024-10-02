using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForms.PagesAuth
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            
            // Eliminar la cookie de autenticación
            FormsAuthentication.SignOut();

            // Opcional: Limpiar la sesión, si la usas
            Session.Abandon();
            SessionManager.GetInstance.Logout();

            // Redirigir al usuario a la página de login o página de inicio
            Response.Redirect("../Pages/Login.aspx");
        }
    }
}