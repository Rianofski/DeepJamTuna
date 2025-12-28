using UnityEngine;

public class InteractableNote : MonoBehaviour
{
    [Header("Bu Objeye Ait Not")]
    public NoteData noteData;

    private bool isPlayerClose = false;

    void Update()
    {
        if (isPlayerClose && Input.GetKeyDown(KeyCode.E))
        {
            if (!NotebookManager.Instance.notebookPanel.activeSelf)
            {
                NotebookManager.Instance.DiscoverNote(noteData);
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerClose = true;
            Debug.Log("Etkileşim Alanına Girildi. E'ye bas.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerClose = false;
            Debug.Log("Etkileşim Alanından Çıkıldı.");
        }
    }
}