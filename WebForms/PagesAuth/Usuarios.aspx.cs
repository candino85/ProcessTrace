using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;
using Servicios;


namespace WebForms.PagesAuth
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        Usuario_CN usuarioCN = new Usuario_CN();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { 
            MostrarUsuarios();
            }
        }
        private void MostrarUsuarios()
        {
            List<Usuario_CE> usuarios = usuarioCN.Listar();

            GVUsuarios.DataSource = usuarios;
            GVUsuarios.DataBind();
        }
        protected void Crear_Click(object sender, EventArgs e)
        {
            Response.Redirect("Usuario_CRUD.aspx?Id=0");
        }
        protected void Editar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string Id = btn.CommandArgument;

            Response.Redirect($"/PagesAuth/Usuario_CRUD.aspx?Id={Id}");
        }
        protected void Eliminar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string Id = btn.CommandArgument;

            bool respuesta = usuarioCN.Eliminar(int.Parse(Id));

            if (respuesta)
            {
                MostrarUsuarios();
            }

            //Response.Redirect($"/PagesAuth/Usuario_CRUD.aspx?Id={Id}");
        }
        protected async void Reset_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string Id = btn.CommandArgument;

            var usuario = usuarioCN.Obtener(int.Parse(Id));

            //Agregar confirm js antes de correr esto
            if (await usuarioCN.ResetClave(usuario))
            {
                Response.Write("<script Language='JavaScript'> parent.alert(La clave del usuario '" + usuario.Email + "' se ha reseteado correctamente y ha sido notificado a su correo.);</script>");
            }
            //Response.Redirect($"/PagesAuth/Usuario_CRUD.aspx?Id={Id}");
        }
    }
}