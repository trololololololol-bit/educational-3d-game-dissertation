using TMPro;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class RecallUI : MonoBehaviour
{
    public static RecallUI Instance;


    public Button[] buttons;
    public GameObject bgPanel;
    public GameObject darioPanel;
    public UnityEngine.UI.Image simonSaysPanel;
   
    public TextMeshProUGUI darioText;
    public TextMeshProUGUI counterText;
    public GameObject counterPanel;
    public int buttonCount => buttons.Length;
    public Color highlightColor = Color.blue;
    public Color regColor = Color.red;
    private Color originalColor;

    // Update is called once per frame
    public void Start()
    {
        HideUI();
        
        
    }
    
     void Awake()
    {
        Instance = this;
        originalColor = simonSaysPanel.color;
       
    }
    public void HighlightButton(int index)
    {
        Debug.Log("Highlighting: " + index);
        buttons[index].image.color = highlightColor;
    }

    public void ResetButton(int index)
    {
        Debug.Log("Resetting: " + index);
        buttons[index].image.color = regColor;
    }

    

    public void FailText()
    {
        darioText.text ="Dario: No, no, no. That's not how we do it. Lets try again.";
    }

    public void WinText()
    {
        darioText.text = "Dario: Perfetto! Just like tradition.";
    }

    public void HideUI()
    {
        bgPanel.SetActive(false);
    }
    public void ShowUI()
    {
        bgPanel.SetActive(true);
    }

     public void FlashFail()
    {
        StopAllCoroutines();
        StartCoroutine(FlashRed());
    }
    
    IEnumerator FlashRed()
    {
        simonSaysPanel.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        simonSaysPanel.color = originalColor;
    }

    public void FlashGood()
    {
       
        StartCoroutine(FlashGreen());
    }

    IEnumerator FlashGreen()
    {
        simonSaysPanel.color = Color.green;
        yield return new WaitForSeconds(1f);
        simonSaysPanel.color = originalColor;
    }

    
}
