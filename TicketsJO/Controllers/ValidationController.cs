
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using QRCoder;
using System.Drawing;
using TicketsJO.Data;
using TicketsJO.Models;
using ZXing.QrCode.Internal;

namespace TicketsJO.Controllers
{
    [Authorize]
    public class ValidationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public ValidationController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //
        // GET:
        /// <summary>
        /// Gère la requête GET pour afficher la page  paiement.
        /// </summary>
        /// <returns>
        /// La vue de la page paiement ou  redirection vers la page du panier si vide.
        /// </returns>
        /// <remarks>
        /// Récupère le  panier        
        /// </remarks>
        [HttpGet]
        public IActionResult ValidationEtPaiement()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext, _context);
            var cartItems = cart.GetCartItems();

            if (cartItems == null || !cartItems.Any())
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
            return View(cart);
        }

      
        /// </remarks>
        private string GenerateCleTicket(Ticket ticket)
        {
            using var sha256 = SHA256.Create();
            var combinedValue = $"{ticket.Id}{ticket.Client.CleCompte}{ticket.DateTicket:yyyyMMddHHmmss}";
            var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(combinedValue));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

      
        [HttpPost]
        public IActionResult CompletePayment()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext, _context);
            var cartItems = cart.GetCartItems();

            if (cartItems == null || !cartItems.Any())
            {
                return RedirectToAction("Index", "ShoppingCart");
            }            

            

            var ticket = new Ticket
            {              
                Client = _context.Set<User>()
                       .FirstOrDefault(o => o.Email.Equals(User.FindFirstValue(ClaimTypes.Email))),
               
                DateTicket = DateTime.Now,
                NumSerie="Ticket JO Paris 2024"               
            };

            ticket.CleTicket = GenerateCleTicket(ticket);            

            cart.CreateTicket(ticket);

            return RedirectToAction("Ticket", new { ticketId = ticket.Id });
        }

        // GET: 
       
        /// </returns>
        [HttpGet]
        public IActionResult Ticket(int ticketId)
        {
            var ticket = _context.Tickets
                .Include(t => t.TicketDetails)
                .ThenInclude(td => td.Offre)
                .ThenInclude(o => o.Events)
                .Include(t => t.Client)
                .Include(c => c.OffreInclud)
                .FirstOrDefault(t => t.Id == ticketId);


            if (ticket == null)
            {
                return RedirectToAction("Index", "ShoppingCart");
            }

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode($@"Le icket de {ticket.Client.Name} {ticket.Client.Prenom} :
            Place: {ticket.OffreInclud?.Place},
            Offre : {ticket.OffreInclud?.Description},
            Prix: {ticket.Prix} €,
            Date: {ticket.DateTicket:dd/MM/yyyy HH:mm},
            Email : {ticket.Client.Email}
            Cle Client: {ticket.Client.CleCompte},
            Cle Ticket: {ticket.CleTicket},
            Ticket", QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeImage = qrCode.GetGraphic(20);
            ViewBag.QRCodeImage = qrCodeImage;

            return View(ticket);
        }
    }
}
