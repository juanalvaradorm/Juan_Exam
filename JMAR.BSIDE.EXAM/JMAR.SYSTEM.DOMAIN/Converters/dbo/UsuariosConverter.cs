using JMAR.SYSTEM.DOMAIN.Entities;
using System.Linq;
using System.Collections.Generic;

namespace JMAR.SYSTEM.DOMAIN.Converters
{
    public class UsuariosConverter : IConverter<Users, UsuariosOutputViewModel>
    {

        public UsuariosOutputViewModel Convert(Users _Usuarios)
        {
            var UsuariosViewModel = new UsuariosOutputViewModel
            {
                IdUser = _Usuarios.IdUser,
                Nombre = _Usuarios.Nombre,
                Apellido = _Usuarios.Apellido,
                Activo = _Usuarios.Activo
            };

            return UsuariosViewModel;

        }

        public List<UsuariosOutputViewModel> ConvertList(IEnumerable<Users> _Usuarios)
        {
            return _Usuarios.Select(_Users =>
            {
                var model = new UsuariosOutputViewModel
                {
                    IdUser = _Users.IdUser,
                    Nombre = _Users.Nombre,
                    Apellido = _Users.Apellido,
                    Activo = _Users.Activo
                };
                return model;
            }).ToList();
        }

    }
}
