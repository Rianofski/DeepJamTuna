using UnityEngine;

[CreateAssetMenu(fileName = "New Note", menuName = "BirthOfMiracle/Note Data")]
public class NoteData : ScriptableObject
{
    [Header("Genel Bilgiler")]
    public string noteID; // Örn: note_01
    
    [TextArea(3, 10)] 
    public string incompleteSentence; // Örn: "Bu barınak ... yüzünden terk edilmiş."

    [Header("Seçenek 1 (Yüzeysel)")]
    public string optionSurfaceText; // Örn: "Yorgunluk"
    [TextArea(2, 5)]
    public string resultSurfaceText; // Seçilince görünecek tam metin.

    [Header("Seçenek 2 (Derin/Lore)")]
    public string optionDeepText; // Örn: "Zamanın Donması"
    [TextArea(2, 5)]
    public string resultDeepText; // Seçilince görünecek tam metin.
    public bool unlocksLore; // Bu seçenek seçilirse Lore açılacak mı?
}