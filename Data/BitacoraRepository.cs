using Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class BitacoraRepository
    {
        protected string Conexion = ConfigurationManager.ConnectionStrings["ProcessTrace"].ConnectionString.ToString();
        public List<Evento_CE> Listar()
        {
            List<Evento_CE> eventos = new List<Evento_CE>();

            using (SqlConnection oConexion = new SqlConnection(Conexion))
            {
                SqlCommand cmd = new SqlCommand("SELECT bita.[IdUsuario], usu.[Email], bita.[FechaHora], bita.[Modulo], bita.[Operacion], bita.[Criticidad], bita.[Mensaje] FROM[dbo].[Bitacora] bita LEFT JOIN dbo.Usuario usu ON usu.id = bita.IdUsuario", oConexion);
                cmd.CommandType = CommandType.Text;
                try
                {
                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Evento_CE oEvento = new Evento_CE
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Email = dr["email"].ToString(),
                                FechaHora = Convert.ToDateTime(dr["FechaHora"]),
                                Modulo = dr["Modulo"].ToString(),
                                Operacion = dr["Operacion"].ToString(),
                                Criticidad = Convert.ToInt16(dr["Criticidad"]),
                                Mensaje = dr["Mensaje"].ToString(),
                            };

                            eventos.Add(oEvento);
                        }
                    }
                    return eventos;
                }
                catch (Exception ex)
                {

                    throw new Exception("Error al obtener los eventos", ex);
                }
            }
        }
        public List<Evento_CE> GetEvents(Filtro filtro)
        {

            var cnn = new SqlConnection(Conexion);
            cnn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnn;

            var query = $@"SELECT bita.[IdUsuario]
                                 ,usu.[Email]
                                 ,bita.[FechaHora]
                                 ,bita.[Modulo]
                                 ,bita.[Operacion]
                                 ,bita.[Criticidad]
                                 ,bita.[Mensaje]
                             FROM [dbo].[Bitacora] bita
							 LEFT JOIN dbo.Usuario usu ON usu.id = bita.IdUsuario";

            if (filtro.IdUsuario != 0)
            {
                query += $@" WHERE bita.IdUsuario = {filtro.IdUsuario}";
            }
            if (filtro.Modulo != null)
            {
                if (query.Contains("WHERE"))
                    query += $@" AND bita.[Modulo] = '{filtro.Modulo}'";
                else
                    query += $@" WHERE bita.[Modulo] = '{filtro.Modulo}'";
            }
            if (filtro.Criticidad != null)
            {
                    foreach (KeyValuePair<string, bool> criti in filtro.Criticidad)
                    {
                        if (criti.Value)
                        {
                            if (query.Contains("WHERE")) 
                            { 
                                if (query.Contains("bita.[Criticidad] = "))
                                    query += $@" OR bita.[Criticidad] = {criti.Key}";
                                else
                                    query += $@" AND (bita.[Criticidad] = {criti.Key}";
                            }
                            else
                            {
                                if (query.Contains("bita.[Criticidad] = "))
                                    query += $@" OR bita.[Criticidad] = {criti.Key}";
                                else
                                    query += $@" WHERE (bita.[Criticidad] = {criti.Key}";
                            }                                
                        }
                    }
                    if (query.Contains("("))
                        query += ")";
            }
            if (filtro.Operacion != null)
            {
                if (query.Contains("WHERE"))
                    query += $@" AND bita.[Operacion] = '{filtro.Operacion}'";
                else
                    query += $@" WHERE bita.[Operacion] = '{filtro.Operacion}'";
            }
            if (filtro.Mensaje != null)
            {
                if (query.Contains("WHERE"))
                    query += $@" AND bita.[Mensaje] LIKE '%{filtro.Mensaje}%'";
                else
                    query += $@" WHERE bita.[Mensaje] LIKE '%{filtro.Mensaje}%'";
            }
            if (filtro.FechaHoraDesde != null && filtro.FechaHoraHasta != null)
            {
                if (query.Contains(" WHERE"))
                    query += $@" AND bita.[FechaHora] >= '{filtro.FechaHoraDesde}' and bita.[FechaHora] <= '{filtro.FechaHoraHasta}'";
                else
                    query += $@" WHERE bita.[FechaHora] >= '{filtro.FechaHoraDesde}' and bita.[FechaHora] <= '{filtro.FechaHoraHasta}'";
            }

            cmd.CommandText = query;

            var reader = cmd.ExecuteReader();

            List<Evento_CE> bitacoraList = new List<Evento_CE>();
            while (reader.Read())
            {
                Evento_CE evento = new Evento_CE();
                //evento.Id = reader.GetInt32(reader.GetOrdinal("id"));
                evento.IdUsuario = reader.GetInt32(reader.GetOrdinal("idUsuario"));
                evento.Email = reader["email"] != DBNull.Value ? reader.GetString(reader.GetOrdinal("email")) : "";
                evento.FechaHora = reader.GetDateTime(reader.GetOrdinal("fechaHora"));
                evento.Modulo = reader.GetString(reader.GetOrdinal("modulo"));
                evento.Operacion = reader.GetString(reader.GetOrdinal("operacion"));
                evento.Criticidad = reader.GetInt32(reader.GetOrdinal("criticidad"));
                evento.Mensaje = reader.GetString(reader.GetOrdinal("mensaje"));

                bitacoraList.Add(evento);
            }

            return bitacoraList;
        }
        public void LogEvent(int idUsuario, string modulo, string operacion, int criticidad, string msj)
        {
            var cnn = new SqlConnection(Conexion);
            cnn.Open();
            var cmd = new SqlCommand();
            cmd.Connection = cnn;

            var query = $@"INSERT INTO [dbo].[Bitacora]
                            ([IdUsuario]
                            ,[FechaHora]
                            ,[Modulo]
                            ,[Operacion]
                            ,[Criticidad]
                            ,[Mensaje])
                           VALUES
                            ({idUsuario},
                             '{DateTime.Now}',
                             '{modulo}',
                             '{operacion}',
                             {criticidad},
                             '{msj}')";

            cmd.CommandText = query;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
