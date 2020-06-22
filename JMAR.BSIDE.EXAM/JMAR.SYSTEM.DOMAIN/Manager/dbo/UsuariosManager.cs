using JMAR.SYSTEM.DOMAIN.Entities;
using JMAR.SYSTEM.DOMAIN.Converters;
using JMAR.SYSTEM.DOMAIN.Repositories;

namespace JMAR.SYSTEM.DOMAIN.Manager
{
    public class UsuariosManager : BaseManager<Users, UsuariosOutputViewModel>, IUsuariosManager
    {

        public UsuariosManager(IUsuariosRepository _Repository) : base(_Repository)
        {

        }

        protected override Users PrepareAddData(UsuariosOutputViewModel viewModel)
        {
            return new Users
            {
                Nombre = viewModel.Nombre,
                Apellido = viewModel.Apellido,
                Activo = 1
            };

        }

        protected override Users PrepareUpdateData(Users entity, UsuariosOutputViewModel viewModel)
        {
            entity.Nombre = viewModel.Nombre;
            entity.Apellido = viewModel.Apellido;
            entity.Activo = viewModel.Activo;

            return entity;
        }

        protected override IConverter<Users, UsuariosOutputViewModel> GetConverter()
        {
            return new UsuariosConverter();
        }

    }
}
