namespace EnergySystem.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Common.Models;

    using Enums;

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

        public string OwnerId { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public Grid Grid { get; set; }

        public virtual ICollection<Battery> Batteries { get; set; }

        public PoweringRegime PoweringRegime { get; set; } //Enum for powering regime (e.g., Grid, Battery, Mixed).

        public DateTime CreatedOn { get; set; }
    }
}
