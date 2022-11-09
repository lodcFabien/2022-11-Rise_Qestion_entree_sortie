using UnityEngine;
using TMPro;

public class TextFitter : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Vector2 padding;
    [SerializeField] private TMP_Text text;
    
    public void FitToText()
    {
        text.ForceMeshUpdate(true);

        Vector2 textSize = text.GetRenderedValues();
        Vector2 newSize = textSize + padding;

        if(rectTransform != null)
        {
            rectTransform.sizeDelta = newSize;
        }
    }
}
