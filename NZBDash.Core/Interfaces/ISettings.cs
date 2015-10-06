
using NZBDash.Core.Model.Settings;

namespace NZBDash.Core.Interfaces
{
    public interface ISettings<T> where T : BaseSettingsDto
    {
        T GetSettings();
        bool SaveSettings(T model);
    }
}
