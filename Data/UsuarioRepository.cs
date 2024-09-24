using Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class UsuarioRepository
    {
        protected string Conexion = ConfigurationManager.ConnectionStrings["ProcessTrace"].ConnectionString.ToString();
        public List<Usuario_CE> Listar()
        {
            List<Usuario_CE> usuarios = new List<Usuario_CE>();

            using (SqlConnection oConexion = new SqlConnection(Conexion))
            {
                oConexion.Open();

                using (SqlCommand oComando = oConexion.CreateCommand())
                {
                    oComando.CommandType = CommandType.StoredProcedure;
                    oComando.CommandText = "Usuario_GetAll";

                    try
                    {
                        using (SqlDataReader dr = oComando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Usuario_CE oUsuario = new Usuario_CE
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Apellido = dr["Apellido"].ToString(),
                                    Email = dr["Email"].ToString(),
                                    Activo = Convert.ToBoolean(dr["Activo"]),
                                    IntentosAcceso = Convert.ToInt32(dr["IntentosAcceso"]),
                                    Bloqueado = Convert.ToBoolean(dr["Bloqueado"]),
                                    UltimoAcceso = dr["UltimoAcceso"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["UltimoAcceso"]),
                                    FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"])
                                };

                                usuarios.Add(oUsuario);
                            }
                        }
                        return usuarios;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al obtener los usuarios", ex);
                    }
                }
            }
        }
        public Usuario_CE Obtener(int id)
        {
            Usuario_CE usuario = new Usuario_CE();

            using (SqlConnection oConexion = new SqlConnection(Conexion))
            {
                oConexion.Open();
                using (SqlCommand cmd = oConexion.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Usuario_GetById";                    
                    cmd.Parameters.AddWithValue("@Id", id);
                    try
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                usuario.Id = Convert.ToInt32(dr["Id"]);
                                usuario.Nombre = dr["Nombre"].ToString();
                                usuario.Apellido = dr["Apellido"].ToString();
                                usuario.Email = dr["Email"].ToString();
                                usuario.Activo = Convert.ToBoolean(dr["Activo"]);
                                usuario.IntentosAcceso = Convert.ToInt32(dr["IntentosAcceso"]);
                                usuario.Bloqueado = Convert.ToBoolean(dr["Bloqueado"]);
                                usuario.UltimoAcceso = dr["UltimoAcceso"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["UltimoAcceso"]);
                                usuario.FechaCreacion = dr["FechaCreacion"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["FechaCreacion"]);
                            }
                        }
                        return usuario;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al obtener el usuario", ex);
                    }
                }
            }                                            
        }
        public bool Crear(Usuario_CE usuario)
        {
            bool respuesta = false;
            
            using (SqlConnection oConexion = new SqlConnection(Conexion))
            {
                SqlCommand cmd = new SqlCommand("Usuario_Create", oConexion);
                cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                cmd.Parameters.AddWithValue("@Email", usuario.Apellido);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    oConexion.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                        respuesta = true;
                    return respuesta;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al crear el usuario", ex);
                }
            }
        }
        public bool Editar(Usuario_CE usuario)
        {
            bool respuesta = false;

            using (SqlConnection oConexion = new SqlConnection(Conexion))
            {
                oConexion.Open();
                using (SqlCommand cmd = oConexion.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Usuario_Update";
                    cmd.Parameters.AddWithValue("@Id", usuario.Id);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@Activo", usuario.Activo);
                    cmd.Parameters.AddWithValue("@Bloqueado", usuario.Bloqueado);
                    cmd.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        if (filasAfectadas > 0)
                            respuesta = true;
                        return respuesta;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al editar el usuario", ex);
                    }
                }               
            }
        }
        public bool Eliminar(int id)
        {
            bool respuesta = false;

            using (SqlConnection oConexion = new SqlConnection(Conexion))
            {
                SqlCommand cmd = new SqlCommand("Usuario_Delete", oConexion);
                cmd.Parameters.AddWithValue("@Id", id);               
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    oConexion.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                        respuesta = true;
                    return respuesta;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al eliminar el usuario", ex);
                }
            }
        }
    }
}
