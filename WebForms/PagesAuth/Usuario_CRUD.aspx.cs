using System;
using Entidades;
using Negocio;
using System.Globalization;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web.UI;
using Servicios.Composite;

namespace WebForms.PagesAuth
{    
    public partial class Usuario_CRUD : System.Web.UI.Page
    {
        private static int id = 0;
        Usuario_CN usuarioCN = new Usuario_CN();
        //TODO: Agregar permisos de usuario

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    id = int.Parse(Request.QueryString["Id"]);

                    if(id != 0) // Editar usuario
                    {
                        lblTitulo.Text = "Editar Usuario";
                        btnGuardar.Text = "Actualizar";

                        Usuario_CE usuario = usuarioCN.Obtener(id);
                        txtNombre.Text = usuario.Nombre;
                        txtApellido.Text = usuario.Apellido;
                        txtEmail.Text = usuario.Email;
                        chkActivo.Checked = usuario.Activo;
                        chkBloqueado.Checked = usuario.Bloqueado;
                        ddlPerfil.SelectedIndex = usuario.Perfil.Id;
                        //CargarPermisos(usuario.Permiso.IdPermiso.ToString()); Agregar permisos de usuario

                    }
                    else //Crear usuario
                    {
                        lblTitulo.Text = "Crear Usuario";
                        btnGuardar.Text = "Guardar";
                        //CargarPermisos();  Agregar permisos de usuario
                    }
                }
                else
                    Response.Redirect("PagesAuth/Usuarios.aspx");
            }
        }

        // Inside the btnGuardar_Click method
        protected async void btnGuardar_Click(object sender, EventArgs e)
        {
            Usuario_CE usuario = new Usuario_CE
            {
                Id = id,
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Email = txtEmail.Text,
                Activo = chkActivo.Checked,
                Bloqueado = chkBloqueado.Checked,
                Perfil = new Role { Id = int.Parse(ddlPerfil.SelectedValue) }
            };

            bool respuesta;

            try
            {
                if (id != 0)
                    respuesta = usuarioCN.Editar(usuario);
                else
                    respuesta = await usuarioCN.Crear(usuario);

                if (respuesta)
                    Response.Redirect("/PagesAuth/Usuarios.aspx");
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Error al realizar la operación');", true);
            }
            catch (Exception ex)
            {
                //Response.Write($"<script>window.alert('{ex.ToString()}');</script>");
                //Response.Write($"<script Language='JavaScript'> parent.alert('{ex.ToString()}');</script>");
            }
        }
    }
}