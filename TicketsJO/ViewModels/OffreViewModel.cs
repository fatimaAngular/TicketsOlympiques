using System.ComponentModel.DataAnnotations;
using TicketsJO.Models;

namespace TicketsJO.ViewModels
{
    public class OffreViewModel
    {
        public int OffreID { get; set; }
        public string? Titre { get; set; }
        public string Description { get; set; }
        public string? Place { get; set; }
        public User? Createur { get; set; }
        public double Prix { get; set; }
        public int EventId { get; set; }
        public int EventName { get; set; }
        public string? DescriptionEvent { get; set; }
        public DateTime? DateEvent { get; set; }
        public string? AdresseEvent { get; set; }
        public List<EventViewModel>? ListeEventsVM { get; set; }
        public List<Ticket>? ListeTickets { get; set; }
        public List<Cart>? Carts { get; set; }

    }
}
