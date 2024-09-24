using Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class BitacoraRepository
    {
        protected string Conexion = ConfigurationManager.ConnectionStrings["ProcessTrace"].ConnectionString.ToString();
        public List<Evento_CE> GetAll()
        {
            List<Evento_CE> eventos = new List<Evento_CE>();

            using (SqlConnection oConexion = new SqlConnection(Conexion))
            {
                SqlCommand cmd = new SqlCommand("select * from fn_bitacora()", oConexion);
                cmd.CommandType = CommandType.Text;
                try
                {
                    oConexion.Open();
                    using(SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Evento_CE oEvento = new Evento_CE
                            {
                                Id = Convert.ToInt32(dr["IdBitacora"]),
                                Usuario = dr["Usuario"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                Fecha = Convert.ToDateTime(dr["FechaRegistro"]),
                                Criticidad = Convert.ToInt16(dr["Activo"]),
                                DVH = dr["DVH"].ToString()
                            };

                            eventos.Add(oEvento);
                        }
                    }
                    return eventos;
                }
                catch (Exception ex)
                {

                    throw new Exception("Error al obtener los usuarios", ex);
                }
            }
        }
    }
}
