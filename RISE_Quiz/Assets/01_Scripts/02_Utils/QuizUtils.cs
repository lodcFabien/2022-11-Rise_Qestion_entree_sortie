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

            case 1: return new int[7] { 0, 6, 5, 4, 3, 2, 1 };

            case 2: return new int[7] { 1, 0, 6, 5, 4, 3, 2 };

            case 3: return new int[7] { 2, 1, 0, 6, 5, 4, 3 };

            case 4: return new int[7] { 3, 2, 1, 0, 6, 5, 4 };

            case 5: return new int[7] { 4, 3, 2, 1, 0, 6, 5 };

            case 6: return new int[7] { 5, 4, 3, 2, 1, 0, 6 };

            case 7: return new int[7] { 6, 5, 4, 3, 2, 1, 0 };
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

    public static int GetIDByTeamNumber(int teamNumber)
    {
        switch (teamNumber)
        {
            case 1:
            case 8:
            case 15:
            case 22:
                return 0;

            case 2:
            case 9:
            case 16:
            case 23:
                return 1;

            case 3:
            case 10:
            case 17:
            case 24:
                return 2;

            case 4:
            case 11:
            case 18:
            case 25:
                return 3;

            case 5:
            case 12:
            case 19:
            case 26:
                return 4;

            case 6:
            case 13:
            case 20:
            case 27:
                return 5;

            case 7:
            case 14:
            case 21:
            case 28:
                return 6;

            default: return -1;
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
    DisplayingHint
}
