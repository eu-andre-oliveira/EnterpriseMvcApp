using EnterpriseMvcApp.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseMvcApp.Infrastructure.Data.Context;

public class AplicacaoDbContext : DbContext
{
    public AplicacaoDbContext(DbContextOptions<AplicacaoDbContext> options) : base(options)
    {
    }

    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<ContatoCliente> ContatosCliente => Set<ContatoCliente>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AplicacaoDbContext).Assembly);
    }
}
