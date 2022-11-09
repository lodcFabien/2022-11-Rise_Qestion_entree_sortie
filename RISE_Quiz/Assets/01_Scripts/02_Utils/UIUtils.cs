using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public static class UIUtils
{
    public static List<string> GetLanguageOptionsList(Language language)
    {
        Language[] languageValues = (Language[])Enum.GetValues(typeof(Language));
        var languages = new List<string>();

        foreach (var lv in languageValues)
        {
            string l = "";
            switch (lv)
            {
                case Language.English:
                    l = language == Language.English ? "English" : "Anglais";
                    break;

                case Language.French:
                    l = language == Language.English ? "French" : "Français";
                    break;
            }

            languages.Add(l.ToUpper());
        }

        return languages;
    }

    public static List<string> GetTerminalIDOptionsList(int teamCount, Language language)
    {
        var ids = new List<string>();

        for (int i = 0; i < teamCount; i++)
        {
            switch (language)
            {
                case Language.French:
                    ids.Add($"BORNE #{i + 1}");
                    break;
                case Language.English:
                    ids.Add($"TERMINAL #{i + 1}");
                    break;
            }
            
        }

        return ids;
    }

}
