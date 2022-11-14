using System.Collections.Generic;
using UnityEngine;

public class PositionByQuizState : MonoBehaviour
{
    [SerializeField] private RectTransform objectTransform;
    [SerializeField] private List<StatePosition> statePositions;

    protected void Position(QuizState state)
    {
        var parent = statePositions.Find(x => x.States.Contains(state)).Transform;
        objectTransform.SetParent(parent);
        objectTransform.anchoredPosition = Vector2.zero;
    }

    private void Awake()
    {
        Position(GameManager.Instance.State);
        GameManager.Instance.OnQuizStateChanged += UpdatePosition;
    }

    private void UpdatePosition(QuizState state)
    {
        Position(state);
    }
}

[System.Serializable]
public struct StatePosition
{
    [SerializeField] private Transform transform;
    [SerializeField] private List<QuizState> associatedStates;

    public Transform Transform => transform;
    public List<QuizState> States => associatedStates;
}
