using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    // Affiche la liste des utilisateurs avec leurs rôles
    public async Task<IActionResult> UserList()
    {
        var users = _userManager.Users.ToList();
        var userRolesViewModel = new List<UserRolesViewModel>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            userRolesViewModel.Add(new UserRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = roles.ToList()
            });
        }

        return View(userRolesViewModel);
    }

    // Gérer les rôles pour un utilisateur donné
    [HttpGet]
    public async Task<IActionResult> ManageRoles(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        var roles = _roleManager.Roles.Where(e => e.Name != "Admin").ToList();
        var userRoles = await _userManager.GetRolesAsync(user);

        var model = new ManageUserRolesViewModel
        {
            UserId = user.Id,
            UserName = user.UserName,
            Roles = roles.Select(role => new RoleViewModel
            {
                RoleName = role.Name,
                IsSelected = userRoles.Contains(role.Name)
            }).ToList()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> ManageRoles(ManageUserRolesViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user == null)
        {
            return NotFound();
        }

        // Récupère les rôles actuels de l'utilisateur
        var userRoles = await _userManager.GetRolesAsync(user);

        var selectedRoles = model.Roles.Where(x => x.IsSelected).Select(y => y.RoleName).ToList();

        // Retirer les rôles non sélectionnés
        var result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Échec lors de la suppression des rôles existants de l'utilisateur");
            return View(model);
        }

        // Ajouter les rôles sélectionnés
        result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Échec lors de l'ajout des rôles sélectionnés à l'utilisateur");
            return View(model);
        }

        return RedirectToAction("UserList");
    }
}
