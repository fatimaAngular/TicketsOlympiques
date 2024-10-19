using System.ComponentModel.DataAnnotations;

namespace TicketsJO.Models
{
    public class Offre
    {
        [Key]
        public int OffreID { get; set; }
        public string? Titre { get; set; }
        public string Description { get; set; }
        public string? Place { get; set; }
        public User? Createur { get; set; }
        public double Prix { get; set; }
        public int EventId { get; set; }
        public Event? Events { get; set; }
        public List<Cart>? Carts { get; set; }

    }
}
