using UnityEngine;
using TMPro;

public class DecorUI : MonoBehaviour
{

    public TextMeshProUGUI decorCounterTxt;
    public GameObject panel;
        void Update()
    {
        if(GameManager.Instance.currentTask == GameManager.TaskType.Decoration) 
        {
            panel.SetActive(true);
            int curr = GameManager.Instance.decorationsCompleted;
            int total = GameManager.Instance.totalDecorations;

            if (curr < total) {
                 decorCounterTxt.text = curr + "/" + total + "Decorated";

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
