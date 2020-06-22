using JMAR.SYSTEM.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JMAR.SYSTEM.DATA.Configuration
{
    public class UsuariosConfiguration
    {

        public UsuariosConfiguration(EntityTypeBuilder<Users> _Entity)
        {
            _Entity.Property(u => u.IdUser).IsRequired();
            _Entity.Property(u => u.Nombre).IsRequired().HasMaxLength(200);
            _Entity.Property(u => u.Apellido).IsRequired().HasMaxLength(100);
            _Entity.Property(u => u.Password).IsRequired().HasMaxLength(200);
            _Entity.Property(u => u.Activo).IsRequired();
        }

    }
}
