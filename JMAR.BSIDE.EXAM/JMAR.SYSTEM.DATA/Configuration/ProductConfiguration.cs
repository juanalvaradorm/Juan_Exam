using JMAR.SYSTEM.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JMAR.SYSTEM.DATA.Configuration
{
    public class ProductConfiguration
    {

        public ProductConfiguration(EntityTypeBuilder<Products> _Entity)
        {
            _Entity.Property(u => u.IdProduct).IsRequired().ValueGeneratedOnAdd();
            _Entity.Property(u => u.Nombre).IsRequired().HasMaxLength(200);
            _Entity.Property(u => u.Costo).HasMaxLength(100);
            _Entity.Property(u => u.CantidadInventario).HasMaxLength(200);
        }

    }
}
