namespace TicketsJO.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? DateEvent { get; set; }
        public int? Capacite{ get; set; }
        public Category? Category { get; set; }
        public User? Creator { get; set; }
        public List<Ticket>? Tickets { get; set; }
        public string AdresseEvent { get; set; }
        public StatutTicket Statut { get; set; }
    }
}
