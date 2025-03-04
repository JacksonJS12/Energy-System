namespace EnergySystem.Services.Grid
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGridService
    {
        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
