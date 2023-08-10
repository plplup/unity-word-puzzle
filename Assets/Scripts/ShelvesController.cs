using System.Collections.Generic;
using UnityEngine;

public class ShelvesController : MonoBehaviour
{
    [Header("Reference Settings")]
    [SerializeField] private Shelf shelfPrefab;
    [SerializeField] private Shelf shelfSixSlotPrefab;
    [SerializeField] private LetterCube letterCubePrefab;
    [SerializeField] private Transform shelfSpawnStartPosition;

    [Header("Shelves Settings")]
    [SerializeField] private float spawnScreenXOffset;
    [SerializeField] private float shelfYSpacing;
    [SerializeField] private float letterCubeXSpacing;
    [SerializeField] private float letterCubeZReposition;

    private List<string> selectedWords;
    private List<Shelf> shelves;
    private List<LetterCube> letterCubes;
    private List<string> shelvesWords;

    private LetterCube selectedCube;
    private Shelf previousSelectedShelf;

    private void Awake()
    {
        MessageSender<Messages>.Subscribe(Messages.SelectShelve, OnShelveSelect);

        shelvesWords = new List<string>();
    }

    private void OnDestroy()
    {
        MessageSender<Messages>.Unsubscribe(Messages.SelectShelve, OnShelveSelect);
    }

    public void Initialize(int shelvesAmount, List<string> words, WordDataType wordDataType, float shelfXPosition, float shelfXOffset)
    {
        selectedWords = words;

        SpawnShelves(shelvesAmount, wordDataType, shelfXPosition, shelfXOffset);
        SpawnLetterCubes();
        ArrangeLetterCubes();
    }

    public void ResetShelves()
    {
        selectedCube = null;
        previousSelectedShelf = null;

        foreach (var shelf in shelves)
        {
            shelf.ClearShelfData();
        }
    }

    public void ArrangeLetterCubes(bool isReplay = false)
    {
        if (isReplay == false)
            letterCubes.Shuffle();

        foreach (var letter in letterCubes)
        {
            var availableShelve = GetAvailableShelve();

            var availabePosition = availableShelve.GetAvailablePosition();

            if (availabePosition == Vector3.zero) continue;

            letter.SetPosition(availabePosition, availableShelve.ShelfEntranceTransform.position);

            availableShelve.AddLetterCubeToStack(letter, availabePosition);

            if (isReplay == true)
                letter.Deselect();
        }

        if (isReplay == false)
            CheckLetterDispositionsToReArrange();
    }

    private void SpawnShelves(int shelvesAmount, WordDataType wordDataType, float shelfXPosition, float shelfXOffset)
    {
        shelves = new List<Shelf>();
        
        var shelfSpawnPos = shelfSpawnStartPosition.position;
        var spacing = new Vector3(0, shelfYSpacing, 0);

        for (int i = 0; i < shelvesAmount; i++)
        {
            var newShelf = Instantiate(wordDataType != WordDataType.SixLetter ? shelfPrefab : shelfSixSlotPrefab, shelfSpawnPos, shelfPrefab.transform.rotation);

            newShelf.transform.position = new Vector3(-(shelfXPosition / 2f) + shelfXOffset, newShelf.transform.position.y,
                newShelf.transform.position.z);

            newShelf.Initialize(letterCubeXSpacing, letterCubeZReposition);

            shelfSpawnPos -= spacing;

            shelves.Add(newShelf);
        }
    }

    private void SpawnLetterCubes()
    {
        letterCubes = new List<LetterCube>();

        foreach (var word in selectedWords)
        {
            foreach (var letter in word)
            {
                var newLetterCube = Instantiate(letterCubePrefab);

                newLetterCube.Initialize(letter);

                letterCubes.Add(newLetterCube);
            }
        }
    }

    private Shelf GetAvailableShelve()
    {
        foreach (var shelf in shelves)
        {
            if (shelf.CanAddNewLetter == true)
                return shelf;
        }
        return null;
    }

    private void TryMoveCubeToShelf(Shelf shelf)
    {
        if (shelf == previousSelectedShelf)
        {
            selectedCube.Deselect();
            selectedCube = null;
            return;
        }        

        if (shelf.CanAddNewLetter == false)
        {
            selectedCube.Deselect();
            selectedCube = shelf.GetLastLetterCube();
            selectedCube.Select();
            previousSelectedShelf = shelf;
            return;
        }

        previousSelectedShelf.RemoveLastLetter();

        var shelfAvailablePos = shelf.GetAvailablePosition();
        
        shelf.AddLetterCubeToStack(selectedCube, shelfAvailablePos);

        selectedCube.SetPosition(shelfAvailablePos, shelf.ShelfEntranceTransform.position, true);
        selectedCube.Deselect();
        selectedCube = null;
        previousSelectedShelf = null;

        ShelvesChanged();
    }

    private void OnShelveSelect(object data)
    {
        if (data is Shelf == false)
            return;

        if (selectedCube != null && previousSelectedShelf != null)
        {
            TryMoveCubeToShelf((Shelf)data);
            return;
        }

        var lastLetterCube = (data as Shelf).GetLastLetterCube();

        if (lastLetterCube == null)
            return;

        lastLetterCube.Select();

        selectedCube = lastLetterCube;

        previousSelectedShelf = (Shelf)data;

    }

    private void ShelvesChanged()
    {
        shelvesWords.Clear();

        foreach(var shelf in shelves)
        {
            shelvesWords.Add(shelf.GetCurrentWord());
        }

        MessageSender<Messages>.Send(Messages.ShelvesChanged, shelvesWords);
    }

    private bool CheckAnyShelfHasWordCompleted()
    {
        bool hasAnyCompletedWord = false;

        foreach (var word in selectedWords)
        {
            foreach (var shelf in shelves)
            {
                if (string.Equals(shelf.GetCurrentWord(), word) == true)
                {
                    hasAnyCompletedWord = true;
                    break;
                }
            }
        }

        return hasAnyCompletedWord;
    }

    private void CheckLetterDispositionsToReArrange()
    {
        if (CheckAnyShelfHasWordCompleted() == false)     
            return;

        ResetShelves();
        ArrangeLetterCubes();
    }
}
