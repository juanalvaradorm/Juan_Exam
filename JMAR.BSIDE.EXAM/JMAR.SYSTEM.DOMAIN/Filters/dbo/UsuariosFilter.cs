using System.Linq;
using System.Linq.Dynamic.Core;

namespace JMAR.SYSTEM.DOMAIN.Filters
{
    public class UsuariosFilter : IFilter
    {

        public UsuariosFilter()
        {

        }


        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Password { get; set; }
        //public string Password { get; set; }

        public IQueryable FilterByNombre(IQueryable query)
        {
            return query.Where("Nombre.Contains(@0)", this.Nombre);
        }

        public IQueryable FilterByPassword(IQueryable query)
        {
            return query.Where("Password.Contains(@0)", this.Password);
        }

        public IQueryable FilterByApellido(IQueryable query)
        {
            return query.Where("Apellido.Contains(@0)", this.Apellido);
        }

    }
}
