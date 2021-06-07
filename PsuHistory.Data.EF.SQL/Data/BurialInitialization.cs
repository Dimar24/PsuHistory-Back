using PsuHistory.Data.Domain.Models.Monuments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsuHistory.Data.EF.SQL.Data
{
    public static class BurialInitialization
    {
        public static List<Burial> GetBurialInitialization(List<TypeBurial> typeBurials, DateTime dateTimeInitialization)
        {
            var id = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id;

            var burials = new List<Burial>()
            {
                new Burial()
                {
                    Name = string.Empty,
                    NumberBurial = 2352,
                    Location = "",
                    KnownNumber = 0,
                    UnknownNumber = 0,
                    Year = 0,
                    Latitude = 0,
                    Longitude = 0,
                    Description = "",
                    TypeBurialId = typeBurials.FirstOrDefault(d => d.Name.Equals("Братская могила")).Id,
                },
            };

            burials.ForEach(d =>
            {
                d.Id = Guid.NewGuid();
                d.CreatedAt = dateTimeInitialization;
                d.UpdatedAt = dateTimeInitialization;
            });

            return burials;
        }
    }
}
