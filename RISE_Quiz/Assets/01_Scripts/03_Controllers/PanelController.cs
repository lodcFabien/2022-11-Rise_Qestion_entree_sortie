using UnityEngine;

public class PanelController : MonoBehaviour, IToggleable
{
    [SerializeField] private GameObject panel;

    public void Toggle(bool toggle)
    {
        panel.SetActive(toggle);
    }
}
