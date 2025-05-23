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
            HttpContext.Session.Clear(); // Para limpiar la sesión
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

        public IActionResult Login(string usuario, string password)
        {
            var rutaJson = Path.Combine(Directory.GetCurrentDirectory(), "Enviroment", "passwords.json");

                if (!System.IO.File.Exists(rutaJson))
                {
                    TempData["Error"] = "Archivo de contraseñas no encontrado.";
                    return RedirectToAction("Index", "Home");
                }

                var json = System.IO.File.ReadAllText(rutaJson);

                using var doc = System.Text.Json.JsonDocument.Parse(json);
                var root = doc.RootElement;

                foreach (var user in root.EnumerateArray())
                {
                    var usr = user.GetProperty("usuario").GetString();
                    var pwd = user.GetProperty("password").GetString();

                    if (usr == usuario && pwd == password)
                    {
                        Console.WriteLine("Usuario correcto");
                        HttpContext.Session.SetString("Rol", "Administrador");
                        return RedirectToAction("Index", "AdminHome");
                    }
                }
            TempData["Error"] = "Usuario o contraseña incorrectos";
            return RedirectToAction("Index", "Home");
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
