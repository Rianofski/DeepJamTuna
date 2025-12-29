using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CollectedNote
{
    public NoteData data;
    public bool isResolved;
    public bool chosenDeep;
}

public class NotebookManager : MonoBehaviour
{
    public static NotebookManager Instance;

    [Header("Ana Panel Ayarları")]
    public GameObject notebookPanel;
    public CanvasGroup panelCanvasGroup;
    public Button btnClose;

    [Header("Sayfalar (Sekme İçerikleri)")]
    public GameObject pageObservation;
    public GameObject pageCharacters; 
    public GameObject pageMenu;       

    [Header("Gözlem Sayfası Elemanları")]
    public TextMeshProUGUI leftText;
    public TextMeshProUGUI rightText;
    public Button btnSurface;
    public Button btnDeep;
    public Button btnNext;
    public Button btnPrev;
    
    [Header("Ayarlar UI Elemanları")]
    public GameObject settingsContainer;
    public Slider volumeSlider;
    public Toggle fullscreenToggle;
    
    [Header("Ses Ayarları")]
    public AudioSource audioSourceFX;     
    public AudioSource audioSourceWriting;
    
    public AudioClip pageTurnClip;
    public AudioClip writingClip;

    private Coroutine currentTypingCoroutine;

    private List<CollectedNote> myNotes = new List<CollectedNote>();
    private int currentNoteIndex = 0;
    private float typingSpeed = 0.04f;


