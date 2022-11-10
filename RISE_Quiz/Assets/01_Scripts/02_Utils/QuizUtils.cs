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

            case 1: return new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            case 2: return new int[7] { 1, 2, 3, 4, 5, 6, 0 };

            case 3: return new int[7] { 2, 3, 4, 5, 6, 0, 1 };

            case 4: return new int[7] { 3, 4, 5, 6, 0, 1, 2 };

            case 5: return new int[7] { 4, 5, 6, 0, 1, 2, 3 };

            case 6: return new int[7] { 5, 6, 0, 1, 2, 3, 4 };

            case 7: return new int[7] { 6, 0, 1, 2, 3, 4, 5 };
        }
    }

    public static int GetLetterHintIndexByTerminalID(int terminalID)
    {
        switch (terminalID)
        {
            default:
                Debug.LogWarning($"No team order found with terminalID {terminalID}.");
                return -1;

            case 1: return 3;

            case 2: return 5;

            case 3: return 0;

            case 4: return 2;

            case 5: return 1;

            case 6: return 6;

            case 7: return 4;
        }
    }

}
public enum GroupLetter
{
    A,
    B,
    C,
    D
}
public enum QuizState
{
    Init,
    Setup,
    WaitingForStart,
    EntryQuestion,
    WaitingForExpertSpeech,
    ExitQuestion,
    Verifying,
    DisplayingHint,
    Ended
}
