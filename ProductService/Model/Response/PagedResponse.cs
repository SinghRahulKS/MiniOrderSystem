using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductService.Model.Response
{
    public class PagedResponse<T>
    {
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public int TotalPages => (PageSize <= 0 || TotalCount == 0) ? 0 : ((TotalCount + PageSize - 1) / PageSize);
    }
}
