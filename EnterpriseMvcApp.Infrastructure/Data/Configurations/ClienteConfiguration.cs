using EnterpriseMvcApp.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseMvcApp.Infrastructure.Data.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nome)
            .IsRequired()
            .HasColumnType("varchar(150)");

        builder.Property(c => c.CnpjCpf)
            .HasColumnType("varchar(20)");

        builder.Property(c => c.Email)
            .HasColumnType("varchar(150)");

        builder.Property(c => c.Telefone)
            .HasColumnType("varchar(20)");

        builder.Property(c => c.Ativo)
            .IsRequired();
        builder.Property(c => c.OrigemCliente)
            .HasColumnType("varchar(255)");

        builder.Property(c => c.DataCadastro)
            .IsRequired();

        builder.Property(c => c.DataAtualizacao);

        builder.HasMany(c => c.Contatos)
            .WithOne(cc => cc.Cliente)
            .HasForeignKey(cc => cc.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
