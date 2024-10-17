using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketsJO.Data;
using TicketsJO.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TicketsJO.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace TicketsJO.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public EventsController(ApplicationDbContext context, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: Events
        [Authorize(Roles = "Organizer,Admin")]
        public async Task<IActionResult> Index()
        {
            List<Event> events;

            var currentUser = await _userManager.GetUserAsync(User);
            bool isOrganizer = await _userManager.IsInRoleAsync(currentUser, "Organizer");


            if (isOrganizer)
            {
                events = await _context.Events
                    .Include(e => e.Discipline)
                    .Where(e => e.Creator.Email.Equals(User.FindFirstValue(ClaimTypes.Email)))
                    .OrderBy(e => e.Discipline)
                    .ToListAsync();
            }
            else
            {

                events = await _context.Events
                        .Include(e => e.Discipline)
                        .OrderBy(e => e.Discipline)
                        .ToListAsync();
            }

            return _context.Events != null ?
                          View(_mapper.Map<List<EventViewModel>>(events)) :
                          Problem("Entity set 'ApplicationDbContext.Events'  is null.");
        }

        // GET: Events/Details/5
        [Authorize(Roles = "Organizer,Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var eventVm = _mapper.Map<EventViewModel>(await _context.Events
                 .Include(c => c.Discipline)
                 .FirstOrDefaultAsync(m => m.Id == id));

            if (@eventVm == null)
            {
                return NotFound();
            }

            return View(@eventVm);
        }

        // GET: Events/Create
        [Authorize(Roles = "Organizer,Admin")]
        public IActionResult Create()
        {
            var eventVm = new EventViewModel
            {
                DisciplineVm = _mapper.Map<List<DisciplineViewModel>>(_context.Disciplines.ToList()),
                StatutEventVm = _mapper.Map<List<StatutEventViewModel>>(_context.StatutEvents.ToList()),
                
            };
            return View(eventVm);
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "Organizer,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,DateEvent,Capacite,IdDiscipline,AdresseEvent,IDStatutEvent")] EventViewModel eventVm)
        {

            if (ModelState.IsValid)
            {
                var @event = _mapper.Map<Event>(@eventVm);

                @event.Discipline = _context.Disciplines
                    .FirstOrDefault(c => c.ID.Equals(eventVm.IdDiscipline));

                @event.StatutEvent = _context.StatutEvents
                    .FirstOrDefault(c => c.Id.Equals(eventVm.IDStatutEvent));

                @event.Creator = _context.Set<User>()
                    .FirstOrDefault(o => o.Email.Equals(User.FindFirstValue(ClaimTypes.Email)));

                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            @eventVm.DisciplineVm = _mapper.Map<List<DisciplineViewModel>>(_context.Disciplines.ToList());
            @eventVm.StatutEventVm = _mapper.Map<List<StatutEventViewModel>>(_context.StatutEvents.ToList());

            return View(_mapper.Map<EventViewModel>(@eventVm));
        }

        // GET: Events/Edit/5
        [Authorize(Roles = "Organizer,Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var eventVm = _mapper.Map<EventViewModel>(await _context.Events
                .Include(c => c.Discipline)
                .FirstOrDefaultAsync(m => m.Id == id));

            if (eventVm == null)
            {
                return NotFound();
            }

            eventVm.DisciplineVm = _mapper.Map<List<DisciplineViewModel>>(_context.Disciplines.ToList());
            eventVm.StatutEventVm = _mapper.Map<List<StatutEventViewModel>>(_context.StatutEvents.ToList());

            return View(eventVm);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "Organizer,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,DateEvent,Capacite,IdDiscipline,AdresseEvent,IDStatutEvent")] EventViewModel eventVm)
        {
            if (id != eventVm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var @event = _mapper.Map<Event>(@eventVm);

                    @event.Discipline = _context.Disciplines
                        .FirstOrDefault(c => c.ID.Equals(eventVm.IdDiscipline));

                    @event.StatutEvent = _context.StatutEvents
                   .FirstOrDefault(c => c.Id.Equals(eventVm.IDStatutEvent));

                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(eventVm.Id))
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

            eventVm.DisciplineVm = _mapper.Map<List<DisciplineViewModel>>(_context.Disciplines.ToList());
            eventVm.StatutEventVm = _mapper.Map<List<StatutEventViewModel>>(_context.StatutEvents.ToList());

            return View(_mapper.Map<EventViewModel>(eventVm));
        }

        // GET: Events/Delete/5
        [Authorize(Roles = "Organizer,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }


            var @event = await _context.Events
                .Include(c => c.Discipline)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (@event == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<EventViewModel>(@event));
        }

        // POST: Events/Delete/5
        [Authorize(Roles = "Organizer,Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Events == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Events'  is null.");
            }
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                _context.Events.Remove(@event);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return (_context.Events?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
