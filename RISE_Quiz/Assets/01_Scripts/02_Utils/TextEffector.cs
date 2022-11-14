using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextEffector : MonoBehaviour
{
    [Header("Wobble Settings")]
    [SerializeField] protected TMP_Text text;
    [SerializeField] protected float wobbleFactor = 2.8f; 

    protected Mesh mesh;
    protected Vector3[] vertices;

    private void Update()
    {
        Wobble();
    }

    protected Vector2 GetWobbleEffect(float time, float factor)
    {
        return new Vector2(Mathf.Sin(time * factor), Mathf.Cos(time * factor));
    }

    protected void Wobble()
    {
        UpdateTextMesh();

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 offset = GetWobbleEffect((Time.time + i), wobbleFactor);
            vertices[i] = vertices[i] + offset;
        }

        mesh.vertices = vertices;
        text.canvasRenderer.SetMesh(mesh);
    }

    private void UpdateTextMesh()
    {
        text.ForceMeshUpdate();
        mesh = text.mesh;
        vertices = mesh.vertices;
    }

    public string ScrambleWord(string word)
    {
        char[] chars = new char[word.Length];
        System.Random rand = new System.Random(10000);
        int index = 0;
        while (word.Length > 0)
        { // Get a random number between 0 and the length of the word. 
            int next = rand.Next(0, word.Length - 1); // Take the character from the random position 
                                                      //and add to our char array. 
            chars[index] = word[next];                // Remove the character from the word. 
            word = word.Substring(0, next) + word.Substring(next + 1);
            ++index;
        }
        return new String(chars);
    }

    public void Scramble(string word)
    {
        char[] chars = new char[word.Length];
        int rand = UnityEngine.Random.Range(0, word.Length);
        int index = 0;


    }
}
