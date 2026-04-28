using UnityEngine;
using TMPro;

public class MessageUI : MonoBehaviour
{
    public GameObject textPanel;
    public TextMeshProUGUI messageText;
    public Player player;
    
    void Start()
    {
        ShowMessage("Hey Sibling.");
        player.StopMoving();
    }

    public void ShowMessage(string message)
    {
        textPanel.SetActive(true);
        messageText.text = message; 
    }

    private void OnDisable()
    {
       
        player.StartMovingAgain();
    }
}

