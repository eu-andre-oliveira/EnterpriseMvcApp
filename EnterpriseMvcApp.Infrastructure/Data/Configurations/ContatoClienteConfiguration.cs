using EnterpriseMvcApp.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseMvcApp.Infrastructure.Data.Configurations;

public class ContatoClienteConfiguration : IEntityTypeConfiguration<ContatoCliente>
{
    public void Configure(EntityTypeBuilder<ContatoCliente> builder)
    {
        builder.ToTable("ContatosCliente");

        builder.HasKey(cc => cc.Id);

        builder.Property(cc => cc.Nome)
            .IsRequired()
            .HasColumnType("varchar(150)");

        builder.Property(cc => cc.Cargo)
            .HasColumnType("varchar(100)");

        builder.Property(cc => cc.Email)
            .HasColumnType("varchar(150)");

        builder.Property(cc => cc.Telefone)
            .HasColumnType("varchar(20)");

        builder.Property(cc => cc.Principal)
            .IsRequired();

        builder.Property(cc => cc.DataCadastro)
            .IsRequired();

        builder.HasOne(cc => cc.Cliente)
            .WithMany(c => c.Contatos)
            .HasForeignKey(cc => cc.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
