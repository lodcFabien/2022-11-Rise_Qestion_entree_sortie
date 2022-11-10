using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public static class UIUtils
{
    public static List<string> GetLanguageOptionsList(Language language)
    {
        Language[] languageValues = (Language[])Enum.GetValues(typeof(Language));
        var options = new List<string>();

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

            options.Add(l.ToUpper());
        }

        return options;
    }

    public static List<string> GetGroupOptionsList()
    {
        GroupLetter[] letterValues = (GroupLetter[])Enum.GetValues(typeof(GroupLetter));
        var options = new List<string>();

        foreach (var lv in letterValues)
        {
            string l = lv.ToString().ToUpper();
            options.Add(l);
        }

        return options;
    }

    public static List<string> GetTeamOptionsList(Group currentGroup)
    {
        var options = new List<string>();

        foreach(var team in currentGroup.Teams)
        {
            options.Add(team.Name.ToUpper());
        }

        return options;
    }

    public static List<string> GetTerminalIDOptionsList(int teamCount, Language language)
    {
        var options = new List<string>();

        for (int i = 0; i < teamCount; i++)
        {
            switch (language)
            {
                case Language.French:
                    options.Add($"BORNE #{i + 1}");
                    break;
                case Language.English:
                    options.Add($"TERMINAL #{i + 1}");
                    break;
            }
            
        }

        return options;
    }

}
