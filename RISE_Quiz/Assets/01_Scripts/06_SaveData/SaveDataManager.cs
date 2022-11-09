using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    #region Singleton

    public static SaveDataManager Instance { get; private set; }

    private void InitializeSingleton()
    {
        Instance = this;
    }

    #endregion

    [SerializeField] private List<SaveData> saveData;

    private void Awake()
    {
        InitializeSingleton();
        saveData.ForEach(x => x.Init());
        LoadAll();
    }

    public void LoadAll()
    {
        saveData.ForEach(x => x.Load());
    }

    public void SaveAll()
    {
        saveData.ForEach(x => x.Save());
    }

    public void SaveData(SaveData sd)
    {
        sd.Save();
    }
}
