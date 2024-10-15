namespace TicketsJO.Models
{
    public class Paiement
    {
        public int Id { get; set; }
        public DateTime DatePai { get; set; }
        public decimal Montant { get; set; }
        public Commande IDCommand { get; set; }
        public ModeDePaiement ModeDePaiement { get; set; }
    }
}
