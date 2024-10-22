using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketsJO.Data;
using TicketsJO.Models;
using Microsoft.AspNetCore.Authorization;
using TicketsJO.Data;
using TicketsJO.Models;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TicketsJO.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public readonly IMapper _mapper;
        public readonly UserManager<IdentityUser> _userManager;
        public TicketsController(ApplicationDbContext context, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: Tickets
        /// <summary>
        /// Accès à la page statistiques réservée à l'Administrateur
        /// </summary>
        /// <remarks>
        /// Cette méthode est uniquement accessible aux utilisateurs ayant le rôle "Admin". 
        /// Elle affiche les ventes effectuées sur l'application avec les détails de la transaction.         //
        /// </remarks>
        /// <returns>
        /// Retourne la vue avec la liste complète des tickets et les informations de statistiques (nombre total et montant des ventes).
        /// </returns>
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            bool isAdmin = await _userManager.IsInRoleAsync(currentUser, "Admin");
           
            List<Ticket> tickets = new List<Ticket>();
          
                if (isAdmin)
            {
                
                    tickets = await _context.Tickets
               .Include(t => t.Client)
               .Include(t => t.TicketDetails)
               .ThenInclude(td => td.Offre)                
               .ToListAsync();
               
            }
            else
            {                
                    tickets = await _context.Tickets
                    .Where(e => e.Client.Id == currentUser.Id)
                   .Include(t => t.Client)
                   .Include(t => t.TicketDetails)
                   .ThenInclude(td => td.Offre)                   
                   .ToListAsync();               
            }

            ViewBag.TotalVentes = tickets.Count();
            ViewBag.MontantTotal = tickets.Sum(t => t.Prix );

            return View(tickets);
        }

        // GET: Tickets/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Prix,DateTicket,CleTicket,,QrCode")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Prix,DateTicket,CleTicket,,QrCode")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }

        
    }
}
