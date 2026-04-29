using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FinalPanelUI : MonoBehaviour
{
    public TextMeshProUGUI buttontext;

    public GameObject panel;
    public Image slide;

    public TextMeshProUGUI captiontext;

    public string[] captions;
    public Sprite[] slides;
    public Player player;
    private int current = 0;
    public AudioSource manager;
    public AudioClip fireworks;

    void Start()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    public void StartSlideshow()
    {
        panel.SetActive(true);
        player.StopMoving();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

     if(fireworks!= null)
        {
            manager.PlayOneShot(fireworks);
        }
        ShowSlide();
    }

    public void NextSlide()
    {
        current++;
        if(current>=slides.Length)
        {
            EndSlideshow();
            return;
        }
        if(current == slides.Length-1)
        {
            buttontext.text = "Take Recipe";
        }
        ShowSlide();
        
    }

    void ShowSlide()
    {
        slide.sprite = slides[current];
        captiontext.text = captions[current];
    }

    void EndSlideshow()
    {
        panel.SetActive(false);
        player.StartMovingAgain();
    }

   

}
