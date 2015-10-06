
namespace NZBDash.Core.Interfaces
{
    public interface ISettings<T> where T : class
    {
        T GetSettings();
        bool SaveSettings(T model);
    }
}
