using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class ButtonController : BaseController
{
    [SerializeField] protected Button button;

    public virtual void ToggleInteractability(bool toggle)
    {
        button.interactable = toggle;
    }

}
