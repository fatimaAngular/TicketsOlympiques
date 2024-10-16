using Microsoft.AspNetCore.Identity;

namespace TicketsJO.Models
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }
        public string? Prenom { get; set; }
        public string? Adresse { get; set; }
        public string? Telephone { get; set; }
        public DateTime DateNaissance { get; set; }
        public DateTime Inscription { get; set; }
        public List<Event>? CreatdEvents { get; set; }
        public List<Commande>? ListeCommandes { get; set; }
    }
}
