using System.ComponentModel.DataAnnotations.Schema;

namespace TicketsJO.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string? NumSerie { get; set; }
        public double Prix { get; set; }
        public DateTime? DateTicket { get; set; }
        public string? CleTicket { get; set; }
        public string? QrCode { get; set; }
        public User? Client { get; set; }
        public Offre? OffreInclud { get; set; }
        public List<TicketDetail>? TicketDetails { get; set; }
    }
}
