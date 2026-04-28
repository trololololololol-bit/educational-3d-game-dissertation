using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class IntroCutscene : MonoBehaviour
{

    public GameObject panel;
    public Image slide;

    public TextMeshProUGUI captiontext;

    public string[] captions;
    public Sprite[] slides;
    public Player player;
    private int current = 0;


    void Start()
    {
        panel.SetActive(false);
    }

    public void StartSlideshow()
    {
        panel.SetActive(true);
        player.StopMoving();
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
