namespace EnterpriseMvcApp.Domain.Entidades;

public class ContatoCliente
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Cargo { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public bool Principal { get; set; }
    public DateTime DataCadastro { get; set; }

    public Cliente Cliente { get; set; } = null!;
}
