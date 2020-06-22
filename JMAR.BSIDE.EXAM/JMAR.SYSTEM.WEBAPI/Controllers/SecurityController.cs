using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using JMAR.SYSTEM.DOMAIN.Utils;
using System.Threading.Tasks;
using JMAR.SYSTEM.DOMAIN.Entities;
using JMAR.SYSTEM.DOMAIN.Exceptions;
using JMAR.SYSTEM.DOMAIN.Filters;
using JMAR.SYSTEM.DOMAIN.Manager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace JMAR.SYSTEM.WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {

        private readonly IConfiguration _Configuration;
        private readonly IUsuariosManager _UsersManager;

        public SecurityController(IConfiguration Configuration, IUsuariosManager UsersManager)
        {
            _Configuration = Configuration;
            _UsersManager = UsersManager;
        }


        [HttpPost]
        //[Authorize]
        [Route("logins")]
        [ProducesResponseType(201, Type = typeof(UsuariosOutputViewModel))]
        [ProducesResponseType(500, Type = typeof(GenericError))]
        public async Task<IActionResult> GetLogin([FromBody] Login logins)
        {

            if (logins == null)
            {
                return StatusCode(400, new GenericError
                {
                    Code = "400",
                    Message = "Login incorrecto"

                });
            }

            if (logins.Nombre == null || logins.Apellido == null || logins.Password == null)
            {
                return StatusCode(400, new GenericError
                {
                    Code = "400",
                    Message = "Login incorrecto"

                });
            }

            UsuariosOutputViewModel ModelReturn = new UsuariosOutputViewModel();
            UsuariosFilter Filter = new UsuariosFilter() { Nombre = logins.Nombre, Apellido = logins.Apellido, Password = logins.Password };
            QueryParameter Parameter = new QueryParameter() { AllowPaging = false };
            System.Threading.CancellationToken Ct = new System.Threading.CancellationToken();
            ModelReturn = _UsersManager.GetAllAsync(Parameter, Filter, Ct).Result.Item1.FirstOrDefault();

            if (ModelReturn == null)
            {
                return StatusCode(400, new GenericError
                {
                    Code = "400",
                    Message = "Login incorrecto"

                });
            }


            if (ModelReturn.Nombre == null)
            {
                return StatusCode(400, new GenericError
                {
                    Code = "400",
                    Message = "Login incorrecto"

                });
            }


            var TokenH = new JwtSecurityTokenHandler();
            var Key = _Configuration["key"]; //var Key = Encoding.ASCII.GetBytes(_Configuration.GetValue<string>("AppSecret"));
            var key = Encoding.ASCII.GetBytes(Key);
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, ModelReturn.Nombre),
                    new Claim(ClaimTypes.Email, logins.Apellido),
                    new Claim("UserID", ModelReturn.IdUser.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            //ModelReturn.ExpiresIn = (60 * 60) * 24;
            var Token = TokenH.CreateToken(TokenDescriptor);
            ModelReturn.Token = TokenH.WriteToken(Token);
            return new ObjectResult(ModelReturn);
        }

    }
}
