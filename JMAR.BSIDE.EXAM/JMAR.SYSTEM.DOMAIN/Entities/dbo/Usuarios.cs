using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JMAR.SYSTEM.DOMAIN.Entities
{
    [Table("Users", Schema = "dbo")]
    public class Users
    {
        [Required()]
        public int IdUser { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Password { get; set; }
        public int Activo { get; set; }
    }

    public class UsuariosOutputViewModel
    {
        public int IdUser { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Password { get; set; }
        public int Activo { get; set; }
        [NotMapped]
        public string Token { get; set; }
    }
    public class UsuariosInputViewModel
    {
        public int IdUser { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Password { get; set; }
    }

    public class Login
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Password { get; set; }
    }

}
