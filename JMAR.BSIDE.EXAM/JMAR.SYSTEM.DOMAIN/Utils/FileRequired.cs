using System;

namespace JMAR.SYSTEM.DOMAIN.Utils
{
    public class FileRequired : Attribute
    {

        public FileRequired(string Valor, int LongMax = 0, int LongMin = 0, bool iSRequired = true)
        {
            Value = Valor;
            ISRequired = iSRequired;
            LongMaxima = LongMax;
            LongMinima = LongMin;
        }

        public string Value { get; set; }
        public bool ISRequired { get; set; }
        public int LongMaxima { get; set; }
        public int LongMinima { get; set; }

    }
}
