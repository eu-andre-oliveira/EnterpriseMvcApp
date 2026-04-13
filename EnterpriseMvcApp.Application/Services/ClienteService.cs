using EnterpriseMvcApp.Domain.Entidades;
using EnterpriseMvcApp.Domain.Interfaces;

namespace EnterpriseMvcApp.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<Cliente> CriarClienteAsync(Cliente cliente)
    {
        ValidarCliente(cliente);

        var clientesExistentes = await _clienteRepository.ObterTodosAsync();
        var cnpjCpfJaCadastrado = clientesExistentes.Any(c =>
            !string.IsNullOrWhiteSpace(c.CnpjCpf)
            && !string.IsNullOrWhiteSpace(cliente.CnpjCpf)
            && c.CnpjCpf.Trim() == cliente.CnpjCpf.Trim());

        if (cnpjCpfJaCadastrado)
        {
            throw new InvalidOperationException("Já existe cliente cadastrado com o mesmo CNPJ/CPF.");
        }

        cliente.DataCadastro = DateTime.UtcNow;
        cliente.DataAtualizacao = null;

        if (cliente.Contatos is not null)
        {
            foreach (var contato in cliente.Contatos)
            {
                ValidarContato(contato);
                contato.DataCadastro = DateTime.UtcNow;
            }
        }

        await _clienteRepository.AdicionarAsync(cliente);
        await _clienteRepository.SalvarAlteracoesAsync();

        return cliente;
    }

    public async Task AtualizarClienteAsync(Cliente cliente)
    {
        ValidarCliente(cliente);

        var atual = await _clienteRepository.ObterPorIdAsync(cliente.Id);

        var clientesExistentes = await _clienteRepository.ObterTodosAsync();
        var cnpjCpfDuplicado = clientesExistentes.Any(c =>
            c.Id != cliente.Id
            && !string.IsNullOrWhiteSpace(c.CnpjCpf)
            && !string.IsNullOrWhiteSpace(cliente.CnpjCpf)
            && c.CnpjCpf.Trim() == cliente.CnpjCpf.Trim());

        if (cnpjCpfDuplicado)
        {
            throw new InvalidOperationException("Já existe outro cliente cadastrado com o mesmo CNPJ/CPF.");
        }

        atual.Nome = cliente.Nome.Trim();
        atual.CnpjCpf = cliente.CnpjCpf?.Trim();
        atual.Email = cliente.Email?.Trim();
        atual.Telefone = cliente.Telefone?.Trim();
        atual.Ativo = cliente.Ativo;
        atual.OrigemCliente = cliente.OrigemCliente?.Trim();
        atual.DataAtualizacao = DateTime.UtcNow;

        atual.Contatos.Clear();
        if (cliente.Contatos is not null)
        {
            foreach (var contato in cliente.Contatos)
            {
                ValidarContato(contato);
                atual.Contatos.Add(new ContatoCliente
                {
                    Nome = contato.Nome.Trim(),
                    Cargo = contato.Cargo?.Trim(),
                    Email = contato.Email?.Trim(),
                    Telefone = contato.Telefone?.Trim(),
                    Principal = contato.Principal,
                    DataCadastro = contato.DataCadastro == default ? DateTime.UtcNow : contato.DataCadastro
                });
            }
        }

        await _clienteRepository.AtualizarAsync(atual);
        await _clienteRepository.SalvarAlteracoesAsync();
    }

    public async Task RemoverClienteAsync(int id)
    {
        await _clienteRepository.RemoverAsync(id);
        await _clienteRepository.SalvarAlteracoesAsync();
    }

    public Task<IEnumerable<Cliente>> ListarClientesAsync()
    {
        return _clienteRepository.ObterTodosAsync();
    }

    public Task<Cliente> ObterClienteCompletoAsync(int id)
    {
        return _clienteRepository.ObterPorIdAsync(id);
    }

    private static void ValidarCliente(Cliente cliente)
    {
        if (cliente is null)
        {
            throw new ArgumentNullException(nameof(cliente));
        }

        if (string.IsNullOrWhiteSpace(cliente.Nome))
        {
            throw new ArgumentException("O nome do cliente é obrigatório.", nameof(cliente));
        }

        if (cliente.Nome.Trim().Length > 150)
        {
            throw new ArgumentException("O nome do cliente deve ter no máximo 150 caracteres.", nameof(cliente));
        }

        if (!string.IsNullOrWhiteSpace(cliente.CnpjCpf) && cliente.CnpjCpf.Trim().Length > 20)
        {
            throw new ArgumentException("CNPJ/CPF deve ter no máximo 20 caracteres.", nameof(cliente));
        }

        if (!string.IsNullOrWhiteSpace(cliente.Email) && cliente.Email.Trim().Length > 150)
        {
            throw new ArgumentException("E-mail deve ter no máximo 150 caracteres.", nameof(cliente));
        }

        if (!string.IsNullOrWhiteSpace(cliente.Telefone) && cliente.Telefone.Trim().Length > 20)
        {
            throw new ArgumentException("Telefone deve ter no máximo 20 caracteres.", nameof(cliente));
        }
    }

    private static void ValidarContato(ContatoCliente contato)
    {
        if (string.IsNullOrWhiteSpace(contato.Nome))
        {
            throw new ArgumentException("O nome do contato é obrigatório.", nameof(contato));
        }

        if (contato.Nome.Trim().Length > 150)
        {
            throw new ArgumentException("O nome do contato deve ter no máximo 150 caracteres.", nameof(contato));
        }

        if (!string.IsNullOrWhiteSpace(contato.Cargo) && contato.Cargo.Trim().Length > 100)
        {
            throw new ArgumentException("Cargo deve ter no máximo 100 caracteres.", nameof(contato));
        }

        if (!string.IsNullOrWhiteSpace(contato.Email) && contato.Email.Trim().Length > 150)
        {
            throw new ArgumentException("E-mail do contato deve ter no máximo 150 caracteres.", nameof(contato));
        }

        if (!string.IsNullOrWhiteSpace(contato.Telefone) && contato.Telefone.Trim().Length > 20)
        {
            throw new ArgumentException("Telefone do contato deve ter no máximo 20 caracteres.", nameof(contato));
        }
    }
}
