namespace TicketsJO.Models
{
    public class Commande
    {
        public int Id { get; set; }
        public DateTime? DateCommande { get; set; }
        public double TotaleCommande { get; set; }
        public Ticket Ticket { get; set; }
        public User User { get; set; }        
        public List<Ticket>? TotalTicket { get; set; }
        public string TokenPrivate { get; set; }
        public byte[] QrCode { get; set; }

    }
}
