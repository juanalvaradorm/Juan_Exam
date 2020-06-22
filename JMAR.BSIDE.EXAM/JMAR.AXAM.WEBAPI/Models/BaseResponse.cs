using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMAR.AXAM.WEBAPI.Models
{
    public class BaseResponse
    {

        public bool Sucess { get; set; }

        public string Redirect { get; set; }

        public List<ErrorDto> ErrorList { get; set; }

    }

    public class ErrorDto
    {
        /// <summary>
        /// Mensaje del error.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Lista de errores para usuario
        /// </summary>
        public IEnumerable<string> MessageList { get; set; }
        /// <summary>
        /// Codigo del error.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Tipo del error.
        /// </summary>
        public ushort Type { get; set; }

        /// <summary>
        /// indica si es bloqueante o es solo de advertencia; por default es bloqueante.
        /// </summary>
        public bool IsWarning { get; set; }
    }

}
