using System;

namespace PsuHistory.Data.Domain.Models.Users
{
    public class User : KeyGuidEntityBase
    {
        public string Mail { get; set; }
        public string Password { get; set; }

        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
