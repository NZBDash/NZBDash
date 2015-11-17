
using NZBDash.Core.Model.Settings;

namespace NZBDash.Core.Interfaces
{
    public interface ISettingsService<T> where T : BaseSettingsDto
    {
        T GetSettings();
        bool SaveSettings(T model);
    }
}
