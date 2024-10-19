using System.ComponentModel.DataAnnotations;

namespace TicketsJO.Models
{
    /// <summary>
    /// Création du panier
    /// Le visiteur n'a pas besoin d'être connecté pour ajouter des offres dans son panier
    /// </summary>
    public class Cart
    {
        [Key]
        public int RecordId { get; set; }
        public int Quantity { get; set; }
        public double Prix { get; set; }
        public System.DateTime DateCreated { get; set; }
        public virtual Offre? Offre { get; set; }
        public User? Client { get; set; }
        public string? CartId { get; internal set; }
        public int OffreID { get; internal set; }
    }
}
