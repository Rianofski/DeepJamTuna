using UnityEngine;
using TMPro; // TextMeshPro kütüphanesi
using UnityEngine.EventSystems; // Mouse olaylarını (Giriş/Çıkış) yakalamak için şart

public class TextHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI myText;
    private Color originalColor;

    [Header("Efekt Ayarları")]
    public Color hoverColor = new Color(0.4f, 0.2f, 0.0f); // Üzerine gelinceki renk
    public bool useUnderline = true;

    void Start()
    {
        myText = GetComponentInChildren<TextMeshProUGUI>();
        
        if (myText != null)
        {
            originalColor = myText.color;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (myText != null)
        {
            myText.color = hoverColor;
            
            if (useUnderline)
                myText.fontStyle = FontStyles.Underline;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (myText != null)
        {
            myText.color = originalColor;
            
            if (useUnderline)
                myText.fontStyle = FontStyles.Normal;
        }
    }
}