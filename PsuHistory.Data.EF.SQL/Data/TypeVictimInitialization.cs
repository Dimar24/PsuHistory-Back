using PsuHistory.Data.Domain.Models.Monuments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsuHistory.Data.EF.SQL.Data
{
    class TypeVictimInitialization
    {
        public static List<TypeVictim> GetTypeVictimInitialization(DateTime dateTimeInitialization)
        {
            var typeVictim = new List<TypeVictim>()
            {
                new TypeVictim() { Name = "ветфельдшер" },
                new TypeVictim() { Name = "военный техник" },
                new TypeVictim() { Name = "вольно-наемный" },
                new TypeVictim() { Name = "гв.генерал-майор" },
            };

            typeVictim.ForEach(d =>
            {
                d.Id = Guid.NewGuid();
                d.CreatedAt = dateTimeInitialization;
                d.UpdatedAt = dateTimeInitialization;
            });

            return typeVictim;
        }
    }
}
