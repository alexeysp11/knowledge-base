using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KnowledgeBase.Examples.ManageTranslationsAspNetCore.BaseTranslationMvc.ViewModels;

namespace KnowledgeBase.Examples.ManageTranslationsAspNetCore.BaseTranslationMvc.Controllers;

public class AuthController : Controller
{
    [AllowAnonymous]
    public IActionResult SignIn()
    {
        if (User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home", null);
        }
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn(SignInViewModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (model.Username == "test" && model.Password == "test")
                {
                    var claims = new List<Claim>
                    {
                        new Claim("UserAccountId", "1"),
                        new Claim(ClaimTypes.Name, model.Username),
                        new Claim(ClaimTypes.Role, "User"),
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(12),
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme, 
                        new ClaimsPrincipal(claimsIdentity), 
                        authProperties);

                    return RedirectToAction("Index", "Home", null);
                }
                else
                {
                    TempData["ErrorMessage"] = "Incorrect username or password";
                }
            }
        }
        catch (System.Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
        }
        return RedirectToAction("SignIn");
    }
    
    [Authorize]
    public async Task<ActionResult> SignOut()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}