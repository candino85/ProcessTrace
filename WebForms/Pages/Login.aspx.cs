using Negocio;
using Servicios;
using Servicios.Composite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Security;

namespace WebForms.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        private Usuario_CN _usuario;
        private Permission_CN _permisos;

        protected void Page_Load(object sender, EventArgs e)
        {
            _usuario = new Usuario_CN();
            _permisos = new Permission_CN();
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                string email = txtEmail.Text;
                string password = txtPassword.Text;

                // lógica de autenticación
                var resultado = _usuario.LogIn(email, password);
                resultado = resultado.Replace("\'", "\\'");
                
                if (SessionManager.GetInstance.IsLogged)
                {
                    // generar cookie que autentica al usuario
                    FormsAuthentication.SetAuthCookie(SessionManager.GetInstance.usuario.Email, false);

                    ObtenerPermisos();

                    Response.Redirect("../PagesAuth/Home.aspx");
                }
                else
                {
                    Response.Write("<script Language='JavaScript'> parent.alert('" + resultado.ToString() + "');</script>");
                }
            }
        }
        public void ObtenerPermisos()
        {
            // recupero los permisos de la bbdd
            IList<Component> permisos = _permisos.GetAll(SessionManager.GetInstance.usuario.Perfil.Id.ToString());

            // los agrego al perfil del usuario
            foreach (Component permiso in permisos)
            {
                if (permiso is Permission)
                {
                    Permission perm = (Permission)permiso;
                    SessionManager.GetInstance.usuario.Perfil.AddChild(perm);
                }
                else
                {
                    Role rol = (Role)permiso;
                    SessionManager.GetInstance.usuario.Perfil.AddChild(rol);
                }
            }
            // los guardo en sesion para recuperarlos posteriormente
            //Session["PermisosUsuario"] = SessionManager.GetInstance.usuario.Perfil;
        }
    }
}