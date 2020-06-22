using JMAR.SYSTEM.DOMAIN.Repositories;
using JMAR.SYSTEM.DOMAIN.Entities;

namespace JMAR.SYSTEM.DATA.Repositories
{
    public class ProdcutRepository : BaseRepository<Products>, IProdcutRepository
    {

        private readonly JMAREXAMContext _Context;

        public ProdcutRepository(JMAREXAMContext _Contexts) : base(_Contexts)
        {
            _Context = _Contexts;
        }

    }
}
