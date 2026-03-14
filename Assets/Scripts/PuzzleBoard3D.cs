using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PuzzleBoard3D : MonoBehaviour
{
    [Header("References")]
    public Camera mainCamera;
    public List<PuzzlePiece3D> pieces = new List<PuzzlePiece3D>();

    [Header("Grid Settings")]
    public int columns = 5;
    public int rows = 5;
    public float spacing = 1f;
    public float yLevel = 0f;

    [Header("Debug")]
    public bool logClicks = true;

    private PuzzlePiece3D firstSelected;
    private PuzzlePiece3D secondSelected;

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        ShufflePieces();

        foreach (PuzzlePiece3D piece in pieces)
        {
            if (piece == null) continue;

            piece.board = this;
            piece.MoveToPosition(GetWorldPosition(piece.currentIndex));
            piece.UpdateLabel();
            piece.UpdateVisual();
        }
    }

    private void Update()
    {
        if (Mouse.current == null || mainCamera == null)
            return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            TrySelectPiece();
        }
    }

    private void TrySelectPiece()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            PuzzlePiece3D clickedPiece = hit.collider.GetComponent<PuzzlePiece3D>();

            if (clickedPiece != null)
            {
                SelectPiece(clickedPiece);
            }
        }
    }

    public void SelectPiece(PuzzlePiece3D piece)
    {
        if (piece == null) return;

        // richtige Teile dürfen nicht mehr bewegt werden
        if (piece.IsCorrect)
        {
            if (logClicks)
            {
                Debug.Log(piece.name + " ist bereits richtig und gesperrt.");
            }
            return;
        }

        if (firstSelected == null)
        {
            firstSelected = piece;

            if (logClicks)
            {
                Debug.Log("Erstes Teil gewählt: " + piece.name);
            }

            return;
        }

        if (firstSelected == piece)
        {
            if (logClicks)
            {
                Debug.Log("Auswahl aufgehoben: " + piece.name);
            }

            firstSelected = null;
            return;
        }

        // zweites Teil ist auch gesperrt -> nichts machen
        if (piece.IsCorrect)
        {
            if (logClicks)
            {
                Debug.Log(piece.name + " ist gesperrt und kann nicht getauscht werden.");
            }
            return;
        }

        secondSelected = piece;

        if (logClicks)
        {
            Debug.Log("Zweites Teil gewählt: " + piece.name);
        }

        SwapPieces(firstSelected, secondSelected);

        firstSelected = null;
        secondSelected = null;

        RefreshAllPieces();
        CheckIfSolved();
    }

    private void SwapPieces(PuzzlePiece3D a, PuzzlePiece3D b)
    {
        int tempIndex = a.currentIndex;
        a.currentIndex = b.currentIndex;
        b.currentIndex = tempIndex;

        a.MoveToPosition(GetWorldPosition(a.currentIndex));
        b.MoveToPosition(GetWorldPosition(b.currentIndex));

        if (logClicks)
        {
            Debug.Log("Getauscht: " + a.name + " <-> " + b.name);
        }
    }

    private void RefreshAllPieces()
    {
        foreach (PuzzlePiece3D piece in pieces)
        {
            if (piece == null) continue;
            piece.UpdateVisual();
        }
    }

    public Vector3 GetWorldPosition(int index)
    {
        int x = index % columns;
        int z = index / columns;

        float startX = -(columns - 1) * spacing / 2f;
        float startZ = (rows - 1) * spacing / 2f;

        return new Vector3(
            startX + x * spacing,
            yLevel,
            startZ - z * spacing
        );
    }

    private void CheckIfSolved()
    {
        foreach (PuzzlePiece3D piece in pieces)
        {
            if (piece == null) continue;

            if (!piece.IsCorrect)
            {
                return;
            }
        }

        Debug.Log("Puzzle gelöst!");
    }

    private void ShufflePieces()
    {
        bool validShuffle = false;

        while (!validShuffle)
        {
            List<int> indices = new List<int>();

            for (int i = 0; i < pieces.Count; i++)
            {
                indices.Add(i);
            }

            // Fisher-Yates Shuffle
            for (int i = indices.Count - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);

                int temp = indices[i];
                indices[i] = indices[randomIndex];
                indices[randomIndex] = temp;
            }

            validShuffle = true;

            // Prüfen: Kein Teil darf auf seinem richtigen Platz landen
            for (int i = 0; i < pieces.Count; i++)
            {
                if (indices[i] == pieces[i].correctIndex)
                {
                    validShuffle = false;
                    break;
                }
            }

            if (validShuffle)
            {
                for (int i = 0; i < pieces.Count; i++)
                {
                    pieces[i].currentIndex = indices[i];
                }
            }
        }
    }
}