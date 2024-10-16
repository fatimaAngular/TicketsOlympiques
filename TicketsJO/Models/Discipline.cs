namespace TicketsJO.Models
{
    public class Discipline
    {

        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<Event> Events { get; set; }
    }
}
