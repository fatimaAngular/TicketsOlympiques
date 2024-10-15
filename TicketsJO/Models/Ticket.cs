namespace TicketsJO.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string? NumSerie { get; set; }
        public decimal Prix { get; set; }
        public DateTime? DateTicket { get; set; }
        public int? IDCommand { get; set; }
        public int? IDUser { get; set; }
        public int IDType { get; set; }
    }
}
