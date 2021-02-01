using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using testeRestaurante.Models;
using System.Text;
using System.Globalization;
using static testeRestaurante.Models.Pedidos;

namespace testeRestaurante.Controllers
{
    public class PedidosController : Controller
    {
        // GET: Pedidos
        public ActionResult Index()
        {
            return View();
        }

        // POST: Pedidos/Create
        [HttpPost]
        public ActionResult Create(string pedido)
        {
            string path = Server.MapPath("~/JSON/pedidos.json");

            try
            {
                Pedidos pedidos = new Pedidos();
                List<string> _pedido = new List<string>(pedidos.ValidarPedido(pedido));

                string PedidosJson = JsonSerializer.Serialize(_pedido);

                System.IO.File.WriteAllText(path, PedidosJson);

                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                ex.ToString();
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
