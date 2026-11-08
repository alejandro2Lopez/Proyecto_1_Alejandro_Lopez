using Proyecto_1_Alejandro_Lopez.Data;
using Proyecto_1_Alejandro_Lopez.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_1_Alejandro_Lopez.Controllers
{
    public class TicketController : Controller
    {
        // GET: Ticket
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertarUsuario(Ticket ticket)
        {
            bool result = TicketData.getInstance().CrearTicket(ticket);
            if (result)
            {
                return Json(new
                {
                    status = 200,
                    message = "Ticket insertado manera exitosa"
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    status = 404,
                    message = "Ha ocurrido un error al ingresar el Ticket"
                }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}