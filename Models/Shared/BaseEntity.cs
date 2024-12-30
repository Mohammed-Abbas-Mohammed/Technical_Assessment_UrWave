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
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public EntityStatus Status { get; set; } = EntityStatus.Active;
    }

    public enum EntityStatus
    {
        Active,
        Inactive,
        Discontinued
    }
}
