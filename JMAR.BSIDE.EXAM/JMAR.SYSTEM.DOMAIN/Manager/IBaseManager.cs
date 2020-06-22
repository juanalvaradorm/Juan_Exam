using System;
using System.Collections.Generic;
using JMAR.SYSTEM.DOMAIN.Utils;
using System.Threading;
using System.Threading.Tasks;
using JMAR.SYSTEM.DOMAIN.Filters;

namespace JMAR.SYSTEM.DOMAIN.Manager
{
    public interface IBaseManager<T, U> where T : class where U : class
    {

        Task<T> AddAsync(U viewModel, CancellationToken ct = default(CancellationToken));
        Task<Tuple<List<U>, PagedResult<T>>> GetAllAsync(QueryParameter pagingParameter, IFilter filter, CancellationToken ct = default(CancellationToken));
        Task<U> GetByIdAsync(int? id, CancellationToken ct = default(CancellationToken));
        Task<bool> UpdateAsync(U ViewModel, int? Id, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<bool> UpdatePatchAsync(U viewModel, int id, CancellationToken ct = default(CancellationToken));
        List<string> getRelatedEntities();
        List<string> getRelatedFields(string entity);

    }
}
