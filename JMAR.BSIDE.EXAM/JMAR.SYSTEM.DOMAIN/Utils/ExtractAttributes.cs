using JMAR.SYSTEM.DOMAIN.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMAR.SYSTEM.DOMAIN.Utils
{
    public class ExtractAttributes<T>
    {

        public bool ErrorCatch(T entity, ref List<SingleError> LstReg)
        {
            bool Result = true;
            var ObjPas = typeof(T).GetProperties();
            foreach (var item in ObjPas)
            {
                object[] atributes = item.GetCustomAttributes(typeof(FileRequired), false);
                if (atributes.Count() > 0)
                {
                    FileRequired AttrTmp = (FileRequired)atributes[0];
                    //var info = entity.GetType().GetProperties();
                    if (AttrTmp.ISRequired == true)
                    {
                        var infos = entity.GetType().GetProperty(AttrTmp.Value);
                        var Infos2 = entity.GetType().GetProperty(AttrTmp.Value).GetValue(entity, null);
                        if (infos.PropertyType.FullName == "System.String")
                        {
                            if (Infos2 == null)
                            {
                                LstReg.Add(new SingleError { Field = infos.Name, Message = "El campo " + infos.Name + " es obligatorio" });
                                Result = false;
                            }
                            else
                            {
                                if (Infos2.ToString().Length > AttrTmp.LongMaxima)
                                {
                                    LstReg.Add(new SingleError { Field = infos.Name, Message = "La longitud maxima del campo " + infos.Name + " es de " + AttrTmp.LongMaxima.ToString() });
                                    Result = false;
                                }
                                if (string.IsNullOrEmpty(Infos2.ToString()))
                                {
                                    LstReg.Add(new SingleError { Field = infos.Name, Message = "El campo " + infos.Name + " es obligatorio" });
                                    Result = false;
                                }
                                if (AttrTmp.LongMinima != 0)
                                {
                                    if (Infos2.ToString().Length < AttrTmp.LongMinima)
                                    {
                                        LstReg.Add(new SingleError { Field = infos.Name, Message = "La longitud mínima del campo " + infos.Name + " es de " + AttrTmp.LongMinima.ToString() });
                                        Result = false;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        var infos = entity.GetType().GetProperty(AttrTmp.Value);
                        var Infos2 = entity.GetType().GetProperty(AttrTmp.Value).GetValue(entity, null);
                        if (infos.PropertyType.FullName == "System.String")
                        {
                            if (Infos2 == null)
                            {

                            }
                            else
                            {
                                if (Infos2.ToString().Length > AttrTmp.LongMaxima)
                                {
                                    LstReg.Add(new SingleError { Field = infos.Name, Message = "La longitud maxima del campo " + infos.Name + " es de " + AttrTmp.LongMaxima.ToString() });
                                    Result = false;
                                }
                            }
                        }
                    }
                }
            }

            return Result;
        }

        public List<T> ExtAttUserName(ref ICollection<string> ArrAtt)
        {
            var ObjPas = typeof(T).GetProperties();
            List<T> Lst = new List<T>();
            List<string> LstRel = new List<string>();
            string Str_Entity = string.Empty;
            foreach (var item in ObjPas)
            {
                object[] atributes = item.GetCustomAttributes(typeof(Related), false);
                if (atributes.Count() > 0)
                {
                    Related Att = (Related)atributes[0];
                    LstRel.Add(Att.Value);
                }
            }
            ArrAtt = (ICollection<string>)LstRel;
            return Lst;
        }

        public List<T> ExtAttRelated(ref ICollection<string> ArrAtt)
        {
            var ObjPas = typeof(T).GetProperties();
            List<T> Lst = new List<T>();
            List<string> LstRel = new List<string>();
            string Str_Entity = string.Empty;
            foreach (var item in ObjPas)
            {
                //var ArrOb = ObjPas.ToArray();
                object[] atributes = item.GetCustomAttributes(typeof(Related), false);
                if (atributes.Count() > 0)
                {
                    Related Att = (Related)atributes[0];
                    LstRel.Add(Att.Value);
                }
            }
            ArrAtt = (ICollection<string>)LstRel;
            return Lst;
        }

    }
}
