using System.ComponentModel.DataAnnotations;
using TicketsJO.Models;

namespace TicketsJO.Models
{
    public class TicketDetail
    {
        [Key]
        public int DetailId { get; set; }
        public int TicketId { get; set; }
        public int OffreId { get; set; }
        public int Count { get; set; }
        public decimal PrixUnitaire { get; set; }
        public virtual Offre? Offre { get; set; }
        public virtual Ticket? Ticket { get; set; }

    }
}
