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
        public DbSet<Discipline>? Disciplines { get; set; }

        public DbSet<Commande>? Commandes { get; set; }
        public DbSet<ModeDePaiement>? ModeDePaiements { get; set; }
        public DbSet<Paiement>? Paiements { get; set; }
        public DbSet<Promotion>? Promotions { get; set; }
        public DbSet<StatutTicket>? StatutTickets { get; set; }
        public DbSet<TypeTicket>? TypeTickets { get; set; }
        public DbSet<Ticket>? Tickets { get; set; }
       
        public DbSet<StatutEvent> StatutEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            //----------------------Methode Fluente API : pour modifier la taille des champs -------------

            //---------------------Event -----------------------------------
            base.OnModelCreating(builder); // pour ne pas ecraser les données deja générés par la méthode      

          
            builder.Entity<User>()
              .HasMany(u => u.CreatdEvents)
              .WithOne(e => e.Creator);
      
            builder.Entity<Event>()
                .HasOne(e => e.Creator)
                .WithMany(u => u.CreatdEvents);

            //--------------------------------------------------------

            builder.Entity<User>()
             .HasMany(u => u.ListeCommandes)
             .WithOne(e => e.User);

            builder.Entity<Commande>()
                .HasOne(e => e.User)
                .WithMany(u => u.ListeCommandes);               


            builder.Entity<Event>()
                .Property(e => e.Name)
                .HasMaxLength(256)
                .IsRequired();


            builder.Entity<Event>()
                .Property(e => e.Description)
                .IsRequired();

            builder.Entity<Event>()
                .Property(e => e.Capacite)
                .IsRequired();

            builder.Entity<Event>()
               .Property(e => e.AdresseEvent)
               .HasMaxLength(256)
               .IsRequired();

            //---------------------------------Discipline ---------------------

            builder.Entity<Discipline>()
                .Property(c => c.Name)
                .HasMaxLength(256)
                .IsRequired();          

            builder.Entity<Discipline>()
                .Property(c => c.Description)
                .IsRequired();          


            //---------------------------------ModeDePaiement ---------------------

            builder.Entity<ModeDePaiement>()
                .Property(c => c.Name)
                .HasMaxLength(256)
                .IsRequired();

            //---------------------------------Offre ---------------------

            builder.Entity<Offre>()
                .Property(c => c.TypeOffre)
                .HasMaxLength(256)
                .IsRequired();

            builder.Entity<Offre>()
               .Property(c => c.Description)            
               .IsRequired();

            builder.Entity<Offre>()
              .Property(c => c.NBPersonnes)
              .IsRequired();

            builder.Entity<Offre>()
             .Property(c => c.Prix)
             .IsRequired();

            //-------------------------Paiement -----------------------           

            builder.Entity<Paiement>()
                .Property(c => c.DatePai)
                .IsRequired();

            builder.Entity<Paiement>()
               .Property(c => c.Montant)
               .IsRequired();

            //-------------------------StatutEvent -----------------------
            builder.Entity<StatutEvent>()
                .Property(c => c.Name)
                .IsRequired();
            //-------------------------StatutTicket -----------------------
            builder.Entity<StatutTicket>()
                .Property(c => c.Name)
                .IsRequired();

            //-------------------------Ticket -----------------------           



            builder.Entity<Event>()
            .HasMany(u => u.Tickets)
            .WithOne(e => e.Event);

            builder.Entity<Ticket>()
                .HasOne(e => e.Event)
                .WithMany(u => u.Tickets);


            builder.Entity<Ticket>()
                .Property(c => c.NumSerie)
                .IsRequired();

            builder.Entity<Ticket>()
              .Property(c => c.Prix)
              .IsRequired();

            builder.Entity<Ticket>()
              .Property(c => c.DateTicket)
              .IsRequired();

            //builder.Entity<Ticket>()
            //  .Property(c => c.Event)
            //  .IsRequired();      

            //-------------------------TypeTicket -----------------------
            builder.Entity<TypeTicket>()
                .Property(c => c.Name)
                .IsRequired();

            //------------------------------ User ----------------------------------

            builder.Entity<User>()
                .Property(u => u.Name)
                .HasMaxLength(256)
                .IsRequired();

            builder.Entity<User>()
               .Property(u => u.Prenom)
               .HasMaxLength(256)
               .IsRequired();
                     

        }
    }
}
