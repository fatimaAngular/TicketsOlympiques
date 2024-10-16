namespace TicketsJO.Models
{
    public class Paiement
    {
        public int Id { get; set; }
        public DateTime DatePai { get; set; }
        public double Montant { get; set; }
        public Commande Command { get; set; }
        public ModeDePaiement ModeDePaie { get; set; }
    }
}
