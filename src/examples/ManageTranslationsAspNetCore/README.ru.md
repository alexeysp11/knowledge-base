# ManageTranslationsAspNetCore

[English](README.md) | [Русский](README.ru.md)

## Использование ViewBag и выполнение перевода в контроллере

Например, для пути `Home/Index` заполнение `ViewBag` внутри контроллера `HomeController` может быть выполнено следующим образом:

```C#
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly EmployeesMvcDbContext _context;

    public HomeController(ILogger<HomeController> logger, EmployeesMvcDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        // Get translations for the Layout page.
        string formName = "_Layout";
        string applicationUid = "d7b0eb60-190e-439d-89e1-c78594ac7f0c";
        Dictionary<string, string> translations = TranslationHelper.GetLanguageKvpByFormName(
            _context,
            LanguageType.Russian,
            formName,
            applicationUid);
        ViewBag.LayoutTranslations = translations;
        
        return View();
    }

    // Another methods.
}
```

Тогда в `cshtml` получить перевод внутри `_Layout` можно следующим образом:

```HTML
Dictionary<string, string> translations = ViewBag.LayoutTranslations as Dictionary<string, string>;

<header>
    <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
        <div class="container-fluid">
            <a class="navbar-brand" asp-area="" asp-controller="Home" asp-action="Index">@translations["Page.Home"]</a>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                    aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
```
