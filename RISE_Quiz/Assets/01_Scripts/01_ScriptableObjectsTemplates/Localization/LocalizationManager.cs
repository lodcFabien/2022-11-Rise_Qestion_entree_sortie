using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    #region Singleton

    public static LocalizationManager Instance { get; private set; }

    private void InitializeSingleton()
    {
        Instance = this;
    }

    #endregion

    [SerializeField] private Language activeLanguage = Language.French;
    public Action<Language> OnLanguageChanged { get; set; }

    public Language ActiveLanguage => activeLanguage;


    #region Unity Messages

    private void Awake()
    {
        InitializeSingleton();
    }

    #endregion


    #region Language Methods

    public void SetLanguage(Language newLanguage)
    {
        if (activeLanguage == newLanguage) return;

        activeLanguage = newLanguage;
        if (OnLanguageChanged != null) OnLanguageChanged.Invoke(newLanguage);
    }

    #endregion
}
public enum Language
{
    French,
    English
}