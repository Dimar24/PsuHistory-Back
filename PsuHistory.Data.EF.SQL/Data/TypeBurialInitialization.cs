using PsuHistory.Data.Domain.Models.Monuments;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PsuHistory.Data.EF.SQL.Data
{
    public static class TypeBurialInitialization
    {
        public static List<TypeBurial> GetTypeBurialInitialization(DateTime dateTimeInitialization)
        {
            var typeBurials = new List<TypeBurial>()
            {
                new TypeBurial() { Name = "Братская могила" },
                new TypeBurial() { Name = "Индивидуальная могила" },
                new TypeBurial() { Name = "Воинское кладбище" },
                new TypeBurial() { Name = "Жертвы войны" },
            };

            typeBurials.ForEach(d =>
            {
                d.Id = Guid.NewGuid();
                d.CreatedAt = dateTimeInitialization;
                d.UpdatedAt = dateTimeInitialization;
            });

            return typeBurials;
        }
    }
}
