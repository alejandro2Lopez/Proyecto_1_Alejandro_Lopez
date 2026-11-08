using Proyecto_1_Alejandro_Lopez.Models.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace Proyecto_1_Alejandro_Lopez.Data
{
    public class TicketData
    {
        private static TicketData instance;

        public TicketData()
        {

        }
        public static TicketData getInstance()
        {

            if (instance == null)
            {
                instance = new TicketData();
            }
            return instance;
        }


        public List<EstadoTicket> GetEstadoTickets()
        {
            List<EstadoTicket> EstadoTickets = new List<EstadoTicket>();

            string cadena = ConfigurationManager
                .ConnectionStrings["ConexionBD"]
                .ConnectionString;

            using (SqlConnection conexion = new SqlConnection(cadena))
            {
                string query = "SELECT  id_estado_ticket, descripcion FROM dbo.vw_EstadosTicket";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    conexion.Open();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EstadoTicket EstadoTicket = new EstadoTicket
                            {
                                id_estado_ticket = Convert.ToInt32(reader["id_estado_ticket"]),
                                descripcion = reader["descripcion"].ToString()
                            };

                            EstadoTickets.Add(EstadoTicket);
                        }
                    }
                }
            }

            return EstadoTickets;
        }
    
            public bool CrearTicket(Ticket ticket)
        {
            try
            {
                string cadenaConexion = ConfigurationManager
                    .ConnectionStrings["ConexionBD"]
                    .ConnectionString;
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                using (SqlCommand comando = new SqlCommand(
                    "dbo.pa_CrearTicket", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@titulo",
                        SqlDbType.NVarChar, 100).Value = ticket.titulo;

                    comando.Parameters.Add("@descripcion",
                        SqlDbType.NVarChar, 100).Value = ticket.descripcion;

                    comando.Parameters.Add("@fk_id_usuario_solicitante",
                        SqlDbType.NVarChar, 100).Value = ticket.fk_id_usuario_solicitante;
                    comando.Parameters.Add("@fk_id_categoria",
                       SqlDbType.NVarChar, 100).Value = ticket.fk_id_usuario_solicitante;
                    comando.Parameters.Add("@fk_id_usuario_asignado",
                        SqlDbType.TinyInt).Value = 1 ;

                    conexion.Open();

                    comando.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool AgregarComentario(Comentario comentario)
        {
            try
            {
                string cadenaConexion = ConfigurationManager
                    .ConnectionStrings["ConexionBD"]
                    .ConnectionString;
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                using (SqlCommand comando = new SqlCommand(
                    "dbo.pa_InsertarComentario", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@id_ticket",
                        SqlDbType.NVarChar, 100).Value = comentario.id_ticket;

                    comando.Parameters.Add("@id_usuario",
                        SqlDbType.NVarChar, 100).Value = comentario.id_usuario;

                    comando.Parameters.Add("@texto",
                        SqlDbType.NVarChar, 100).Value = comentario.texto;
                  

                    conexion.Open();

                    comando.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public List<Comentario> GetComentariosPorId(int id)
        {
            List<Comentario> comentarios = new List<Comentario>();

            string cadena = ConfigurationManager
                .ConnectionStrings["ConexionBD"]
                .ConnectionString;

            using (SqlConnection conexion = new SqlConnection(cadena))
            {
                string query = @"SELECT id_comentario,
                        id_ticket,
                        id_usuario,
                        texto,
                        fecha,
                        id_comentario
                 FROM dbo.vw_ComentariosPorTicket
                 WHERE id_ticket = @id_ticket
                 ORDER BY fecha DESC";

                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.Add("@id_ticket", SqlDbType.Int).Value = id;
                    conexion.Open();

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Comentario comentario = new Comentario
                            {
                                id_comentario = Convert.ToInt32(reader["id_comentario"]),
                                texto = reader["texto"].ToString(),
                                id_usuario = Convert.ToInt32(reader["id_usuario"]),
                                id_ticket = Convert.ToInt32(reader["id_ticket"]),
                                fecha = Convert.ToDateTime(reader["fecha"]).ToString("dd/MM/yyyy hh:mm tt")
                            };

                            comentarios.Add(comentario);
                        }
                    }
                }
            }

            return comentarios;
        }

    }
}

