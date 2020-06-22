using JMAR.SYSTEM.DOMAIN.Repositories;
using JMAR.SYSTEM.DOMAIN.Entities;

namespace JMAR.SYSTEM.DATA.Repositories
{
    public class UsuarioRepository : BaseRepository<Users>, IUsuariosRepository
    {

        private readonly JMAREXAMContext _Context;

        public UsuarioRepository(JMAREXAMContext _Contexts) : base(_Contexts)
        {
            _Context = _Contexts;
        }

    }
}
