using System.Collections.Generic;

namespace RoverController.Web.API.DataTable
{
    public abstract class SearchResponse<T> where T : SearchDetail
    {
        public int Draw { get; set; }

        public int RecordsTotal { get; set; }

        public int RecordsFiltered { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}