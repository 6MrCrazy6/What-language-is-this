using System.Collections.Generic;

namespace Localization
{
    public interface ILocalizationLoader<T>
    {
        List<LocalizationEntry<T>> LoadLocalization();
    }

    [System.Serializable]
    public class LocalizationEntry<T>
    {
        public string language;
        public T data;
    }
}
