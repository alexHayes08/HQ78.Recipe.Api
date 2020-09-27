using System;
using System.Collections.Generic;

namespace HQ78.Recipe.Api.Models
{
    public class PaginationResponse<T>
    {
        public PaginationResponse()
        {
            Items = Array.Empty<T>();
        }

        public int TotalCount { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
