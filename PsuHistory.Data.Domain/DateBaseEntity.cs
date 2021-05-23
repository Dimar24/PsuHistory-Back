using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PsuHistory.Data.Domain
{
    public class DateBaseEntity
    {
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
