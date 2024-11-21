namespace EnergySystem.Services.Data.Settings
{
    using System.Collections.Generic;
    using System.Linq;

    using EnergySystem.Data.Common.Repositories;
    using EnergySystem.Data.Models;

    using Mapping;

    public class SettingsService : ISettingsService
    {
        private readonly IDeletableEntityRepository<Setting> settingsRepository;

        public SettingsService(IDeletableEntityRepository<Setting> settingsRepository)
        {
            this.settingsRepository = settingsRepository;
        }

        public int GetCount() => this.settingsRepository.AllAsNoTracking().Count();

        public IEnumerable<T> GetAll<T>() => this.settingsRepository.All().To<T>().ToList();
    }
}
