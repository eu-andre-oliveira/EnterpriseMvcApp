namespace EnterpriseMvcApp.Domain.Entidades;

public class Cliente
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? CnpjCpf { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public bool Ativo { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataAtualizacao { get; set; }

    public ICollection<ContatoCliente> Contatos { get; set; } = new List<ContatoCliente>();
}
