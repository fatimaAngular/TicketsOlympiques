using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketsJO.Models;

namespace TicketsJO.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Event>? Events { get; set; }
        public DbSet<Category>? Categories { get; set; }

        public DbSet<Commande>? Commandes { get; set; }
        public DbSet<ModeDePaiement>? ModeDePaiements { get; set; }
        public DbSet<Paiement>? Paiements { get; set; }
        public DbSet<Promotion>? Promotions { get; set; }
        public DbSet<StatutTicket>? StatutTickets { get; set; }
        public DbSet<Ticket>? Tickets { get; set; }
        public DbSet<TypeTicket>? TypeTickets { get; set; }
        public DbSet<User>? Users { get; set; }
    }
}
