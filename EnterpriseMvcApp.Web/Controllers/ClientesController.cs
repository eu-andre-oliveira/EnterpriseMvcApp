using EnterpriseMvcApp.Application.Services;
using EnterpriseMvcApp.Domain.Entidades;
using EnterpriseMvcApp.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseMvcApp.Web.Controllers;

public class ClientesController : Controller
{
    private readonly IClienteService _clienteService;

    public ClientesController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Clientes";
        var clientes = await _clienteService.ListarClientesAsync();
        var model = clientes.Select(MapearParaViewModelLista).ToList();
        return View("~/Views/Cliente/Index.cshtml", model);
    }

    public async Task<IActionResult> Details(int id)
    {
        ViewData["Title"] = "Detalhes do Cliente";

        try
        {
            var cliente = await _clienteService.ObterClienteCompletoAsync(id);
            return View("~/Views/Cliente/Details.cshtml", MapearParaViewModelDetalhe(cliente));
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewData["Title"] = "Novo Cliente";
        return View("~/Views/Cliente/Create.cshtml", new ClienteViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ClienteViewModel model)
    {
        ViewData["Title"] = "Novo Cliente";

        if (!ModelState.IsValid)
        {
            return View("~/Views/Cliente/Create.cshtml", model);
        }

        try
        {
            var cliente = MapearParaEntidade(model);
            await _clienteService.CriarClienteAsync(cliente);

            TempData["Sucesso"] = "Cliente cadastrado com sucesso.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Não foi possível cadastrar o cliente. Verifique os dados e tente novamente.");
            return View("~/Views/Cliente/Create.cshtml", model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        ViewData["Title"] = "Editar Cliente";

        try
        {
            var cliente = await _clienteService.ObterClienteCompletoAsync(id);
            return View("~/Views/Cliente/Edit.cshtml", MapearParaViewModelDetalhe(cliente));
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ClienteViewModel model)
    {
        ViewData["Title"] = "Editar Cliente";

        if (!ModelState.IsValid)
        {
            return View("~/Views/Cliente/Edit.cshtml", model);
        }

        try
        {
            var cliente = MapearParaEntidade(model);
            await _clienteService.AtualizarClienteAsync(cliente);

            TempData["Sucesso"] = "Cliente atualizado com sucesso.";
            return RedirectToAction(nameof(Index));
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Não foi possível atualizar o cliente. Verifique os dados e tente novamente.");
            return View("~/Views/Cliente/Edit.cshtml", model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        ViewData["Title"] = "Excluir Cliente";

        try
        {
            var cliente = await _clienteService.ObterClienteCompletoAsync(id);
            return View("~/Views/Cliente/Delete.cshtml", MapearParaViewModelDetalhe(cliente));
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _clienteService.RemoverClienteAsync(id);
            TempData["Sucesso"] = "Cliente removido com sucesso.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            TempData["Erro"] = "Não foi possível remover o cliente.";
            return RedirectToAction(nameof(Delete), new { id });
        }
    }

    private static ClienteViewModel MapearParaViewModelLista(Cliente cliente)
    {
        return new ClienteViewModel
        {
            Id = cliente.Id,
            Nome = cliente.Nome,
            CnpjCpf = cliente.CnpjCpf,
            Email = cliente.Email,
            Telefone = cliente.Telefone,
            Ativo = cliente.Ativo
        };
    }

    private static ClienteViewModel MapearParaViewModelDetalhe(Cliente cliente)
    {
        return new ClienteViewModel
        {
            Id = cliente.Id,
            Nome = cliente.Nome,
            CnpjCpf = cliente.CnpjCpf,
            Email = cliente.Email,
            Telefone = cliente.Telefone,
            Ativo = cliente.Ativo,
            Contatos = cliente.Contatos.Select(c => new ContatoClienteViewModel
            {
                Id = c.Id,
                Nome = c.Nome,
                Cargo = c.Cargo,
                Email = c.Email,
                Telefone = c.Telefone,
                Principal = c.Principal
            }).ToList()
        };
    }

    private static Cliente MapearParaEntidade(ClienteViewModel model)
    {
        return new Cliente
        {
            Id = model.Id,
            Nome = model.Nome.Trim(),
            CnpjCpf = model.CnpjCpf?.Trim(),
            Email = model.Email?.Trim(),
            Telefone = model.Telefone?.Trim(),
            Ativo = model.Ativo,
            Contatos = model.Contatos.Select(c => new ContatoCliente
            {
                Id = c.Id,
                Nome = c.Nome.Trim(),
                Cargo = c.Cargo?.Trim(),
                Email = c.Email?.Trim(),
                Telefone = c.Telefone?.Trim(),
                Principal = c.Principal
            }).ToList()
        };
    }
}
