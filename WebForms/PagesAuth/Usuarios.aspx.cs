using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;


namespace WebForms.PagesAuth
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        Usuario_CN usuarioCN = new Usuario_CN();

        protected void Page_Load(object sender, EventArgs e)
        {
            MostrarUsuarios();
        }

        private void MostrarUsuarios()
        {
            List<Usuario_CE> usuarios = usuarioCN.Listar();

            GVUsuarios.DataSource = usuarios;
            GVUsuarios.DataBind();
        }

        protected void Crear_Click(object sender, EventArgs e)
        {
            Response.Redirect($"/PagesAuth/Usuario_CRUD.aspx?Id=0");
            //Usuario_CE usuario = new Usuario_CE
            //{
            //    Nombre = txtNombre.Text,
            //    Apellido = txtApellido.Text,
            //    Email = txtEmail.Text,
            //    Activo = chkActivo.Checked,
            //    IntentosAcceso = int.Parse(txtIntentosAcceso.Text),
            //    Bloqueado = chkBloqueado.Checked,
            //    UltimoAcceso = DateTime.Parse(txtUltimoAcceso.Text),
            //    FechaCreacion = DateTime.Parse(txtFechaCreacion.Text)
            //};

            //usuarioCN.Crear(usuario);
            //GetUsuarios();
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


        //private void GetUsuarios()
        //{
        //    usuario = new Negocio.Usuario();
        //    var usuarios = usuario.GetUsuarios();

        //    foreach (Usuario_CE u in usuarios)
        //    {
        //        TableRow row = new TableRow();

        //        TableCell Id = new TableCell { Text = u.Id.ToString() };
        //        TableCell Nombre = new TableCell { Text = u.Nombre };
        //        TableCell Apellido = new TableCell { Text = u.Apellido };
        //        TableCell Email = new TableCell { Text = u.Email };
        //        TableCell Activo = new TableCell { Text = u.Activo.ToString() };
        //        TableCell IntentosAcceso = new TableCell { Text = u.IntentosAcceso.ToString() };
        //        TableCell Bloqueado = new TableCell { Text = u.Bloqueado.ToString() };
        //        TableCell UltimoAcceso = new TableCell { Text = u.UltimoAcceso.ToString() };
        //        TableCell FechaCreacion = new TableCell { Text = u.FechaCreacion.ToString() };

        //        // Create Edit button
        //        Button btnEdit = new Button
        //        {
        //            Text = "Editar",
        //            OnClientClick = $"openEditModal({u.Id}); return false;"
        //        };

        //        // Create Delete button
        //        Button btnDelete = new Button
        //        {
        //            Text = "Delete",
        //            OnClientClick = $"openDeleteModal({u.Id}); return false;"

        //        };

        //        TableCell cellActions = new TableCell();
        //        cellActions.Controls.Add(btnEdit);
        //        cellActions.Controls.Add(new Literal { Text = " " }); // Add space between buttons
        //        cellActions.Controls.Add(btnDelete);

        //        row.Cells.Add(Id);
        //        row.Cells.Add(Nombre);
        //        row.Cells.Add(Apellido);
        //        row.Cells.Add(Email);
        //        row.Cells.Add(Activo);
        //        row.Cells.Add(IntentosAcceso);
        //        row.Cells.Add(Bloqueado);
        //        row.Cells.Add(UltimoAcceso);
        //        row.Cells.Add(FechaCreacion);
        //        row.Cells.Add(cellActions);

        //        tableContent.Rows.Add(row);
        //    }           
        //}
    }
}