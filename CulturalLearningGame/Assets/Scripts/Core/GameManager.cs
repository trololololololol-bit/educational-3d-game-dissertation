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


    public int decorationsCompleted = 0;
    public int totalDecorations = 6;


    void Awake()
    {
        Instance = this;
    }


    // lock mini games
    public enum TaskType
    {
    None,
    Delivery,
    Fishing,
    Decoration,
    Recall,
    Precision
    }

    public TaskType currentTask = TaskType.None;
   

    public void StartFishingTask()
    {
        if (currentTask != TaskType.None)
        {
        Debug.Log("Another task is already active");
        return;
        }

        currentTask = TaskType.Fishing;
        Debug.Log("Fishing task started");
        
    }

    public void StartDecorationTask()
    {
        if (currentTask != TaskType.None)
        {
        Debug.Log("Another task is already active");
        return;
        }

        currentTask = TaskType.Decoration;
        DecoratingMiniGame.Instance.StartTask();
        Debug.Log("Decoration task started");
    }

    public void StartDeliveryTask()
    {

        if (currentTask != TaskType.None)
        {
        Debug.Log("Another task is already active");
        return;
        }

        currentTask = TaskType.Delivery;
        Debug.Log("Delivery task started");

        DeliveryMiniGame.Instance.StartTask();
    }

    public void StartRecallGame()
    {

        if (currentTask != TaskType.None)
        {
        Debug.Log("Another task is already active");
        return;
        }

        currentTask = TaskType.Recall;
        Debug.Log("Recall game started");
        //RecallMiniGame.Instance.StartTask();
    }

    public void StartPrecisionGame()
    {
        if (currentTask != TaskType.None)
        {
        Debug.Log("Another task is already active");
        return;
        }

        currentTask = TaskType.Precision;
        Debug.Log("Presicion game started");
    }



    public void DesaturateEnvironment()
    {
        Debug.Log("Festival gets worse...Darker...");

    
    }

}
