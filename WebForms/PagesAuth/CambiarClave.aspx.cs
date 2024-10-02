using Entidades;
using Negocio;
using Servicios;
using System;

namespace WebForms.PagesAuth
{
    public partial class CambiarClave : System.Web.UI.Page
    {
        Usuario_CN usuario_CN;
        protected void Page_Load(object sender, EventArgs e)
        {
            usuario_CN = new Usuario_CN();
            lblTitulo.Text = "Cambiar Clave";
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Usuario_CE usuario = SessionManager.GetInstance.usuario;

            if (Encrypt.GetSHA256(txtClaveActual.Text) == usuario.Clave)
            {
                usuario.Clave = Encrypt.GetSHA256(txtClaveNueva.Text);
                usuario_CN.ActualizarClave(usuario);
                lblMensaje.Text = "Clave modificada correctamente";
                lblMensaje.ForeColor = System.Drawing.Color.Blue;

                LimpiarCampos();
            }
            else
            {
                lblMensaje.Text = "Clave actual incorrecta";
            }
        }
        protected void LimpiarCampos()
        {
            txtClaveActual.Text = "";
            txtClaveNueva.Text = "";
            txtClaveNueva2.Text = "";
        }

    }  
}