using JMAR.SYSTEM.DOMAIN.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using JMAR.SYSTEM.DOMAIN.Utils;

namespace JMAR.SYSTEM.DOMAIN.Extensions
{
    public static class IQueryableExtensions
    {

        public static Utils.PagedResult<T> GetPaged<T>(this IQueryable<T> query, QueryParameter queryParameter, IFilter filter) where T : class
        {
            int page = queryParameter.pageNumber;
            int pageSize = queryParameter.pageSize;
            var result = new Utils.PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize
            };

            var skip = (page - 1) * pageSize;

            if (!string.IsNullOrEmpty(queryParameter.Sort))
            {
                string sort = queryParameter.Sort.StartsWith("-") ? queryParameter.Sort.Substring(1) : queryParameter.Sort;
                var type = typeof(T);
                var propertyInfo = type.GetProperty(sort);
                if (propertyInfo != null)
                {
                    if (queryParameter.Sort.StartsWith("-"))
                        query = query.OrderByDescending(x => propertyInfo.GetValue(x, null));
                    else
                        query = query.OrderBy(x => propertyInfo.GetValue(x, null));
                }
            }

            query = query.ApplyFilters<T>(filter);
            query = query.ApplyFields<T>(queryParameter.Fields);

            if (queryParameter.AllowPaging)
            {
                result.Results = query.Skip(skip).Take(pageSize).ToList();
                var nextPage = page + 1;
                var tempSkip = (nextPage - 1) * pageSize;
                result.NextPage = (query.Skip(tempSkip).Take(pageSize).ToList().Count > 0) ? true : false;
                result.PreviousPage = (skip == 0 && result.Results.Count <= pageSize) ? false : true;
            }
            else
            {
                result.Results = query.ToList();
            }

            return result;
        }

        public static IQueryable<T> ApplyFields<T>(this IQueryable<T> query, string fields = "") where T : class
        {
            if (fields != "")
            {
                var propsNames = typeof(T).GetProperties().Select(p => p.Name);
                var queryFields = fields.Split(",").ToList();

                var allowedFields = queryFields.Where(propsNames.Contains).ToArray();
                var allowefFieldsString = string.Join(",", allowedFields);

                query = query.Select(FieldsFilter.DynamicSelectGenerator<T>(allowefFieldsString)).AsQueryable();
            }

            return query;
        }

        public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, IFilter filter) where T : class
        {
            var reflectionFilterData = filter.GetType();
            var properties = reflectionFilterData.GetProperties();
            ExtractAttributes<T> extract = new ExtractAttributes<T>();



            foreach (var prop in properties)
            {
                var propValue = prop.GetValue(filter);
                if (propValue == null)
                {
                    continue;
                }

                var methodName = "FilterBy" + prop.Name;

                MethodInfo methodInfo = reflectionFilterData.GetMethod(methodName);
                if (methodInfo != null)
                {
                    object[] parameters = new object[] { query };
                    query = (System.Linq.IQueryable<T>)methodInfo.Invoke(filter, parameters);
                }
                else
                {

                    if (extract.ToString().Contains("CountryChannelSizeST"))
                    {
                        var ArrDts = properties[4].GetValue(filter);
                        if (ArrDts != null)
                        {
                            var defaultFilter = string.Format("{0} = \"{1}\"", properties[4].Name, properties[4].GetValue(filter));
                            query = query.Where(defaultFilter);

                            return query;
                        }
                    }

                    if (extract.ToString().Contains("SuccessPictureST"))
                    {
                        if (prop.Name == "ApplicationUserId") // StatusId
                        {
                            var defaultFilter = string.Format("{0} = \"{1}\"" + " || {2} = \"{3}\"", prop.Name, propValue, "StatusId", "3");
                            query = query.Where(defaultFilter);
                        }
                        else if (prop.Name == "InitialDate" || prop.Name == "Validity")
                        {
                            DateTime dteGba = Convert.ToDateTime(propValue);
                            string strMes = Convert.ToString(dteGba.Month);
                            string strdia = Convert.ToString(dteGba.Day);
                            char dto = Convert.ToChar("0");
                            strMes = strMes.PadLeft(2, dto);
                            strdia = strdia.PadLeft(2, dto);
                            if (prop.Name == "InitialDate")
                            {
                                var defaultFilter = string.Format("{0} >= \"{1}\"", prop.Name, dteGba.Year + "-" + strMes + "-" + strdia + " 00:00");
                                query = query.Where(defaultFilter);
                            }
                            else
                            {
                                var defaultFilter = string.Format("{0} <= \"{1}\"", prop.Name, dteGba.Year + "-" + strMes + "-" + strdia + " 00:00");
                                query = query.Where(defaultFilter);
                            }

                        }
                        else
                        {
                            if (prop.PropertyType.FullName.Contains("System.DateTime"))
                            {
                                DateTime dteGba = Convert.ToDateTime(propValue);
                                string strMes = Convert.ToString(dteGba.Month);
                                string strdia = Convert.ToString(dteGba.Day);
                                char dto = Convert.ToChar("0");
                                strMes = strMes.PadLeft(2, dto);
                                strdia = strdia.PadLeft(2, dto);
                                var defaultFilter = string.Format("{0} = \"{1}\"", prop.Name, dteGba.Year + "-" + strMes + "-" + strdia + " 00:00");
                                query = query.Where(defaultFilter);
                            }
                            else
                            {

                                if (properties[2].Name == prop.Name)
                                {
                                    if (properties[2].GetValue(filter) != null)
                                    {
                                        var defaultFilter1 = string.Format("{0} = \"{1}\"", prop.Name, propValue);
                                        query = query.Where(defaultFilter1);
                                    }
                                }
                                if (properties[3].Name == prop.Name)
                                {
                                    if (properties[3].GetValue(filter) != null)
                                    {
                                        var defaultFilter1 = string.Format("{0} = \"{1}\"", prop.Name, propValue);
                                        query = query.Where(defaultFilter1);
                                    }
                                }
                                if (properties[4].Name == prop.Name)
                                {
                                    if (properties[4].GetValue(filter) != null)
                                    {
                                        var defaultFilter1 = string.Format("{0} = \"{1}\"", prop.Name, propValue);
                                        query = query.Where(defaultFilter1);
                                    }
                                }
                                else
                                {
                                    if (prop.Name == "UserName")
                                    {

                                    }
                                    else
                                    {
                                        var defaultFilter1 = string.Format("{0} = \"{1}\"", prop.Name, propValue);
                                        query = query.Where(defaultFilter1);
                                    }

                                }
                            }

                        }

                    }
                    else
                    {
                        var defaultFilter = string.Format("{0} = \"{1}\"", prop.Name, propValue);
                        query = query.Where(defaultFilter);
                    }

                }
            }
            if (extract.ToString().Contains("SuccessPictureSTL") || extract.ToString().Contains("SuccessPictureST"))
            {
                var ArrDts = properties[11].GetValue(filter);
                if (ArrDts != null)
                {
                    var defaultFilter = string.Format("{0} = \"{1}\"" + " || {2} = \"{3}\"", properties[11].Name, properties[11].GetValue(filter), "StatusId", "3");
                    query = query.Where(defaultFilter);
                    return query;
                }
            }
            return query;
        }

    }
}
