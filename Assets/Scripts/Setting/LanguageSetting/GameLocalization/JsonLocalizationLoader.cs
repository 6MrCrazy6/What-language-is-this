using System.Collections.Generic;
using UnityEngine;

namespace Localization
{
    public class JsonLocalizationLoader : ILocalizationLoader<GameLocalizationData>
    {
        private readonly string _filePath;

        public JsonLocalizationLoader(string filePath)
        {
            _filePath = filePath;
        }

        public List<LocalizationEntry<GameLocalizationData>> LoadLocalization()
        {
            var jsonFile = Resources.Load<TextAsset>(_filePath);
            var wrapper = JsonUtility.FromJson<LocalizationWrapper>(jsonFile.text);
            return wrapper.localizations;
        }

        [System.Serializable]
        private class LocalizationWrapper
        {
            public List<LocalizationEntry<GameLocalizationData>> localizations;
        }
    }
}
