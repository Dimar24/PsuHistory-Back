using System;

namespace PsuHistory.Data.Domain.Models.Monuments
{
    public class Victim : KeyGuidEntityBase
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public bool IsHeroSoviet { get; set; }
        public bool IsPartisan { get; set; }
        public string DateOfBirth { get; set; }
        public string DateOfDeath { get; set; }

        public Guid TypeVictimId { get; set; }
        public virtual TypeVictim TypeVictim { get; set; }
        public Guid DutyStationId { get; set; }
        public virtual DutyStation DutyStation { get; set; }
        public Guid BirthPlaceId { get; set; }
        public virtual BirthPlace BirthPlace { get; set; }
        public Guid ConscriptionPlaceId { get; set; }
        public virtual ConscriptionPlace ConscriptionPlace { get; set; }
        public Guid BurialId { get; set; }
        public virtual Burial Burial { get; set; }
    }
}
