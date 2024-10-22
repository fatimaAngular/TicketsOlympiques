using Microsoft.AspNetCore.Mvc;
using TicketsJO.Models;
using TicketsJO.ViewModels;


namespace TicketsJO.ViewComponents
{

    /// <summary>
    /// Le CartTotalViewComponent des informations sur le panier d'achat actuel, avec
    /// le nombre total d'articles et le prix total du panier.
    /// </summary>
    public class CartTotalViewComponent : ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;

        public CartTotalViewComponent(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        /// <summary>
        /// Invoque le composant pour calculer et afficher le résumé du panier.
        /// </summary>
        /// <remarks>
        /// Cette méthode récupère les articles du panier d'achat actuel et calcule
        /// le nombre total d'articles et le prix total des articles dans le panier.
        /// Le résultat est ensuite passé à la vue dans la navbar.
        /// </remarks>
        /// <returns>
        /// Un <see cref="IViewComponentResult"/> qui contient la vue affichant
        /// le résumé du panier, incluant le nombre total d'articles et le prix total.
        /// </returns>
        public IViewComponentResult Invoke()
        {
            var items = _shoppingCart.GetCartItems();

            var viewModel = new CartTotaleViewModel
            {
                CartNombre = items.Sum(x => x.Quantity),
                CartTotal = (decimal)items.Sum(x => x.Quantity * x.Offre.Prix)
            };

            return View(viewModel);
        }
    }
}