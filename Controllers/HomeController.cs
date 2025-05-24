using System.Diagnostics;
using Ing_Soft.Data;
using Ing_Soft.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ing_Soft.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {        
            _logger = logger;
            _context = context;
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
            var rutaJson = Path.Combine(Directory.GetCurrentDirectory(), "Environment", "passwords.json");

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
            var usuario = _context.Cliente.FirstOrDefault(u => u.Email == email && u.Contrasenia == password);
            
            if (usuario != null)
            {
                HttpContext.Session.SetString("nombre", usuario.Nombre);
                HttpContext.Session.SetString("Rol", "Cliente");
                HttpContext.Session.SetInt32("idCliente", usuario.ID_Cliente);
                return RedirectToAction("Index", "Tienda");
            }
            else
            {
                TempData["Error"] = "Usuario o contraseña incorrectos";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RegistroUsuario(string nombreCompleto, string email, string telefono, string direccion, string password)
        {
            // Verificar que no exista un cliente con ese email
            if (_context.Cliente.Any(u => u.Email == email))
            {
                TempData["Error"] = "Ya existe un usuario con ese correo electrónico.";
                return RedirectToAction("Index", "Home");
            }

            // Crear nuevo cliente
            var nuevoCliente = new Cliente
            {
                Nombre = nombreCompleto,
                Email = email,
                Telefono = telefono,
                Direccion = direccion,
                Contrasenia = password
            };

            _context.Cliente.Add(nuevoCliente);
            await _context.SaveChangesAsync();

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
