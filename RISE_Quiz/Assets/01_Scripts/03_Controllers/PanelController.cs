using UnityEngine;

public class PanelController : MonoBehaviour, IToggleable
{
    [Header("Panel Object(s)")]
    [SerializeField] protected GameObject panel;

    public virtual void Toggle(bool toggle)
    {
        panel.SetActive(toggle);
    }
}
