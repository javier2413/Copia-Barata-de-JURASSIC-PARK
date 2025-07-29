using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public static NoteManager instance;
    private List<NoteData> collectedNotes = new List<NoteData>();

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void AddNote(NoteData note)
    {
        if (!collectedNotes.Contains(note))
        {
            collectedNotes.Add(note);
        }
    }

    public List<NoteData> GetAllNotes() => collectedNotes;
}