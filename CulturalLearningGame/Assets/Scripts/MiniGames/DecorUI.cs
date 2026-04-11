using UnityEngine;
using TMPro;

public class DecorUI : MonoBehaviour
{

    
    public GameObject panel;
    public TextMeshProUGUI decorCounterTxt;

    void Start()
    {
        panel.SetActive(false);
        }
        void Update()
    {
        Debug.Log("DecorUI running");
        if(GameManager.Instance.currentTask == GameManager.TaskType.Decoration) 
        {
            panel.SetActive(true);
            int curr = GameManager.Instance.decorationsCompleted;
            int total = GameManager.Instance.totalDecorations;

            if (curr < total) {
                 decorCounterTxt.text = curr + " / " + total + " Decorated";

            } else
            {
                 decorCounterTxt.text = "Decorations Completed!";
            }
        }else
        {
            panel.SetActive(false);
        }

    }

    



}
