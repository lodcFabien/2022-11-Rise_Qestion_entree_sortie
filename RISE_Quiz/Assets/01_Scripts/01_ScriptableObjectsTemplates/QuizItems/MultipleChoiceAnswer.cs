using UnityEngine;

[System.Serializable]
public class MultipleChoiceAnswer : Answer
{
    [Header("Is Answer Correct?")]
    [SerializeField] private bool isCorrect;

    [Header("Debug")]
    [SerializeField] private bool isChecked = false;

    public bool IsChecked => isChecked;

    public override bool IsCorrect()
    {
        return isCorrect;
    }

    public override void ResetParameters()
    {
        //Debug.Log($"Resetting parameters on multiple choice answer #{Id}");
        isChecked = false;
    }

    public void Toggle(bool newValue)
    {
        isChecked = newValue;
        //Debug.Log("isChecked? " + isChecked);
    }
}
