using UnityEngine;

public class PanelController : MonoBehaviour, IToggleable
{
    [SerializeField] protected GameObject panel;

    public void Toggle(bool toggle)
    {
        panel.SetActive(toggle);
    }
}
