using UnityEngine;

[CreateAssetMenu(fileName = "New Note", menuName = "Inventory/Note")]
public class NoteData : ScriptableObject
{
    public string noteTitle;
    [TextArea(5, 10)] public string noteText;
    public Sprite noteImage; // opcional si quieres agregar una imagen
}
