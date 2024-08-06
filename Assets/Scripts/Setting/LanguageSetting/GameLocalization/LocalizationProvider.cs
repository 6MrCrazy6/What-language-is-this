using System.Collections.Generic;
using System.Linq;

namespace Localization
{
    public interface ILocalizationProvider<T>
    {
        T GetLocalizationData(string language);
    }

    public class LocalizationProvider<T> : ILocalizationProvider<T>
    {
        private readonly Dictionary<string, T> _localizations;

        public LocalizationProvider(ILocalizationLoader<T> loader)
        {
            _localizations = loader.LoadLocalization().ToDictionary(entry => entry.language, entry => entry.data);
        }

        public T GetLocalizationData(string language)
        {
            if (_localizations.TryGetValue(language, out var localizationData))
            {
                return localizationData;
            }
            else
            {
                return _localizations["en"];
            }
        }
    }
}
