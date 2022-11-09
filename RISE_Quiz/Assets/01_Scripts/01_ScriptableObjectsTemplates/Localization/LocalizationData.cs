using UnityEngine;

[System.Serializable]
public struct LocalizationData
{
    [SerializeField][TextArea(1, 25)] private string english;
    [SerializeField][TextArea(1, 25)] private string french;

    public string GetTranslation(Language language)
    {
        switch (language)
        {
            default:
                Debug.LogWarning("Translation not found!");
                return "";

            case Language.English:
                return english;

            case Language.French:
                return french;
        }
    }
}