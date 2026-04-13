using EnterpriseMvcApp.Domain.Entidades;

namespace EnterpriseMvcApp.Application.Services;

public interface IClienteService
{
    Task<Cliente> CriarClienteAsync(Cliente cliente);
    Task AtualizarClienteAsync(Cliente cliente);
    Task RemoverClienteAsync(int id);
    Task<IEnumerable<Cliente>> ListarClientesAsync();
    Task<Cliente> ObterClienteCompletoAsync(int id);
}
