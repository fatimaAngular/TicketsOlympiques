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

        [Display (Name="Nombre")]
        public int Quantity { get; set; }
        public double Prix { get; set; }
        [Display(Name = "Date création")]
        public System.DateTime DateCreated { get; set; }
        public virtual Offre? Offre { get; set; }
        public User? Client { get; set; }
        public string? CartId { get; internal set; }
        public int OffreID { get; internal set; }
    }
}
