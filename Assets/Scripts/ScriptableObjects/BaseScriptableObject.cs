using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class BaseScriptableObject : ScriptableObject
{
    [Tooltip("ScriptableObject ID")]
    [SerializeField] private string id;
    public string Id => id;

#if UNITY_EDITOR
    [ContextMenu("Generate ID")]
    private void GenerateGuid()
    {
        id = Guid.NewGuid().ToString();
        EditorUtility.SetDirty(this);
    }
#endif

    public void SetId(string id)
    {
        this.id = id;
    }
}

