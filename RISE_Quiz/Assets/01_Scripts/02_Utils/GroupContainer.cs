using UnityEngine;

public class GroupContainer : MonoBehaviour
{
    [SerializeField] private GroupConfig group;

    public void SetGroup()
    {
        GameManager.Instance.SetGroup(group);
    }
}
