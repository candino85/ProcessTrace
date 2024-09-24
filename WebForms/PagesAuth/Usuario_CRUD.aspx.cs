using System;
using Entidades;
using Negocio;
using System.Globalization;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web.UI;

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
                    if(id != 0)
                    {
                        lblTitulo.Text = "Editar Usuario";
                        btnGuardar.Text = "Actualizar";

                        Usuario_CE usuario = usuarioCN.Obtener(id);
                        txtNombre.Text = usuario.Nombre;
                        txtApellido.Text = usuario.Apellido;
                        txtEmail.Text = usuario.Email;
                        chkActivo.Checked = usuario.Activo;
                        chkBloqueado.Checked = usuario.Bloqueado;
                        //CargarPermisos(usuario.Permiso.IdPermiso.ToString()); Agregar permisos de usuario

                    }
                    else
                    {
                        lblTitulo.Text = "Crear Usuario";
                        btnGuardar.Text = "Guardar";
                        //CargarPermisos();  Agregar permisos de usuario
                    }
                }
                else
                    Response.Redirect("/PagesAuth/Usuarios.aspx");
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Usuario_CE usuario = new Usuario_CE
            {
                Id = id,
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Email = txtEmail.Text,
                Activo = chkActivo.Checked,
                Bloqueado = chkBloqueado.Checked,
                //Permiso = new Permiso_CE { IdPermiso = int.Parse(ddlPermisos.SelectedValue) } Agregar permisos de usuario
            };

            bool respuesta;

            if (id != 0)
                respuesta = usuarioCN.Editar(usuario);
            else
                respuesta = usuarioCN.Crear(usuario);

            if (respuesta)
                Response.Redirect("/PagesAuth/Usuarios.aspx");
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Error al realizar la operación');", true);
        }

        //Agregar permisos de usuario
        //private void CargarPermisos(string IdPermiso = "")
        //{
        //    List<Permiso_CE> permisos = permisoCN.Listar();

        //    ddlPermisos.DataTextField = "Descripcion";
        //    ddlPermisos.DataValueField = "Id";

        //    ddlPermisos.DataSource = permisos;
        //    ddlPermisos.DataBind();

        //    if(IdPermiso != "")
        //        ddlPermisos.SelectedValue = IdPermiso;
        //}
    }
}