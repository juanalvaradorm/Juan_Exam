using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JMAR.SYSTEM.DOMAIN.Entities
{
    [Table("Products", Schema = "dbo")]
    public class Products
    {
        public int IdProduct { get; set; }
        public string Nombre { get; set; }
        public string Costo { get; set; }
        public string CantidadInventario { get; set; }
    }

    public class ProductOutputViewModel
    {
        public int IdProduct { get; set; }
        public string Nombre { get; set; }
        public string Costo { get; set; }
        public string CantidadInventario { get; set; }
    }

    public class ProductInputViewModel
    {
        public int IdProduct { get; set; }
        public string Nombre { get; set; }
        public string Costo { get; set; }
        public string CantidadInventario { get; set; }
    }

}
