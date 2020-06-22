using JMAR.SYSTEM.DOMAIN.Entities;
using JMAR.SYSTEM.DOMAIN.Converters;
using JMAR.SYSTEM.DOMAIN.Repositories;

namespace JMAR.SYSTEM.DOMAIN.Manager
{
    public class ProductManager : BaseManager<Products, ProductOutputViewModel>, IProductManager
    {

        public ProductManager(IProdcutRepository _Repository) : base(_Repository)
        {

        }

        protected override Products PrepareAddData(ProductOutputViewModel viewModel)
        {
            return new Products
            {
                IdProduct = viewModel.IdProduct,
                Nombre = viewModel.Nombre,
                Costo = viewModel.Costo,
                CantidadInventario = viewModel.CantidadInventario
            };

        }

        protected override Products PrepareUpdateData(Products entity, ProductOutputViewModel viewModel)
        {
            entity.IdProduct = viewModel.IdProduct;
            entity.Nombre = viewModel.Nombre;
            entity.Costo = viewModel.Costo;
            entity.CantidadInventario = viewModel.CantidadInventario;

            return entity;
        }

        protected override IConverter<Products, ProductOutputViewModel> GetConverter()
        {
            return new ProductConvert();
        }

    }
}
