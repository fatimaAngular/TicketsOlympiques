using System.ComponentModel.DataAnnotations;

namespace TicketsJO.Models
{

    public enum Lieu
    {
        [Display(Name = "Paris - Parc des Princes")]
        ParcDesPrinces,
        [Display(Name = "Paris - Stade Rolland Garros")]
        StadeRollandGarros,
        [Display(Name = "Saint-Denis - Athlétisme Stade de France")]
        AthetismeStadeDeFrance,
        [Display(Name = "Strasbourg - Gymnase Rhénus")]
        GymnaseRhénus,
        [Display(Name = "Angers - Centre Aquatique Olympique")]
        CentreAquatiqueOlympique,
    }
    public class Event
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? DateEvent { get; set; }
        public int? Capacite{ get; set; }
        public Discipline? Discipline { get; set; }
        public User? Creator { get; set; }
        public string? AdresseEvent { get; set; }
        public StatutEvent? StatutEvent { get; set; }    
        public ICollection<Offre>? Offres { get; set; }

    }
}
