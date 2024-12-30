using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Shared
{
    public class EntityPaginated<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int CountAllItems { get; set; }

        public int PageNumber { get; set; }

        public int Count { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)CountAllItems / Count);
    }
}
