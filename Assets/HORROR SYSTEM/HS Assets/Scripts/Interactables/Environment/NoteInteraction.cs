using UnityEngine;

public class NoteInteraction : InteractiveObject
{
    public string noteSound;
    public NoteData noteData;

    public override void Interact(GameObject player = null)
    {
        AudioManager.instance.Play(noteSound);
        NoteManager.instance.AddNote(noteData);
        UIManager.instance.ShowNote(noteData);  // abre el panel con la nota
    }
}