using PsuHistory.Data.Domain.Models.Monuments;
using System;
using System.Collections.Generic;

namespace PsuHistory.Data.EF.SQL.Data
{
    public class BaseDataInitialization : IDisposable
    {
        public List<TypeBurial> TypeBurials { get; set; }
        public List<Burial> Burials { get; set; }

        private DateTime DateTimeInitialization;

        public BaseDataInitialization()
        {
            DateTimeInitialization = DateTime.Now;

            TypeBurials = TypeBurialInitialization.GetTypeBurialInitialization(DateTimeInitialization);
            Burials = BurialInitialization.GetBurialInitialization(TypeBurials, DateTimeInitialization);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
