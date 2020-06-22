using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JMAR.SYSTEM.DOMAIN.Controllers;
using JMAR.SYSTEM.DOMAIN.Manager;
using JMAR.SYSTEM.DOMAIN.Entities;
using JMAR.SYSTEM.DOMAIN.Filters;
using System.Threading;
using JMAR.SYSTEM.DOMAIN.Exceptions;
using JMAR.SYSTEM.DOMAIN.Utils;
using Microsoft.AspNetCore.Authorization;

namespace JMAR.SYSTEM.WEBAPI.Controllers
{
    [Route("products")]
    [ApiController]
    [Authorize]
    public class ProductController : BaseController<IProductManager, Products, ProductInputViewModel, ProductOutputViewModel, ProductFilter>
    {

        public ProductController(IProductManager Manager) : base(Manager)
        {

        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ProductOutputViewModel>))]
        [ProducesResponseType(500, Type = typeof(GenericError))]
        public override async Task<IActionResult> Get([FromQuery] QueryParameter pagingParameter, [FromQuery] ProductFilter entityFilter, CancellationToken ct = default(CancellationToken))
                => await base.Get(pagingParameter, entityFilter, ct);


        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ProductOutputViewModel))]
        [ProducesResponseType(500, Type = typeof(GenericError))]
        public override async Task<IActionResult> Post([FromBody] ProductInputViewModel input, CancellationToken ct = default(CancellationToken))
            => await base.Post(input, ct);

        [ApiExplorerSettings(IgnoreApi = true)]
        public override async Task<ProductOutputViewModel> PopulateEntity(ProductOutputViewModel entity, CancellationToken ct = default(CancellationToken))
                => await base.PopulateEntity(entity, ct);

        protected override ProductOutputViewModel ConverterOutput(ProductInputViewModel input)
        {
            ProductOutputViewModel Output = new ProductOutputViewModel();
            Output.IdProduct = input.IdProduct;
            Output.Nombre = input.Nombre;
            Output.Costo = input.Costo;
            Output.CantidadInventario = input.CantidadInventario;
            return Output;
        }
    }
}
