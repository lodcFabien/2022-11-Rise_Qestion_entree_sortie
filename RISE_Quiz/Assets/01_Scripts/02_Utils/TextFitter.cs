using UnityEngine;
using TMPro;

public class TextFitter : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Vector2 padding;
    [SerializeField] private TMP_Text text;

    //private void OnValidate()
    //{
    //    if(text != null)
    //    {
    //        text.ForceMeshUpdate(true);
    //        Debug.Log(text.GetPreferredValues());
    //    }
    //}

    public void FitToText()
    {
        text.ForceMeshUpdate(true);

        Vector2 textSize = text.GetPreferredValues();
        Vector2 newSize = textSize + padding;

        if(rectTransform != null)
        {
            rectTransform.sizeDelta = newSize;
        }
    }

    public void FitHorizontallyToText()
    {
        text.ForceMeshUpdate(true);
        Vector2 textSize = text.GetPreferredValues();
        
        float newWidth = textSize.x + padding.x;

        if(rectTransform != null)
        {
            Vector2 newSize = rectTransform.sizeDelta;
            newSize.x = newWidth;

            rectTransform.sizeDelta = newSize;
        }
    }

    public void FitVerticallyToText()
    {
        text.ForceMeshUpdate(true);
        Vector2 textSize = text.GetPreferredValues();

        float newHeight = textSize.y + padding.y;

        if (rectTransform != null)
        {
            Vector2 newSize = rectTransform.sizeDelta;
            newSize.y = newHeight;

            rectTransform.sizeDelta = newSize;
        }
    }

    public void FitHorizontallyToText(float cap)
    {
        float newWidth = GetPreferredWidth(cap);

        if (rectTransform != null)
        {
            Vector2 newSize = rectTransform.sizeDelta;
            newSize.x = newWidth;

            rectTransform.sizeDelta = newSize;
        }
    }

    public void FitHorizontallyToText(float cap, out float newWidth)
    {
        newWidth = GetPreferredWidth(cap);

        if (rectTransform != null)
        {
            Vector2 newSize = rectTransform.sizeDelta;
            newSize.x = newWidth;

            rectTransform.sizeDelta = newSize;
        }
    }

    public void SetWidth(float newWidth)
    {
        if (rectTransform != null)
        {
            Vector2 newSize = rectTransform.sizeDelta;
            newSize.x = newWidth;

            rectTransform.sizeDelta = newSize;
        }
    }

    public float GetPreferredWidth(float cap)
    {
        text.ForceMeshUpdate(true);
        Vector2 textSize = text.GetPreferredValues();

        float newWidth = textSize.x + padding.x;

        if (newWidth < cap)
        {
            newWidth = cap;
        }

        return newWidth;
    }

    public void FitVerticallyToText(float cap)
    {
        text.ForceMeshUpdate(true);
        Vector2 textSize = text.GetPreferredValues();

        float newHeight = textSize.y + padding.y;

        if (newHeight < cap)
        {
            return;
        }

        if (rectTransform != null)
        {
            Vector2 newSize = rectTransform.sizeDelta;
            newSize.y = newHeight;

            rectTransform.sizeDelta = newSize;
        }
    }
}
