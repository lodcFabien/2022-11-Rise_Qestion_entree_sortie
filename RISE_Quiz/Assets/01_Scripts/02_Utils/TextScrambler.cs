using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TextScrambler : MonoBehaviour
{
    [SerializeField] protected List<TMP_Text> texts;

    protected string referenceWord;
    protected string scrambledWord;
    protected char[] chars;

    private void Awake()
    {
        referenceWord = texts[0].text;
    }

    protected static int[] GenerateUniqueNumbers(int minValue, int maxValue)
    {
        if (minValue > maxValue)
        {
            (maxValue, minValue) = (minValue, maxValue);
        }

        int[] values = new int[maxValue - minValue + 1];

        for (int i = 0; i < values.Length; ++i)
            values[i] = minValue + i;

        System.Random random = new System.Random();

        for (int i = 0; i < values.Length; ++i)
        {
            int index = random.Next(values.Length);

            if (i == index)
                continue;

            int tmp = values[i];

            values[i] = values[index];
            values[index] = tmp;
        }

        return values;
    }

    public void Scramble()
    {
        var order = GenerateUniqueNumbers(0, referenceWord.Length - 1);
        scrambledWord = new string (order.Select(i => referenceWord[i]).ToArray());
        texts.ForEach(x => x.text = scrambledWord);
    }

    public void Unscramble()
    {
        texts.ForEach(x => x.text = referenceWord);
    }
}
