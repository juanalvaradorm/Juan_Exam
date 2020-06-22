using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JMAR.AXAM.WEBAPI.Models;
using JMAR.SYSTEM.DOMAIN.Entities;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace JMAR.AXAM.WEBAPI.Controllers
{
    public class LoginController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        public IConfiguration _Configuration;

        public LoginController(ILogger<HomeController> logger, IConfiguration Configuration)
        {
            _logger = logger;
            _Configuration = Configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Login(Login model)
        {
            BaseResponse response = new BaseResponse();

            UsuariosOutputViewModel Usuario = new UsuariosOutputViewModel();
            CancellationToken ct = default(CancellationToken);
            var responseBody = String.Empty;
            try
            {
                

                using (var Client = new HttpClient())
                {

                    string UriAddres = _Configuration["URLServicio"];
                    Client.BaseAddress = new Uri(UriAddres);
                    //Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Ikp1YW4gTWFudWVsIEFsdmFyYWRvIFJvc2FzIiwiZW1haWwiOiJqdWFuYWx2YXJhZG9ybUBnbWFpbC5jb20iLCJVc2VySUQiOiIxIiwibmJmIjoxNTkxODE2NTg0LCJleHAiOjE1OTE5MDI5ODMsImlhdCI6MTU5MTgxNjU4NH0.zRDaNAd7hM9z15n8HX1kQkKx4FsZxbp6RD4jKlJCgDfodZcU80dwobOxqi5PO21Jg2eiASCAlZ89PjIsxhZ0qw");
                    HttpContent Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    Task<HttpResponseMessage> response1 = Client.PostAsync(new Uri(UriAddres + "api/Security/logins"), Content);
                    response1.Result.EnsureSuccessStatusCode();
                    responseBody = await response1.Result.Content.ReadAsStringAsync();
                    Usuario = JsonConvert.DeserializeObject<UsuariosOutputViewModel>(responseBody);

                }
                HttpContext.Session.SetString("Usuario", Usuario.Nombre + " " + Usuario.Apellido);
                HttpContext.Session.SetString("Token", Usuario.Token);
                response.Sucess = true;
                //Session["UsuarioId"] = userLogin.idusuario;
                response.Redirect = Url.Action("Index", "Home");

            }
            catch(Exception Ex)
            {

            }

            //var response = new Models.BaseResponse() { Sucess = true, ErrorList = new List<PpcProyect.Entities.ErrorDto>() };
            ////RoleManager

            //ProcessorAccount processor = new ProcessorAccount();

            //var login = processor.LoginUser(model.Email.Trim().ToLower(), model.Password.Trim().ToLower());
            //if (login.Failure)
            //{
            //    response.Sucess = false;
            //    response.ErrorList.AddRange(login.ErrorListService);
            //}
            //else
            //{
            //    Entities.Catalogs.Login userLogin = new Entities.Catalogs.Login();

            //    userLogin = await GetCompleteUserList(model.Email.Trim().ToLower(), model.Password.Trim().ToLower());


            //    if (userLogin.idusuario != null)
            //    {
            //        Session["UsuarioId"] = userLogin.idusuario;
            //        Session["EmpresaId"] = (userLogin.idempresa == 1) ? 0 : userLogin.idempresa;
            //        Session["PreRegistroId"] = userLogin.idprereg;
            //        Session["Correo"] = userLogin.email;
            //        Session["Usuario"] = userLogin.usuarioNombre;

            //        Session["ClientId"] = CheckClientId(userLogin.idusuario.ToString());
            //        Session["idprov"] = CheckProvedorId(userLogin.idusuario.ToString());
            //        if (Convert.ToInt16(Session["idprov"].ToString()) > 0)
            //        {
            //            Session["ProviderName"] = GetProveedores(Session["idprov"].ToString()).Result.nombrecomercial;
            //        }
            //    }
            //    else
            //    {

            //        if (login.Result.User.ToUpper() == "ADMIN")
            //        {
            //            Session["Correo"] = model.Email;
            //            Session["EmpresaId"] = 1;
            //            Session["PreRegistroId"] = 1;
            //        }


            //    }
            //    FormsAuthentication.SetAuthCookie(login.Result.User.ToUpper(), false);
            //    //FormsAuthenticationUser

            //    //FormsAuthentication
            //    Session["userLogin"] = login.Result;
            //    response.Redirect = Url.Action("Index", "Home");
            //}

            return Json(new
            {
                response = response
            });
        }


    }
}
