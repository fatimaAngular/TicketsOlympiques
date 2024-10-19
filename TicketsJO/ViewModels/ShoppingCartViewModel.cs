using TicketsJO.Models;

namespace TicketsJO.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Cart>? CartItems { get; set; }
        public double CartTotal { get; set; }
    }
}
