using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class DataLoger : MonoBehaviour
    {
        private Dictionary<string, WordData> languageData = new Dictionary<string, WordData>();

        // Method to start loading data
        public void LoadData(string[] nameFiles)
        {
            StartCoroutine(LoadDataCoroutine(nameFiles));
        }

        private IEnumerator LoadDataCoroutine(string[] nameFiles)
        {
            foreach (string file in nameFiles)
            {
                TextAsset jsonFile = Resources.Load<TextAsset>($"WordsData/{file}");
                if (jsonFile != null)
                {
                    WordData data = JsonUtility.FromJson<WordData>(jsonFile.text);
                    if (data != null && !string.IsNullOrEmpty(data.Language) && data.Words != null)
                    {
                        if (!languageData.ContainsKey(data.Language))
                        {
                            languageData[data.Language] = data;
                        }
                    }
                    else
                    {

                        Debug.LogError($"Invalid data in file: {file}");
                    }
                }
                else
                {
                    // Handle file not found
                    Debug.LogError($"File not found: WordsData/{file}");
                }

                yield return null; // Wait for one frame between iterations
            }
        }

        public Dictionary<string, WordData> LanguageData
        {
            get { return languageData; }
        }
    }
}


