using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsuHistory.Business.DTO.Models.UpdateDataModels
{
    public class UpdateVictim : KeyGuidEntityBase
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public bool IsHeroSoviet { get; set; }
        public bool IsPartisan { get; set; }
        public string DateOfBirth { get; set; }
        public string DateOfDeath { get; set; }

        public Guid TypeVictimId { get; set; }
        public Guid DutyStationId { get; set; }
        public Guid BirthPlaceId { get; set; }
        public Guid ConscriptionPlaceId { get; set; }
        public Guid BurialId { get; set; }
    }
}
