using Microsoft.AspNetCore.Mvc;

namespace EnterpriseMvcApp.Web.Controllers;

public class ContatosController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Contatos";
        return View();
    }
}
