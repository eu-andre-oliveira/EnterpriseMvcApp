using Microsoft.AspNetCore.Mvc;

namespace EnterpriseMvcApp.Web.Controllers;

public class ClientesController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Clientes";
        return View();
    }
}
