using UnityEngine;
using TMPro;

public class PuzzlePiece3D : MonoBehaviour
{
    [Header("Puzzle Data")]
    public int correctIndex;
    public int currentIndex;

    [HideInInspector] public PuzzleBoard3D board;

    [Header("Label")]
    public TextMeshPro label;

    [Header("Optional")]
    public Renderer pieceRenderer;
    public Color normalColor = new Color(0.4f, 0.85f, 0.95f);
    public Color lockedColor = new Color(0.55f, 1f, 0.55f);

    public bool IsCorrect
    {
        get { return currentIndex == correctIndex; }
    }

    private void Start()
    {
        UpdateLabel();
        UpdateVisual();
    }

    public void MoveToPosition(Vector3 targetPosition)
    {
        transform.position = targetPosition;
        UpdateVisual();
    }

    public void UpdateLabel()
    {
        if (label != null)
        {
            label.text = correctIndex.ToString();
        }
    }

    public void UpdateVisual()
    {
        if (pieceRenderer != null)
        {
            pieceRenderer.material.color = IsCorrect ? lockedColor : normalColor;
        }
    }
}