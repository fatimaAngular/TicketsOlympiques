using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

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
        public List<Cart>? ListePaniers { get; set; }

        public List<Offre>? CreatdOffres { get; set; }

        //public string? AccountKey { get; private set; }


        /// <summary>
        /// Génère une clé unique (AccountKey) pour l'utilisateur en utilisant une chaîne combinant
        /// l'Id de l'utilisateur, son prénom (FirstName), son nom (LastName) et son Email.
        /// Cette combinaison est ensuite hachée avec l'algorithme SHA256 pour assurer l'unicité.
        /// </summary>
        /// /// <remarks>
        /// La clé générée est une chaîne hexadécimale en minuscules, dérivée du hachage SHA256.
        /// </remarks>
        /// 
        //public void GenerateAccountKey()
        //{
        //    using var sha256 = SHA256.Create();
        //    var combinedValue = $"{Id}{Name}{Prenom}{Email}";
        //    var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(combinedValue));
        //    AccountKey = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        //}
    }
}
