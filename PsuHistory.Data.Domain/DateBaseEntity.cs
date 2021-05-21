using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PsuHistory.Data.Domain
{
    public class DateBaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? CreatedAt { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? UpdatedAt { get; set; }
    }
}
