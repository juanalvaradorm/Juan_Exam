using System;

namespace JMAR.SYSTEM.DOMAIN.Utils
{
    public class Related : Attribute
    {

        public Related(string Valor)
        {
            Value = Valor;
        }

        public string Value { get; set; }

    }
}
