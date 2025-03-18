using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using SupplementsShop.Application.Services;
using SupplementsShop.Domain.Entities;
using SupplementsShop.ViewModels;

namespace SupplementsShop.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    
    private IHttpContextAccessor _httpContextAccessor;
    
    private IEmailSenderService _emailSenderService;
    private readonly ICartService _cartService;

    public AccountController(
        SignInManager<User> signInManager, 
        UserManager<User> userManager,
        IHttpContextAccessor httpContextAccessor,
        IEmailSenderService emailSenderService,
        ICartService cartService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _emailSenderService = emailSenderService;
        _cartService = cartService;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login()
    {
        string? returnUrl = HttpContext.Request.Query["returnUrl"];
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
    {
        if (!ModelState.IsValid) return View(model);
        
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }
        
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
        if (result.Succeeded)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return View(model);
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        
        var user = new User { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Action(
                action: "ConfirmEmail",
                "Account",
                new { userId = user.Id, token },
                protocol: Request.Scheme);
            
            //await _emailSenderService.SendEmailAsync(user.Email, "Confirm your email", $"Please confirm your account by clicking here: <a href='{callbackUrl}'>link</a>");
            
            return RedirectToAction("ThankYou", new { email = user.Email });
        }

        Console.WriteLine("User creation failed.");
        foreach (var error in result.Errors)
        {
            Console.WriteLine(error.Description);
            ModelState.AddModelError(string.Empty, error.Description);
        }
        
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        _cartService.ClearCartSession();
        HttpContext.Session.SetString("CartMerged", "false");
        return RedirectToAction("Index", "Home");
    }

    public IActionResult ThankYou(string email)
    {
        return View((object)email);
    }

    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        if (userId == null || token == null)
        {
            return RedirectToAction("Index", "Home");
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{userId}'.");
        }

        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (result.Succeeded)
        {
            return View((object)"Confirmed");
        }
        else
        {
            return View((object)"Error");
        }
    }
}