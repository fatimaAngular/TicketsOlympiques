using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authorization;
using TicketsJO.Data;
using TicketsJO.Models;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace TicketsJO.Controllers
{
    public class OffresController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public OffresController(ApplicationDbContext context, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }
        // GET: Offres
        /// <summary>
        /// Récupère et affiche la liste des offres avec les événements associés.
        /// </summary>
        /// <returns>Charge les offres et les événements reliés depuis la base de données et les affiche dans une vue.</returns>
        public async Task<IActionResult> Index()
        {
            var Offres = await _context.Offres
           .Include(o => o.Events)
           .ToListAsync();
            return View(Offres);
        }

        // GET: Offres/Details/5
        /// <summary>
        /// Affiche les détails d'une offre spécifique.
        /// </summary>
        /// <remarks>
        /// Récupère une offre par son ID, en incluant les événements associés.
        /// Redirige vers une page "NotFound" si l'offre est introuvable.
        /// </remarks>
        /// <param name="id">Identifiant de l'offre.</param>
        /// <returns>Vue des détails de l'offre ou erreur si non trouvée.</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offre = await _context.Offres
                .Include(o => o.Events)
                .FirstOrDefaultAsync(m => m.OffreID == id);
            if (offre == null)
            {
                return NotFound();
            }

            return View(offre);
        }

        // GET: offres/Create
        /// <summary>
        /// Affiche le formulaire pour créer une nouvelle offre.
        /// </summary>
        /// <remarks>
        /// Génère une liste déroulante avec les événements disponibles pour lier à l'offre.
        /// L'utilisateur doit avoir le rôle d'Admin pour accéder à cette action.
        /// </remarks>
        /// <returns>Vue du formulaire de création.</returns>
        [Authorize(Roles = "Organizer,Admin")]
        public IActionResult Create()
        {
            var events = _context.Events
                .Select(e => new {
                    Id = e.Id,
                    DisplayText = $"{e.Name} - le {e.DateEvent:dd/MM/yyyy} - à {e.AdresseEvent}"
                })
                .ToList();

            ViewData["EventId"] = new SelectList(events, "Id", "DisplayText");

            return View();
        }

        // POST: offres/Create
        /// <summary>
        /// Crée une nouvelle offre dans la base de données.
        /// </summary>
        /// <remarks>
        /// Valide les données du formulaire avant d'ajouter l'offre.
        /// Redirige vers la liste des offres après la création, ou renvoie le formulaire en cas d'erreur.
        /// Nécessite un rôle Admin pour cette action.
        /// Référence de .NET, consultez : http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </remarks>
        /// <param name="offre">Les détails de l'offre à créer.</param>
        /// <returns>Redirection vers la liste des offres ou réaffichage du formulaire en cas d'erreur.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Organizer,Admin")]
        public async Task<IActionResult> Create([Bind("OffreID,Titre,Description,Place,Prix,EventId")] Offre offre)
        {
            if (ModelState.IsValid)
            {

                offre.Createur = _context.Set<User>()
                   .FirstOrDefault(o => o.Email.Equals(User.FindFirstValue(ClaimTypes.Email)));

                _context.Add(offre);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var events = _context.Events
                .Select(e => new {
                    Id = e.Id,
                    DisplayText = $"{e.Name} - le {e.DateEvent:dd/MM/yyyy} - à {e.AdresseEvent}"
                })
                .ToList();

            ViewData["EventId"] = new SelectList(events, "Id", "DisplayText", offre.EventId);
            return View(offre);
        }

        // GET: offres/Edit/5
        /// <summary>
        /// Affiche le formulaire pour modifier une offre.
        /// </summary>
        /// <remarks>
        /// L'utilisateur doit être authentifié avec le rôle admin pour cette action. 
        /// Charge l'offre existante par son ID. Si elle n'existe pas, une page "NotFound" est renvoyée.
        /// </remarks>
        /// <param name="id">Identifiant de l'offre à modifier.</param>
        /// <returns>Vue du formulaire d'édition ou erreur si l'offre n'est pas trouvée.</returns>
        /// 
        [Authorize(Roles = "Organizer,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offre = await _context.Offres.FindAsync(id);
            if (offre == null)
            {
                return NotFound();
            }
            return View(offre);
        }

        // POST: offres/Edit/5
        /// <summary>
        /// Met à jour une offre existante.
        /// </summary>
        /// <remarks>
        /// Valide et met à jour les informations de l'offre. En cas de conflit lors de la mise à jour,
        /// vérifie si l'offre existe encore. Nécessite un rôle Admin pour cette action.
        /// Plus de détails .NEt : http://go.microsoft.com/fwlink/?LinkId=317598.
        /// </remarks>
        /// <param name="id">Identifiant de l'offre à mettre à jour.</param>
        /// <param name="offre">Les détails mis à jour de l'offre.</param>
        /// <returns>Redirection vers la liste des offres ou réaffichage du formulaire en cas d'erreur.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Organizer,Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("OffreID,Titre,Description,Place,Prix,EventId")] Offre offre)
        {
            if (id != offre.OffreID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(offre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OffreExists(offre.OffreID))
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
            return View(offre);
        }

        // GET: offres/Delete/5
        /// <summary>
        /// Affiche le formulaire de suppression d'une offre.
        /// </summary>
        /// <remarks>
        /// Cette méthode nécessite que l'utilisateur soit authentifié avec le rôle "Admin".
        /// Récupère l'offre à supprimer par son ID. Si elle n'existe pas, redirige vers "NotFound".
        /// </remarks>
        /// <param name="id">Identifiant de l'offre à supprimer.</param>
        /// <returns>Vue du formulaire de suppression ou erreur si l'offre n'est pas trouvée.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offre = await _context.Offres
                .FirstOrDefaultAsync(m => m.OffreID == id);
            if (offre == null)
            {
                return NotFound();
            }

            return View(offre);
        }

        // POST: offres/Delete/5
        /// <summary>
        /// Confirme la suppression d'une offre.
        /// </summary>
        /// <remarks>
        /// Cette méthode est protégée contre les attaques CSRF (Cross-Site Request Forgery)/
        /// L'utilisateur doit authentifié avec le rôle "Admin" pour cette action. Supprime l'offre 
        /// sélectionnée et la supprime de la base de données. Redirige vers la liste des offres après 
        /// suppression.
        /// </remarks>
        /// <param name="id">Identifiant de l'offre à supprimer.</param>
        /// <returns>Redirection vers la liste des offres après suppression.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Organizer,Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var offre = await _context.Offres.FindAsync(id);
            if (offre != null)
            {
                _context.Offres.Remove(offre);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OffreExists(int id)
        {
            return _context.Offres.Any(e => e.OffreID == id);
        }
    }
}
