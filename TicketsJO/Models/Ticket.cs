using System.ComponentModel.DataAnnotations.Schema;

namespace TicketsJO.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string? NumSerie { get; set; }
        public double Prix { get; set; }
        public DateTime? DateTicket { get; set; }       
        public Event Event { get; set; }
        public Offre Offre { get; set; }
        public TypeTicket TypeTicke { get; set; }
    }
}
