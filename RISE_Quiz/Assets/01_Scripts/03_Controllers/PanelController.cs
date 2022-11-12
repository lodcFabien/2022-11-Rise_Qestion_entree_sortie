using UnityEngine;

public class PanelController : MonoBehaviour, IToggleable
{
    [SerializeField] protected GameObject panel;

    public virtual void Toggle(bool toggle)
    {
        panel.SetActive(toggle);
    }
}
