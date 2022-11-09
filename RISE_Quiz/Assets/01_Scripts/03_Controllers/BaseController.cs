using TMPro;
using UnityEngine;

public abstract class BaseController : MonoBehaviour, IToggleable
{
    [SerializeField] protected GameObject toggleObject;
    [SerializeField] protected BaseView view;

    public virtual void Toggle(bool toggle)
    {
        toggleObject.SetActive(toggle);
    }
}
