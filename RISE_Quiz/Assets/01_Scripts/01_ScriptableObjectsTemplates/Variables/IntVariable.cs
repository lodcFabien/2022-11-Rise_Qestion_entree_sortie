using System;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/Variables/Int")]
public class IntVariable : ScriptableObject
{
    [SerializeField] private int baseValue;
    [SerializeField] private int defaultValue;

    public Action<int> OnValueChanged { get; set; }
    
    public int Value
    {
        get { return baseValue; }
        set 
        { 
            if(baseValue == value) { return; }

            baseValue = value;
            OnValueChanged?.Invoke(baseValue);
        }
    }

    public void ResetToDefault()
    {
        Value = defaultValue;
    }
}
