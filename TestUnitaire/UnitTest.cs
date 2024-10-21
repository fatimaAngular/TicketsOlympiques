using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using TicketsJO;
using TicketsJO.Controllers;
using TicketsJO.Data;

using TicketsJO.Models;

namespace TestUnitaire
{
    [TestClass]
    public class TesterCreatEvenement
    {
        [TestMethod]

        // -----------Test de creaion d'un évenement par l'Admin-----------------
        //public void TestCreatEvent()
        //{

        //    var controller = new EventsController();

        //    var user = new ClaimsPrincipal(new ClaimsIdentity(claims: new[] { new Claim(ClaimTypes.Role, "Admin") }));

        //    var context = new DefaultHttpContext { User = user };

        //    controller.ControllerContext.HttpContext = context;

        //    var reponse = controller.Create();
        //    Assert.IsNotNull(reponse);
        //    Assert.IsInstanceOfType(reponse, typeof(ViewResult));
        //    Console.WriteLine("l'évenement a été crée avec Succés");

        //    var veiwReponse = reponse as ViewResult;
        //    Assert.IsNotNull(veiwReponse);

        //}

        // -----------Test de creaion d'un évenement par l'Admin-----------------
        public void TestGenerationCleUtilisateur()
        {

       
        var user01 = new User { Email = "Test1@mail.com",Name="Test1", Prenom="Prenom1", Adresse = "Paris 75000" };
        var user02 = new User { Email = "Test2@mail.com", Name = "Test2", Prenom = "Prenom2", Adresse = "Pontoise 95300" };
       

            user01.GenerationCleCompte();
            user02.GenerationCleCompte();
       
                      
            Assert.AreNotEqual(user01.CleCompte, user02.CleCompte);
            Assert.IsNotNull(user01.CleCompte);
            Assert.IsNotNull(user02.CleCompte);      

            Console.WriteLine($"La clé du User01 est :{user01.CleCompte}");
            Console.WriteLine($"La clé du User01 est :{user01.CleCompte}");

        }

    }
}