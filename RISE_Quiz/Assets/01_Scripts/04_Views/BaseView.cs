using TMPro;
using UnityEngine;

public abstract class BaseView : MonoBehaviour
{
    [SerializeField] protected TMP_Text textField;

    public virtual void Populate(string text)
    {
        textField.text = text;
    }

}
