using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Controllers
{
    public class LanguageController
    {
        private Dictionary<string, string> translations;
        private string currentLanguage;
        private readonly string resourcesFolder = "Resources";

        public LanguageController(string language)
        {
            SetLanguage(language);
        }

        public void SetLanguage(string language)
        {
            if (language != "en" && language != "fr")
            {
                throw new ArgumentException("Language not supported");
            }

            currentLanguage = language;
            LoadTranslations();
        }

        private void LoadTranslations()
        {
            string filePath = Path.Combine(resourcesFolder, $"lang.{currentLanguage}.json");
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Language file not found: {filePath}");
            }

            string json = File.ReadAllText(filePath);
            translations = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        }

        public string T(string key)
        {
            if (translations != null && translations.TryGetValue(key, out string value))
            {
                return value;
            }
            return key; // fallback: return the key itself
        }

        public string CurrentLanguage => currentLanguage;
    }
}
