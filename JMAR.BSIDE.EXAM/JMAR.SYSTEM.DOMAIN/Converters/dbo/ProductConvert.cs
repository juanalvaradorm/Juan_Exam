using JMAR.SYSTEM.DOMAIN.Entities;
using System.Linq;
using System.Collections.Generic;

namespace JMAR.SYSTEM.DOMAIN.Converters
{
    public class ProductConvert : IConverter<Products, ProductOutputViewModel>
    {

        public ProductOutputViewModel Convert(Products _Product)
        {
            var ProductViewModel = new ProductOutputViewModel
            {
                IdProduct = _Product.IdProduct,
                Nombre = _Product.Nombre,
                Costo = _Product.Costo,
                CantidadInventario = _Product.CantidadInventario
            };

            return ProductViewModel;

        }

        public List<ProductOutputViewModel> ConvertList(IEnumerable<Products> _Product)
        {
            return _Product.Select(_Products =>
            {
                var model = new ProductOutputViewModel
                {
                    IdProduct = _Products.IdProduct,
                    Nombre = _Products.Nombre,
                    Costo = _Products.Costo,
                    CantidadInventario = _Products.CantidadInventario
                };
                return model;
            }).ToList();
        }


    }
}
