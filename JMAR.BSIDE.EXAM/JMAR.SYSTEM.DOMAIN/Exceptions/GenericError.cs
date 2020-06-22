using System.Collections.Generic;

namespace JMAR.SYSTEM.DOMAIN.Exceptions
{
    public class GenericError
    {

        public string Code { get; set; }
        public string Message { get; set; }
        public ICollection<SingleError> Errors { get; set; } = new HashSet<SingleError>();

    }
}
