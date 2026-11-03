using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_1_Alejandro_Lopez.Models.Entities
{
    public class Usuario
    {
        public int id_usuario { get; set; }
        public string nombre_usuario { get; set; }
        public string email_usuario { get; set; }
        public string contrasenia_usuario { get; set; }
        public DateTime fecha_registro { get; set; }
        public bool estado { get; set; }
        public byte fk_id_rol { get; set; }
    }
}