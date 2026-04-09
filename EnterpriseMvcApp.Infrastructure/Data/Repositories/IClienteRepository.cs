using EnterpriseMvcApp.Domain.Entidades;

namespace EnterpriseMvcApp.Infrastructure.Data.Repositories;

public interface IClienteRepository
{
    Task<Cliente> ObterPorIdAsync(int id);
    Task<IEnumerable<Cliente>> ObterTodosAsync();
    Task AdicionarAsync(Cliente cliente);
    Task AtualizarAsync(Cliente cliente);
    Task RemoverAsync(int id);
    Task SalvarAlteracoesAsync();
}
