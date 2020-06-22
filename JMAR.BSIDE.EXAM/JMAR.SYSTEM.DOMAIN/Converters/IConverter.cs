using System.Collections.Generic;

namespace JMAR.SYSTEM.DOMAIN.Converters
{
    public interface IConverter<T, U> where T : class where U : class
    {
        U Convert(T entity);
        List<U> ConvertList(IEnumerable<T> entity);
    }
}
