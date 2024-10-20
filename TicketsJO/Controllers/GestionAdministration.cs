using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TicketsJO.Models;

namespace TicketsJO.Controllers
{
    public class GestionAdministration : Controller
    {
        private readonly ILogger<GestionAdministration> _logger;

        public GestionAdministration(ILogger<GestionAdministration> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
