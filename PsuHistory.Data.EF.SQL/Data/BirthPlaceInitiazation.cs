using PsuHistory.Data.Domain.Models.Monuments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsuHistory.Data.EF.SQL.Data
{
    class BirthPlaceInitiazation
    {
        public static List<BirthPlace> GetTypeBirthPlaceInitialization(DateTime dateTimeInitialization)
        {
            var typeBirthPlace = new List<BirthPlace>()
            {
                new BirthPlace() { Place = "БССР, Витебская обл., Лиозненский р-н, д.Лососино." },
                new BirthPlace() { Place = "БССР, Витебская обл., Лиозненский р-н, Стасевский с/с, д. Пыжи." },
                new BirthPlace() { Place = "БССР, Витебская обл., Оршанский р-н, с.Салошико." },
                new BirthPlace() { Place = "БССР, Витебская обл., Освейский р-н, м. Освея." },
            };

            typeBirthPlace.ForEach(d =>
            {
                d.Id = Guid.NewGuid();
                d.CreatedAt = dateTimeInitialization;
                d.UpdatedAt = dateTimeInitialization;
            });

            return typeBirthPlace;
        }
    }
}
