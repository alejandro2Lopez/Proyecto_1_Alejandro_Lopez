using Proyecto_1_Alejandro_Lopez.Models.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Proyecto_1_Alejandro_Lopez.Data
{
    public class EstadoTicketData
    {
        private static EstadoTicketData instance;

        public EstadoTicketData()
        {

        }
        public static EstadoTicketData getInstance()
        {

            if (instance == null)
            {
                instance = new EstadoTicketData();
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
    }
}
