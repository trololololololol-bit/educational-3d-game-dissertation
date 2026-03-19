using UnityEngine;

public class GameManager : MonoBehaviour

{
    public static GameManager Instance;

    [Header("Tasks")]
    public float festivalSaturationLevel = 1f;
    public bool deliveryTaskComplete;
    public bool fishingTaskComplete;
    public bool decorationTaskComplete;

    public bool recallTaskComplete;
    public bool precisionTaskComplete;
   
    public bool fishingTaskRejected;
    public bool decorationTaskRejected;
    public bool deliveryTaskRejected;
    


    [Header("UI")]
    public GameObject taskPanel;
    

    public void ShowTaskList()
    {
        taskPanel.SetActive(true);
    }


    


    void Awake()
    {
        Instance = this;
    }
   

    public void StartFishingTask()
    {
        Debug.Log("Fishing task started");
        
    }

    public void StartDecorationTask()
    {
        Debug.Log("Decoration task started");
    }

    public void StartDeliveryTask()
    {
        Debug.Log("Delivery task started");
        DeliveryMiniGame.Instance.StartTask();
    }

    public void StartRecallGame()
    {
        Debug.Log("Recall game started");
        //RecallMiniGame.Instance.StartTask();
    }

    public void StartPrecisionGame()
    {
        Debug.Log("Presicion game started");
    }



    public void DesaturateEnvironment()
    {
        Debug.Log("Festival gets worse...Darker...");

    
    }

}
