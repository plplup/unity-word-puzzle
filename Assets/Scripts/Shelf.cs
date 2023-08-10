using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    [Header("Reference Settings")]
    [SerializeField] private Transform letterStartTransform;
    [SerializeField] private Transform shelfEntraceTransform;

    [Header("Behavior Settings")]
    [SerializeField] private int letterCapacity;

    public Stack<Vector3> positionsStack;
    private Dictionary<Vector3, LetterCube> positionToLetterCubeDict;

    public int LetterCapacity => letterCapacity;
    public Transform LetterStartTransform => letterStartTransform;
    public Transform ShelfEntranceTransform => shelfEntraceTransform;
    public bool CanAddNewLetter => positionToLetterCubeDict != null && positionToLetterCubeDict.Count < letterCapacity;
    public IReadOnlyDictionary<Vector3, LetterCube> LetterCubes => positionToLetterCubeDict;

    public void Initialize(float spacingX, float repositionZ)
    {
        positionsStack = new Stack<Vector3>();
        positionToLetterCubeDict = new Dictionary<Vector3, LetterCube>();

        Vector3 newPosition = LetterStartTransform.position;
        for (int i = 0; i < letterCapacity; i++)
        {
            positionsStack.Push(newPosition);
            newPosition += new Vector3(spacingX, 0, repositionZ);
        }
    }

    public Vector3 GetAvailablePosition()
    {
        if (positionsStack == null || positionToLetterCubeDict == null)
        {
            Debug.LogError("Shelve positions not found");
            return Vector3.zero;
        }

        var availablePos = Vector3.zero;

        foreach(var position in positionsStack)
        {
            if (positionToLetterCubeDict.ContainsKey(position) == false)
                availablePos = position;
        }

        return availablePos;
    }

    public LetterCube GetLastLetterCube()
    {
        foreach (var postion in positionsStack)
        {
            if (positionToLetterCubeDict.TryGetValue(postion, out var letterCube) == true)
                return letterCube;
        }

        return null;
    }

    public void AddLetterCubeToStack(LetterCube letterCube, Vector3 targetPosition)
    {
        if (positionsStack == null)
            return;

        if (positionToLetterCubeDict.ContainsKey(targetPosition) == true)
            return;

        positionToLetterCubeDict.Add(targetPosition, letterCube);
    }

    public void RemoveLastLetter()
    {
        if (positionsStack == null || positionsStack.Count == 0)
            return;

        foreach (var position in positionsStack)
        {
            if (positionToLetterCubeDict.ContainsKey(position) == true)
            { 
                positionToLetterCubeDict.Remove(position);
                break;
            }
        }
    }

    public void Deselect()
    {
        foreach(var kvp in positionToLetterCubeDict)
        {
            kvp.Value.Deselect();
        }
    }

    public void ClearShelfData()
    {
        positionToLetterCubeDict.Clear();
    }

    public string GetCurrentWord()
    {
        string shelfString = string.Empty;

        if (positionToLetterCubeDict == null || positionToLetterCubeDict.Count == 0)
            return shelfString;

        foreach (var letter in LetterCubes)
        {
            shelfString += letter.Value.LetterValue;
        }

        return shelfString;
    }

    private void OnMouseDown()
    {        
        MessageSender<Messages>.Send(Messages.SelectShelve, this);
    }
}
