using System;
using TMPro;
using UnityEngine;

public class LetterCube : MonoBehaviour
{
    [Header("Reference Settings")]
    [SerializeField] private TMP_Text letterText;
    [SerializeField] private Movement movement;
    [SerializeField] private MeshRenderer cubeMesh;
    [SerializeField] private Material selectedMaterial;

    private Vector3 oldPosition;
    private Vector3 selectPosition;
    private Material defaultMaterial;

    public char LetterValue { get; private set; }

    private void Awake()
    {
        defaultMaterial = cubeMesh.sharedMaterial;
    }

    public void Initialize(char letterValue)
    {
        letterText.text = letterValue.ToString();
        LetterValue = letterValue;
    }

    public void SetPosition(Vector3 currentPosition, Vector3 selectPosition, bool useMovement = false)
    {
        oldPosition = currentPosition;
        this.selectPosition = selectPosition;

        if (useMovement == false)
        {
            transform.position = currentPosition;
            return;
        }

        Move(currentPosition);
    }

    public void Select()
    {
        cubeMesh.sharedMaterial = selectedMaterial;
        oldPosition = transform.position;
        Move(selectPosition);
    }

    public void Move(Vector3 targetPosition, Action OnComplete = null)
    {
        movement.MoveToTarget(targetPosition);
    }

    public void Deselect()
    {
        cubeMesh.sharedMaterial = defaultMaterial;

        if (oldPosition == transform.position)
            return;

        movement.MoveToTarget(oldPosition);
    }
}
