
using NZBDash.Core.Model.Settings;

namespace NZBDash.Core.Interfaces
{
    public interface ISettingsService<T>
    {
        T GetSettings();
        bool SaveSettings(T model);
    }
}
