using PsuHistory.Data.Domain.Models.Monuments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsuHistory.Data.EF.SQL.Data
{
    public static class ConscriptionPlaceInitialization
    {
        public static List<ConscriptionPlace> GetTypeConscriptionPlaceInitialization(DateTime dateTimeInitialization)
        {
            var typeConscriptionPlace = new List<ConscriptionPlace>()
            {
                new ConscriptionPlace() { Place = "Призван из партизанского отряда" },
                new ConscriptionPlace() { Place = "Призван из партизанской бригады Алексеева" },
                new ConscriptionPlace() { Place = "Призван Призван Освейским РВК." },
                new ConscriptionPlace() { Place = "Призван Оршанским РВК." },
            };

            typeConscriptionPlace.ForEach(d =>
            {
                d.Id = Guid.NewGuid();
                d.CreatedAt = dateTimeInitialization;
                d.UpdatedAt = dateTimeInitialization;
            });

            return typeConscriptionPlace;
        }
    }
}
