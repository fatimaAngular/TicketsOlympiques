namespace TicketsJO.Models
{
    public class Commande
    {
        public int Id { get; set; }
        public DateTime? DateCommande { get; set; }
        public decimal TotaleCommande { get; set; }
        public Ticket Ticket { get; set; }
        public User User { get; set; }
        public Promotion Promo {  get; set; }
        public List<Ticket>? TotalTicket { get; set; }

    }
}
