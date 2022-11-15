using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextWobbler : MonoBehaviour
{
    [Header("Wobble Settings")]
    [SerializeField] protected bool doWobble = true;
    [SerializeField] protected List<TMP_Text> texts;
    [SerializeField] protected float wobbleFactor = 5f;

    protected Mesh mesh;
    protected Vector3[] vertices;

    public void SetFactor(float newFactor)
    {
        wobbleFactor = newFactor;
    
    }

    public void StopWobble()
    {
        doWobble = false;
    }

    public void StartWobble()
    {
        doWobble = true;
    }

    private void Update()
    {
        if(doWobble)
        {
            Wobble();
        }
    }

    #region Wobble

    protected Vector2 GetWobbleEffect(float time, float factor)
    {
        return new Vector2(Mathf.Sin(time * factor), Mathf.Cos(time * factor));
    }

    protected void Wobble()
    {
        foreach(var text in texts)
        {
            text.ForceMeshUpdate();
            mesh = text.mesh;
            vertices = mesh.vertices;

            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 offset = GetWobbleEffect((Time.time + i), wobbleFactor);
                vertices[i] = vertices[i] + offset;
            }

            mesh.vertices = vertices;
            text.canvasRenderer.SetMesh(mesh);
        }
    }

    #endregion

    
}
