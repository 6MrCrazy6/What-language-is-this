using System.Collections.Generic;

namespace Localization
{
    [System.Serializable]
    public class GameLocalizationData
    {
        public string pause;
        public string resume;
        public string exit;
        public string times_up;
        public string restart;
        public string score;
        public string best_score;
        public string finish_score;
        public string score_label;
        public List<string> languageFiles;
    }
}
