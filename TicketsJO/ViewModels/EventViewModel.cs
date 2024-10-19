using TicketsJO.ViewModels;
using System.ComponentModel.DataAnnotations;
using TicketsJO.Models;

namespace TicketsJO.ViewModels
{
    public class EventViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Le nom est obligatoire")]
        [StringLength(100, ErrorMessage = "Le nom ne peut pas dépasser 100 caractères")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "La description est obligatoire")]
        [StringLength(500, ErrorMessage = "La description ne peut pas dépasser 500 caractères")]
        public string? Description { get; set; }       
        public DateTime? DateEvent { get; set; }
        public int? Capacite { get; set; } 
        public int IDStatutEvent { get; set; }
        public string StatutEventName { get; set; }
        public int IdDiscipline { get; set; }
        public string? DisciplineName { get; set; }

        public List<DisciplineViewModel>? DisciplineVm { get; set; }
        public List<StatutEventViewModel>? StatutEventVm { get; set; }

        public ICollection<Offre>? Offres { get; set; }
        public string? AdresseEvent { get; set; }
        

     

    }
}
