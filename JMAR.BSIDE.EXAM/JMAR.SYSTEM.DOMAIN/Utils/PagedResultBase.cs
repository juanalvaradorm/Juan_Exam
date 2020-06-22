using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace JMAR.SYSTEM.DOMAIN.Utils
{
    public abstract class PagedResultBase
    {

        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public int TotalPages { get; set; }
        public bool PreviousPage { get; set; }
        public bool NextPage { get; set; }

    }

    public class PagedResult<T> : PagedResultBase where T : class
    {
        [JsonIgnore]
        public IList<T> Results { get; set; }

        public PagedResult()
        {
            Results = new List<T>();
        }
    }

}