    private void Start()
    {
        if(volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume;
            // Slider değişince SetVolume fonksiyonunu çalıştır
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }

        if(fullscreenToggle != null)
        {
            fullscreenToggle.isOn = Screen.fullScreen;
            // Toggle değişince SetFullscreen fonksiyonunu çalıştır
            fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        }
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        notebookPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (notebookPanel.activeSelf) CloseNotebook();
            else OpenNotebookTab(0);
        }
    }

    // --- TAB SYSTEM ---
    
    // 0: Observation, 1: Characters, 2: Map
    public void OpenNotebookTab(int tabIndex)
    {
        StopTyping(); 

        if (!notebookPanel.activeSelf)
        {
            notebookPanel.SetActive(true);
            Time.timeScale = 0;
            StartCoroutine(FadeInPanel());
        }

        pageObservation.SetActive(false);
        pageCharacters.SetActive(false);
        pageMenu.SetActive(false);
        
        if(settingsContainer != null) settingsContainer.SetActive(false);

        if(audioSourceFX && pageTurnClip) audioSourceFX.PlayOneShot(pageTurnClip);

        switch (tabIndex)
        {
            case 0:
                pageObservation.SetActive(true);
                if(myNotes.Count > 0) UpdateObservationUI();
                break;
            case 1:
                pageCharacters.SetActive(true);
                break;
            case 2:
                pageMenu.SetActive(true);
                break;
        }
    }


    public void DiscoverNote(NoteData newData)
    {
        bool alreadyExists = false;
        for (int i = 0; i < myNotes.Count; i++)
        {
            if (myNotes[i].data == newData)
            {
                currentNoteIndex = i;
                alreadyExists = true;
                break;
            }
        }

        if (!alreadyExists)
        {
            CollectedNote newEntry = new CollectedNote();
            newEntry.data = newData;
            newEntry.isResolved = false;
            myNotes.Add(newEntry);
            currentNoteIndex = myNotes.Count - 1;
        }

        OpenNotebookTab(0);
    }

    private void UpdateObservationUI()
    {
        if (myNotes.Count == 0) return;

        CollectedNote currentNote = myNotes[currentNoteIndex];

        StopTyping();
        leftText.text = "";
        rightText.text = "";

        if (!currentNote.isResolved)
        {
            btnSurface.gameObject.SetActive(true);
            btnDeep.gameObject.SetActive(true);
            btnSurface.GetComponentInChildren<TextMeshProUGUI>().text = currentNote.data.optionSurfaceText;
            btnDeep.GetComponentInChildren<TextMeshProUGUI>().text = currentNote.data.optionDeepText;
        }
        else
        {
            btnSurface.gameObject.SetActive(false);
            btnDeep.gameObject.SetActive(false);
        }

        btnPrev.interactable = (currentNoteIndex > 0);
        btnNext.interactable = (currentNoteIndex < myNotes.Count - 1);

        currentTypingCoroutine = StartCoroutine(WritePageSequence(currentNote));
    }

    IEnumerator WritePageSequence(CollectedNote note)
    {
        yield return StartCoroutine(TypeEffect(note.data.incompleteSentence, leftText));

        if (note.isResolved)
        {
            yield return new WaitForSecondsRealtime(0.2f);

            string finalResult = note.chosenDeep ? note.data.resultDeepText : note.data.resultSurfaceText;
            yield return StartCoroutine(TypeEffect(finalResult, rightText));
        }
    }
    
    public void MakeChoice(bool isDeep)
    {
        CollectedNote currentNote = myNotes[currentNoteIndex];
        currentNote.isResolved = true;
        currentNote.chosenDeep = isDeep;

        btnSurface.gameObject.SetActive(false);
        btnDeep.gameObject.SetActive(false);

        string resultText = isDeep ? currentNote.data.resultDeepText : currentNote.data.resultSurfaceText;
        
        StopTyping();
        
        currentTypingCoroutine = StartCoroutine(TypeEffect(resultText, rightText));
    }
    
    private void StopTyping()
    {
        if (currentTypingCoroutine != null) 
        {
            StopCoroutine(currentTypingCoroutine);
            currentTypingCoroutine = null; // Değişkeni boşa çıkar
        }
        
        if (audioSourceWriting != null && audioSourceWriting.isPlaying)
        {
            audioSourceWriting.Stop();
        }
    }
    // --- NAVİGASYON ---

    public void NextNote()
    {
        if (currentNoteIndex < myNotes.Count - 1)
        {
            currentNoteIndex++;
            if(audioSourceFX) audioSourceFX.PlayOneShot(pageTurnClip);
            UpdateObservationUI();
        }
    }

    public void PrevNote()
    {
        if (currentNoteIndex > 0)
        {
            currentNoteIndex--;
            if(audioSourceFX) audioSourceFX.PlayOneShot(pageTurnClip);
            UpdateObservationUI();
        }
    }

    public void CloseNotebook()
    {
        StopTyping();

        notebookPanel.SetActive(false);
        Time.timeScale = 1;
    }

    // --- EFEKTLER ---
    
    IEnumerator FadeInPanel()
    {
        panelCanvasGroup.alpha = 0;
        float timer = 0;
        while (timer < 0.3f)
        {
            timer += Time.unscaledDeltaTime;
            panelCanvasGroup.alpha = Mathf.Lerp(0, 1, timer / 0.3f);
            yield return null;
        }
        panelCanvasGroup.alpha = 1;
    }

    IEnumerator TypeEffect(string text, TextMeshProUGUI targetText)
    {
        targetText.text = "";
        
        if (audioSourceWriting && writingClip)
        {
            audioSourceWriting.clip = writingClip;
            audioSourceWriting.Play();
        }

        foreach (char letter in text.ToCharArray())
        {
            if (!targetText.gameObject.activeInHierarchy || !notebookPanel.activeSelf)
            {
                if (audioSourceWriting) audioSourceWriting.Stop();
                yield break; 
            }

            targetText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        if (audioSourceWriting)
        {
            audioSourceWriting.Stop();
        }
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void QuitGame()
    {
        Debug.Log("Oyundan Çıkılıyor...");
        Application.Quit();
    }

    public void ResumeGame()
    {
        CloseNotebook();
    }

    public void OpenSettings()
    {
        bool isActive = settingsContainer.activeSelf;
        settingsContainer.SetActive(!isActive);
    }
}