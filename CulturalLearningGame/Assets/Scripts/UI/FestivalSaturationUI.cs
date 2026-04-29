using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class FestivalSaturationUI : MonoBehaviour
{
    public TextMeshProUGUI saturationCounter;
    public GameObject panel;

    void Start()
    {
        panel.SetActive(false);
    }

    public void SetAct()
    {
        panel.SetActive(true);
    }

    void Update()
    {
        if(GameManager.Instance != null)
        {
            int percent = Mathf.RoundToInt(GameManager.Instance.festivalSaturationLevel * 100);
            saturationCounter.text = "Overall Town Happiness:" + percent +"%";
        }
    }

}
