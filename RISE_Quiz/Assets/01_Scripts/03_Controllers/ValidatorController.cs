using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidatorController : BaseController
{
    protected ValidatorView View => view as ValidatorView;

    public void SetText(bool isCorrect, Language language)
    {
        string newText;

        bool isFrench = language == Language.French;

        if(isCorrect)
        {
            newText = isFrench ? "CORRECT ! " : "RIGHT!";
        }
        else
        {
            newText = isFrench ? "INCORRECT..." : "WRONG...";
        }

        Debug.Log($"Correct? {isCorrect} | Language: {language} | Populating view with {newText}");
        View.Populate(newText);
    }

    public override void Toggle(bool toggle)
    {
        View.SetVisible(toggle);
    }
}
