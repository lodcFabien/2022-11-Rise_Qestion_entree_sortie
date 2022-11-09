
public class MultipleChoiceAnswer : Answer
{
    public MultipleChoiceAnswer(int id, string text, bool isCorrect)
    {
        ID = id;
        Text = text;
        IsCorrect = isCorrect;
        IsChecked = false;
    }

    public bool IsChecked { get; protected set; }

    public override void ResetParameters()
    {
        //Debug.Log($"Resetting parameters on multiple choice answer #{Id}");
        IsChecked = false;
    }

    public void Toggle(bool newValue)
    {
        IsChecked = newValue;
        //Debug.Log("isChecked? " + IsChecked);
    }
}
