namespace TicketsJO.Models
{
    public class TicketDetail
    {
        public int Id { get; set; }
        public int IdTicket { get; set; }
        public int IdOffre { get; set; }
        public int Nombre { get; set; }
        public double PrixUnitaire { get; set; }
        public virtual Offre? Offre { get; set; }
        public virtual Ticket? Ticket { get; set; }
    }
}
