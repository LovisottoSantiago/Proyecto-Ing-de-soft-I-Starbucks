namespace Ing_Soft.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Ing_Soft.Data;
    using Ing_Soft.Models;
    using Ing_Soft.Filters;
    using Microsoft.AspNetCore.Authorization;

    [RolAuthorize("Administrador")]
    public class AdminHome : Controller
    {
        private readonly AppDbContext _context;

        public AdminHome(AppDbContext context)
        {
            _context = context;
        }

        // GET: AdminHome
        public IActionResult Index()
        {
            return View();
        }

    }
    
}