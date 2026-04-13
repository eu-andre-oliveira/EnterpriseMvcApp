using EnterpriseMvcApp.Domain.Entidades;
using EnterpriseMvcApp.Domain.Interfaces;
using EnterpriseMvcApp.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseMvcApp.Infrastructure.Data.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly AplicacaoDbContext _context;

    public ClienteRepository(AplicacaoDbContext context)
    {
        _context = context;
    }

    public async Task<Cliente> ObterPorIdAsync(int id)
    {
        var cliente = await _context.Clientes
            .Include(c => c.Contatos)
            .FirstOrDefaultAsync(c => c.Id == id);

        return cliente ?? throw new KeyNotFoundException($"Cliente com ID {id} não foi encontrado.");
    }

    public async Task<IEnumerable<Cliente>> ObterTodosAsync()
    {
        return await _context.Clientes
            .AsNoTracking()
            .Include(c => c.Contatos)
            .OrderBy(c => c.Nome)
            .ToListAsync();
    }

    public async Task AdicionarAsync(Cliente cliente)
    {
        await _context.Clientes.AddAsync(cliente);
    }

    public Task AtualizarAsync(Cliente cliente)
    {
        _context.Clientes.Update(cliente);
        return Task.CompletedTask;
    }

    public async Task RemoverAsync(int id)
    {
        var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
        if (cliente is null)
        {
            throw new KeyNotFoundException($"Cliente com ID {id} não foi encontrado.");
        }

        _context.Clientes.Remove(cliente);
    }

    public async Task SalvarAlteracoesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
