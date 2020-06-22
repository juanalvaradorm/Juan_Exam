using System.Linq;
using System.Linq.Dynamic.Core;

namespace JMAR.SYSTEM.DOMAIN.Filters
{
    public class ProductFilter : IFilter
    {

        public ProductFilter()
        {

        }

        //public int IdProduct { get; set; }
        public string Nombre { get; set; }
        public string Costo { get; set; }

        public IQueryable FilterByNombre(IQueryable query)
        {
            return query.Where("Nombre.Contains(@0)", this.Nombre);
        }

        public IQueryable FilterBySpanishCosto(IQueryable query)
        {
            return query.Where("Costo.Contains(@0)", this.Costo);
        }

    }
}
