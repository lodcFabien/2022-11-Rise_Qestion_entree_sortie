
public abstract class Answer
{
    public int ID { get; protected set; }
    public string Text { get; protected set; }
    public virtual bool IsCorrect { get; protected set; }
    public abstract void ResetParameters();
    //public abstract bool CanBeValidated();
}
