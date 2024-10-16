namespace TicketsJO.Models
{
    public class Promotion
    {
        public int Id { get; set; }
        public double Prix { get; set; }
        public string CodePromo { get; set; }
        public double Reduction { get; set; }
        public string statut {  get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}
