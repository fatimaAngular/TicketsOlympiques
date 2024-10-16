namespace TicketsJO.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? DateEvent { get; set; }
        public int? Capacite{ get; set; }
        public Discipline Discipline { get; set; }
        public User? Creator { get; set; }
        public string AdresseEvent { get; set; }
        public StatutEvent StatutEvent { get; set; }     
        public List<Ticket> Tickets { get; set; }

    }
}
