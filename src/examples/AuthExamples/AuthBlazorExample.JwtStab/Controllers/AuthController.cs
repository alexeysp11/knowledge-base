using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using AuthBlazorExample.JwtStab.Models;

namespace AuthBlazorExample.JwtStab.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger)
    {
        _logger = logger;
    }

    [HttpPost("ValidateUser")]
    public IActionResult ValidateUser(UserCredentials request)
    {
        if (request == null)
            return BadRequest("Request is not specified");
        if (string.IsNullOrEmpty(request.Login))
            return BadRequest("Request does not include login");
        if (string.IsNullOrEmpty(request.Password))
            return BadRequest("Request does not include password");
        
        System.Console.WriteLine("POINT 1");

        // Stab for validation.
        var isValidated = true;
        if (!isValidated)
            return BadRequest("User is not validated");
        
        System.Console.WriteLine("POINT 2");

        // Sign in the user.
        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Name, request.Login));
        claims.Add(new Claim(ClaimTypes.Role, request.Role));
        var claimsIdentity = new ClaimsIdentity(claims);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        HttpContext.SignInAsync(claimsPrincipal);

        System.Console.WriteLine("POINT 3");
        
        return Ok();
    }
}