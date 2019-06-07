using Architecture.Infra.CrossCutting.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Compras;

namespace Project.Infra.Data.Mappings
{
    public class CompraMapping : EntityTypeConfiguration<Compra>
    {
        public override void Map(EntityTypeBuilder<Compra> builder)
        {
            builder.Property(c => c.Codigo)
                .HasColumnType("varchar(7)")
                .IsRequired();

            builder.Property(c => c.Mercadoria)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(c => c.Quantidade)
                .IsRequired();

            builder.Property(c => c.ValorUnitario)
                .IsRequired();

            builder.Property(c => c.ValorTotalMercadoria)
                .IsRequired();

            builder.Property(c => c.FreteCompra)
                .IsRequired();

            builder.Property(c => c.DespesaNF)
                .IsRequired();

            builder.Property(c => c.TotalNF)
                .IsRequired();

            builder.Property(c => c.CustoUnitario)
                .IsRequired();

            builder.Property(c => c.Observacao)
                .HasColumnType("varchar(100)");

            builder.Ignore(c => c.ValidationResult);

            builder.Ignore(c => c.CascadeMode);

            builder.ToTable("Compras");

            builder
                .HasOne(c => c.Cliente)
                .WithMany(co => co.Compras)
                .HasForeignKey(c => c.ClienteId);
        }
    }
}
