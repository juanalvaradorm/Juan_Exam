using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using JMAR.SYSTEM.DOMAIN.Utils;

namespace JMAR.SYSTEM.DOMAIN.Controllers
{
    public interface IBaseController<T, U, V, W, X> where T : class where U : class where V : class where W : class where X : class
    {

        Task<IActionResult> Get([FromQuery] QueryParameter pagingParameter, [FromQuery] X entityFilter, CancellationToken ct = default(CancellationToken));
        Task<IActionResult> Post([FromBody] V input, CancellationToken ct = default(CancellationToken));
        Task<W> PopulateEntity(W entity, CancellationToken ct = default(CancellationToken));

    }
}
