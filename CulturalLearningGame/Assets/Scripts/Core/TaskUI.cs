using UnityEngine;

public class TaskUI : MonoBehaviour
{
    public GameObject deliveryTick;

    public GameObject fishingTick;
    public GameObject decorationTick;

    public GameObject darioTick;
    public GameObject dinoTick;

    public GameObject taskPanel;
    

    void Start()
    {
        taskPanel.SetActive(false);
        deliveryTick.SetActive(false);
        fishingTick.SetActive(false);
        decorationTick.SetActive(false);
        darioTick.SetActive(false);
        dinoTick.SetActive(false);
    }

      public void ShowTaskList()
    {
        taskPanel.SetActive(true);
    }


    void Update()
    {
        if(GameManager.Instance.deliveryTaskComplete)
            deliveryTick.SetActive(true);

        if(GameManager.Instance.fishingTaskComplete)
            fishingTick.SetActive(true);

        if(GameManager.Instance.decorationTaskComplete)
            decorationTick.SetActive(true);

        if(GameManager.Instance.recallTaskComplete)
            darioTick.SetActive(true);

        if(GameManager.Instance.precisionTaskComplete)
            dinoTick.SetActive(true);
    }
}
