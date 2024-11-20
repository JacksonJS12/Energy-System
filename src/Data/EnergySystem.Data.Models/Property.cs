using System;
using System.Collections.Generic;
using EnergySystem.Data.Common.Models;
using EnergySystem.Data.Models.Enums;

namespace EnergySystem.Data.Models
{
    public class Property : BaseDeletableModel<string>
    {
        public Property()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Batteries = new HashSet<Battery>();
        }

        public string Name { get; set; }

        public string Address { get; set; }

        public float ElectricityNeed { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public Grid Grid { get; set; }

        public virtual ICollection<Battery> Batteries { get; set; }

        public PoweringRegime PoweringRegime { get; set; } //Enum for powering regime (e.g., Grid, Battery, Mixed).

        public DateTime CreatedOn { get; set; }
    }
}
