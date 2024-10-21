
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TicketsJO.Controllers;

namespace TestsUnitaires
{
    [TestClass]
    public class UnitTestEvenement
    {
        [TestMethod]
        public void TestCreationEvenement()
        {
            var controller = new EventsController();
            var user = new ClaimsPrincipal(new ClaimsIdentity([new Claim(ClaimTypes.Role, "Admin")]));
            var context = new DefaultHttpContext { User = user };

            controller.ControllerContext.HttpContext = context;

            var reponse = controller.Create();
            Assert.IsNotNull(reponse);
            Assert.IsInstanceOfType(reponse, typeof(EventsController));
            Console.WriteLine("l'évenement a été crée avec Succés");



        }
    }
}