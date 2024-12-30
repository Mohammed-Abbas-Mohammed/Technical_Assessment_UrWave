using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technical_Assessment_Overview.Shared
{
    public class BaseEntity
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [MaxLength(100)]
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
    }
}
