using UnityEngine;
using TMPro;

public class MessageUI : MonoBehaviour
{
    public GameObject textPanel;
    public TextMeshProUGUI messageText;
    public Player player;
    
    void Start()
    {
        ShowMessage("Text Message from Sibling: Hey! Don't forget to gather the best recipes you can for our restaurant opening! Learn some useful skills and make a good impression. Understand what makes their food and culture special.");
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

