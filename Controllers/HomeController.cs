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

            [HttpPost]
        public IActionResult LoginUsuario(string email, string password)
        {
            var rutaJson = Path.Combine(Directory.GetCurrentDirectory(), "Enviroment", "passwordsUsers.json");

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
                var ema = user.GetProperty("email").GetString();
                var pwd = user.GetProperty("password").GetString();

                if (ema == email && pwd == password)
                {
                    var nombre = user.GetProperty("nombre").GetString();
                    var idCliente = user.GetProperty("idCliente").GetInt32();
                    Console.WriteLine("Usuario correcto");
                    HttpContext.Session.SetString("Rol", "Usuario");
                    HttpContext.Session.SetString("Usuario", email); // Guarda el nombre de usuario
                    HttpContext.Session.SetString("nombre", nombre);
                    HttpContext.Session.SetInt32("idCliente", idCliente); // Guarda el idCliente
                    return RedirectToAction("Index", "Home");
                }
            }
            TempData["Error"] = "Usuario o contraseña incorrectos";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult RegistroUsuario(string nombreCompleto, string email, string telefono, string direccion, string password)
        {
            var rutaJson = Path.Combine(Directory.GetCurrentDirectory(), "Enviroment", "passwordsUsers.json");

            List<Dictionary<string, object>> usuarios = new List<Dictionary<string, object>>();

            int nuevoId = 1;

            if (System.IO.File.Exists(rutaJson))
            {
                var jsonExistente = System.IO.File.ReadAllText(rutaJson);
                if (!string.IsNullOrWhiteSpace(jsonExistente))
                {
                    try
                    {
                        using var doc = System.Text.Json.JsonDocument.Parse(jsonExistente);
                        foreach (var user in doc.RootElement.EnumerateArray())
                        {
                            var id = user.GetProperty("idCliente").GetInt32();
                            var nombre = user.GetProperty("nombre").GetString() ?? "";
                            var correo = user.GetProperty("email").GetString() ?? "";
                            var tel = user.GetProperty("telefono").GetString() ?? "";
                            var dir = user.GetProperty("direccion").GetString() ?? "";
                            var pwd = user.GetProperty("password").GetString() ?? "";

                            usuarios.Add(new Dictionary<string, object>
                            {
                                { "idCliente", id },
                                { "nombre", nombre },
                                { "email", correo },
                                { "telefono", tel },
                                { "direccion", dir },
                                { "password", pwd }
                            });
                        }
                         nuevoId = usuarios.Max(u => Convert.ToInt32(u["idCliente"])) + 1;
                    }
                    catch
                    {
                        TempData["Error"] = "Error leyendo el archivo de usuarios.";
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            // Validar si el usuario ya existe
            if (usuarios.Any(u => u["email"] == email))
            {
                TempData["Error"] = "Ya existe un usuario con ese correo electrónico.";
                return RedirectToAction("Index", "Home");
            }

            // Agregar nuevo usuario
           usuarios.Add(new Dictionary<string, object>
            {
                { "idCliente", nuevoId },
                { "nombre", nombreCompleto },
                { "email", email },
                { "telefono", telefono },
                { "direccion", direccion },
                { "password", password }
            });

            // Guardar archivo
            var jsonNuevo = System.Text.Json.JsonSerializer.Serialize(usuarios);
            System.IO.File.WriteAllText(rutaJson, jsonNuevo);

            TempData["Mensaje"] = "Usuario registrado correctamente.";
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
