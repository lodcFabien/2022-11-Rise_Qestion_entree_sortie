using UnityEngine;

public static class QuizUtils
{
    public static int[] GetTeamOrderFromTerminalID(int terminalID)
    {
        switch (terminalID)
        {
            default:
                Debug.LogWarning($"No team order found with terminalID {terminalID}.");
                return null;

            case 1: return new int[7] { 1, 2, 3, 4, 5, 6, 7 };

            case 2: return new int[7] { 2, 3, 4, 5, 6, 7, 1 };

            case 3: return new int[7] { 3, 4, 5, 6, 7, 1, 2 };

            case 4: return new int[7] { 4, 5, 6, 7, 1, 2, 3 };

            case 5: return new int[7] { 5, 6, 7, 1, 2, 3, 4 };

            case 6: return new int[7] { 6, 7, 1, 2, 3, 4, 5 };

            case 7: return new int[7] { 7, 1, 2, 3, 4, 5, 6 };
        }
    }

    public static int[] GetQuestionIDsFromTerminalID(int terminalID)
    {
        switch (terminalID)
        {
            default:
                Debug.LogWarning($"No question IDs found with terminalID {terminalID}.");
                return null;

            case 1: return new int[2] { 1, 2 };

            case 2: return new int[2] { 3, 4 };

            case 3: return new int[2] { 5, 6 };

            case 4: return new int[2] { 7, 8 };

            case 5: return new int[2] { 9, 10 };

            case 6: return new int[2] { 11, 12 };

            case 7: return new int[2] { 13, 14 };
        }
    }
}
public enum Group
{
    A,
    B,
    C,
    D
}
public enum QuizState
{
    NotStarted,
    EntryQuestion,
    ExpertSpeech,
    ExitQuestion,
    Ended
}

[System.Serializable]
public struct QuestionSet
{
    public MultipleChoiceQuestion EntryQuestion { get; private set; }
    public MultipleChoiceQuestion ExitQuestion { get; private set; }

    public QuestionSet(MultipleChoiceQuestion entryQuestion, MultipleChoiceQuestion exitQuestion)
    {
        EntryQuestion = entryQuestion;
        ExitQuestion = exitQuestion;
    }
}
