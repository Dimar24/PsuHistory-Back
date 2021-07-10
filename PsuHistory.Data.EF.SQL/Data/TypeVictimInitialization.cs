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
                new TypeVictim() { Name = "гв.ефрейтор" },
                new TypeVictim() { Name = "гв.капитан" },
                new TypeVictim() { Name = "гв.лейтенант" },
                new TypeVictim() { Name = "гв.майор" },
                new TypeVictim() { Name = "гв.мл.л-нт" },
                new TypeVictim() { Name = "гв.мл.с-нт" },
                new TypeVictim() { Name = "гв.подполковник" },
                new TypeVictim() { Name = "гв.полковник" },
                new TypeVictim() { Name = "гв.рядовой" },
                new TypeVictim() { Name = "гв.сержант" },
                new TypeVictim() { Name = "гв.ст.л-нт" },
                new TypeVictim() { Name = "гв.ст.с-нт" },
                new TypeVictim() { Name = "гв.старшина" },
                new TypeVictim() { Name = "генерал-майор" },
                new TypeVictim() { Name = "гражданский" },
                new TypeVictim() { Name = "ефрейтор" },
                new TypeVictim() { Name = "жертва войны" },
                new TypeVictim() { Name = "капитан" },
                new TypeVictim() { Name = "курсант" },
                new TypeVictim() { Name = "лейтенант" },
                new TypeVictim() { Name = "майор" },
                new TypeVictim() { Name = "медсестра" },
                new TypeVictim() { Name = "милиционер" },
                new TypeVictim() { Name = "мл.командир" },
                new TypeVictim() { Name = "мл.л-нт" },
                new TypeVictim() { Name = "мл.политрук" },
                new TypeVictim() { Name = "мл.сержант" },
                new TypeVictim() { Name = "неизвестно" },
                new TypeVictim() { Name = "подполковник" },
                new TypeVictim() { Name = "полковник" },
                new TypeVictim() { Name = "рядовой" },
                new TypeVictim() { Name = "рядовой / лейтенант" },
                new TypeVictim() { Name = "санинструктор" },
                new TypeVictim() { Name = "сержант" },
                new TypeVictim() { Name = "ст.лейтенант" },
                new TypeVictim() { Name = "ст.лейтенант / мл.сержант" },
                new TypeVictim() { Name = "ст.сержант" },
                new TypeVictim() { Name = "старшина" },
                new TypeVictim() { Name = "техник-интендант 1 ранга" },
                new TypeVictim() { Name = "участник сопротивления" },
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
