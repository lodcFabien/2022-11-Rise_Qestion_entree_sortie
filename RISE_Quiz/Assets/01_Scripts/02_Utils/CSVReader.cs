using System;
using System.IO;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    #region Singleton

    public static CSVReader Instance { get; private set; }

    private void InitializeSingleton()
    {
        Instance = this;
    }

    #endregion

    private void Awake()
    {
        InitializeSingleton();
    }

    public string[] ReadQuestionsFile(out bool success)
    {
        return ReadCSV("QuestionsFile", out success);
    }

    public string[] ReadConfigFile(out bool success)
    {
        return ReadCSV("ConfigFile", out success);
    }

    private string[] ReadCSV(string fileName, out bool success)
    {
        if(!File.Exists(GetFilePath(fileName)))
        {
            Debug.LogWarning($"There is no config file!");
            success = false;
            return null;
        }

        string[] csvData = File.ReadAllText(GetFilePath(fileName)).Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

        if(csvData == null || csvData.Length == 0)
        {
            Debug.LogWarning($"The config file is empty.");
            success = false;
            return null;
        }

        string[] csvRelevantData = new string[csvData.Length -1];

        for (int i = 0; i < csvRelevantData.Length; i++)
        {
            csvRelevantData[i] = csvData[i + 1];
        }

        success = true;
        return csvRelevantData;
    }

    private string GetFilePath(string fileName)
    {
        return Path.Combine(Application.streamingAssetsPath, fileName + ".csv");
    }
}
