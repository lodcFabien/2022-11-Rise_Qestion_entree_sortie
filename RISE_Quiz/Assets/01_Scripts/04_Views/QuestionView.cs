using UnityEngine;

public class QuestionView : BaseView
{
    public void SetTextAlignment(bool center)
    {
        if (center)
        {
            textField.alignment = TMPro.TextAlignmentOptions.Center;
        }
        else textField.alignment = TMPro.TextAlignmentOptions.Justified;
    }
}
