using Proyecto_1_Alejandro_Lopez.Data;
using Proyecto_1_Alejandro_Lopez.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_1_Alejandro_Lopez.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ValidarUsuario(string email, string password)
        {
            Usuario usuario = UsuarioData.getInstance().ValidarUsuario(email, password);
            if (usuario != null)
            {
                Session["UsuarioId"] = usuario.id_usuario;
                Session["NombreUsuario"] = usuario.nombre_usuario;
                Session["RolId"] = usuario.fk_id_rol;
                return Json(new
                {
                    status = 200,
                    message = "Bienvenido: " + usuario.nombre_usuario
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    status = 403,
                    message = "Acceso prohibido intente de nuevo."
                }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}