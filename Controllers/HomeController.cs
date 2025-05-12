using System.Diagnostics;
using Ing_Soft.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ing_Soft.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        // Para el login
        [HttpPost]
        public IActionResult Login(string rol)
        {
            HttpContext.Session.SetString("Rol", rol);
            if (rol == "Administrador")
            {
                return RedirectToAction("Index", "Producto");
            }
            else if (rol == "Usuario")
            {
                return RedirectToAction("Index", "Cliente");
            }

            return RedirectToAction("Index");
        }

        public IActionResult AccesoDenegado()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}
