using Newtonsoft.Json;

namespace JMAR.SYSTEM.DOMAIN.Utils
{
    public class QueryParameter
    {

        const int maxPageSize = 100;

        public int pageNumber { get; set; } = 1;

        [JsonIgnore]
        private int _pageSize { get; set; } = 10;

        public string Sort { get; set; } = "";
        public string Fields { get; set; } = "";
        public bool AllowPaging { get; set; } = true;

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int pageSize
        {

            get { return _pageSize; }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

    }
}
