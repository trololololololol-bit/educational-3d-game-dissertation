using UnityEngine;
using TMPro;

public class MessageUI : MonoBehaviour
{
    public GameObject textPanel;
    public TextMeshProUGUI messageText;
    
    void Start()
    {
        ShowMessage("Hey Sibling.");
    }

    public void ShowMessage(string message)
    {
        textPanel.SetActive(true);
        messageText.text = message; 
    }

    public void RemoveMessage()
    {
        textPanel.SetActive(false);
    }
}

