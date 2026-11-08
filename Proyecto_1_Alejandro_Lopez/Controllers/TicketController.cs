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
        public ActionResult InsertarTicket(Ticket ticket)
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
                    status = 500,
                    message = "Ha ocurrido un error al ingresar el Ticket"
                }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public ActionResult AgregarComentarioTicket(Comentario comentario)
        {
            bool result = TicketData.getInstance().AgregarComentario(comentario);
            if (result)
            {
                return Json(new
                {
                    status = 200,
                    message = "Comentario agregado manera exitosa"
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    status = 500,
                    message = "Ha ocurrido un error al ingresar el comentario"
                }, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public ActionResult GetComentariosId(int id)
        {
            List<Comentario> result = TicketData.getInstance().GetComentariosPorId(id);
           
                return Json(new
                {
                    status = 200,
                    message = result
                }, JsonRequestBehavior.AllowGet);
           

        }
    }
}