using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tarefas.Models;

namespace Tarefas.Data
{
    public class UsuarioMap : IEntityTypeConfiguration<UsuarioModel>
    {
        public void Configure(EntityTypeBuilder<UsuarioModel> builder)
        {
            builder.HasKey(x=> x.Id);
            builder.Property(x=>x.Nome).IsRequired().HasMaxLength(255);
            builder.Property(x=>x.Email).IsRequired().HasMaxLength(150);
        }
    }
}
