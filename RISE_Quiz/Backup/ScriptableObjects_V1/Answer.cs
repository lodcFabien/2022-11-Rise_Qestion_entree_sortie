using UnityEngine;

public abstract class Answer : ScriptableObject
{
    [SerializeField] protected int id;
    [SerializeField][TextArea(3, 6)] protected string answerText;
    [SerializeField] protected GameObject answerSlotPrefab;

    public int Id => id;
    public string Text => answerText;
    public GameObject Prefab => answerSlotPrefab;

    public abstract bool IsCorrect();
    public abstract void ResetParameters();
    //public abstract bool CanBeValidated();
}
