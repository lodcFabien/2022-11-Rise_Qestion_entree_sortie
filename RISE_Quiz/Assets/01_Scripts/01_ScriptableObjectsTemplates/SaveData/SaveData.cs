using UnityEngine;

public abstract class SaveData : MonoBehaviour
{
    [SerializeField] protected string settingName;
    [SerializeField] protected SaveableComponent component;

    public void Init()
    {
        component.Init();
    }

    public void Save()
    {
        if (string.IsNullOrEmpty(settingName))
        {
            return;
        }

        DoSave();
    }

    public void Load()
    {
        if (string.IsNullOrEmpty(settingName))
        {
            return;
        }

        DoLoad();
    }

    protected abstract void DoSave();
    protected abstract void DoLoad();
}
