namespace TicketsJO.Models
{
    public class Category
    {

        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<Event> Events { get; set; }
    }
}
