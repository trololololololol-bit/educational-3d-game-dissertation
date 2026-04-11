using UnityEngine;
using TMPro;

public class FishingUI : MonoBehaviour
{

    
    public GameObject panel;
    public TextMeshProUGUI fishCounterTxt;

    void Start()
    {
        panel.SetActive(false);
        }
        void Update()
    {
        Debug.Log("fISH UI running");
        if(GameManager.Instance.currentTask == GameManager.TaskType.Fishing) 
        {
            panel.SetActive(true);
            int curr = GameManager.Instance.fishCaught;
            int total = GameManager.Instance.totalFish;

            if (curr < total) {
                 fishCounterTxt.text = curr + " / " + total + " Fish Caught!";

            } else
            {
                 fishCounterTxt.text = "Fishing Completed!";
            }
        }else
        {
            panel.SetActive(false);
        }

    }

    



}
