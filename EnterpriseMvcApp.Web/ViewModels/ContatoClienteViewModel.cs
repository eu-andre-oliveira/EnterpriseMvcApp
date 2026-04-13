using System.ComponentModel.DataAnnotations;

namespace EnterpriseMvcApp.Web.ViewModels;

public class ContatoClienteViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome do contato é obrigatório.")]
    [StringLength(150, ErrorMessage = "O nome do contato deve ter no máximo 150 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "Cargo deve ter no máximo 100 caracteres.")]
    public string? Cargo { get; set; }

    [StringLength(150, ErrorMessage = "E-mail deve ter no máximo 150 caracteres.")]
    [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
    public string? Email { get; set; }

    [StringLength(20, ErrorMessage = "Telefone deve ter no máximo 20 caracteres.")]
    public string? Telefone { get; set; }

    public bool Principal { get; set; }
}
