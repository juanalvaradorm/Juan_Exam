using System;
using Microsoft.AspNetCore.Mvc;
using JMAR.SYSTEM.DOMAIN.Manager;
using System.Threading.Tasks;
using JMAR.SYSTEM.DOMAIN.Utils;
using System.Threading;
using JMAR.SYSTEM.DOMAIN.Filters;
using Newtonsoft.Json;
using JMAR.SYSTEM.DOMAIN.Exceptions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace JMAR.SYSTEM.DOMAIN.Controllers
{
    public abstract class BaseController<T, U, V, W, X> : ControllerBase, IBaseController<T, U, V, W, X> where T : class where U : class where V : class where W : class where X : class
    {

        public readonly IBaseManager<U, W> _manager;


        public BaseController(IBaseManager<U, W> manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Gets a entities list
        /// </summary>
        /// <param name="pagingParameter"></param>
        /// <param name="entityFilter"></param>
        /// <param name="ct">CancellationToken</param>
        /// <returns>Returns status 200 or 500 if has any exception</returns>
        public virtual async Task<IActionResult> Get([FromQuery] QueryParameter pagingParameter, [FromQuery] X entityFilter, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                var results = await _manager.GetAllAsync(pagingParameter, (IFilter)entityFilter, ct);

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(results.Item2));

                return new ObjectResult(results.Item1);
            }
            catch (CodeException ex)
            {
                return StatusCode(500, new GenericError
                {
                    Code = ex.Code,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GenericError
                {
                    Code = "ServerError",
                    Message = ex.Message
                });
            }
        }


        /// <summary>
        /// Adds a new entity
        /// </summary>
        /// <param name="input">A ViewModel instance</param>
        /// <param name="ct">CancellationToken</param>
        /// <returns>Returns status 201 or status 500 if exception</returns>
        public virtual async Task<IActionResult> Post([FromBody] V input, CancellationToken ct = default(CancellationToken))
        {
            if (input == null) return BadRequest();
            try
            {
                ExtractAttributes<V> extract = new ExtractAttributes<V>();
                List<SingleError> LstReg = new List<SingleError>();
                if (extract.ErrorCatch(input, ref LstReg) == false)
                {
                    return StatusCode(400, new GenericError
                    {
                        Code = "400",
                        Message = "Error de entidad",
                        Errors = LstReg

                    });
                }
                W output = this.ConverterOutput(input);
                return StatusCode(201, await _manager.AddAsync(output, ct));
            }
            catch (CodeException ex)
            {
                return StatusCode(500, new GenericError
                {
                    Code = ex.Code,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new GenericError
                {
                    Code = "ServerError",
                    Message = ex.Message
                });
            }
        }

        public virtual async Task<W> PopulateEntity(W entity, CancellationToken ct = default(CancellationToken))
        {
            await Task.Delay(1);
            return entity;
        }

        abstract protected W ConverterOutput(V input);

    }
}
