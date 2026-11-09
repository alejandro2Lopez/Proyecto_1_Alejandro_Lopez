using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_1_Alejandro_Lopez.Models.Entities
{
    public class Ticket
    {
        public int id_ticket { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public int fk_id_usuario_solicitante { get; set; }
        public int? fk_id_usuario_asignado { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_actualizacion { get; set; }
        public byte fk_id_categoria { get; set; }
        public byte fk_id_estado_ticket { get; set; }

        // Atributos adicionales de la vista
        public string usuarioSolicitante { get; set; }
        public string UsuarioAsignado { get; set; }
    }
}