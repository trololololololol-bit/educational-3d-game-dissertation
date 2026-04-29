using UnityEngine;

public class GameManager : MonoBehaviour

{
    public static GameManager Instance;

    [Header("Tasks")]
    public float festivalSaturationLevel = 0f;
    public bool deliveryTaskComplete;
    public bool fishingTaskComplete;
    public bool decorationTaskComplete;

    public bool recallTaskComplete;
    public bool precisionTaskComplete;
   
    public bool fishingTaskRejected;
    public bool decorationTaskRejected;
    public bool deliveryTaskRejected;
    public bool precisionTaskRejected;
    public bool recallTaskRejected;

    public bool allTasksComplete()
    {
        return precisionTaskComplete &&
        recallTaskComplete &&
        fishingTaskComplete &&
        deliveryTaskComplete &&
        decorationTaskComplete;
    }
    private NPCInteractable NPCI;
    


    [Header("UI")]
    public GameObject taskPanel;
    

    public void ShowTaskList()
    {
        taskPanel.SetActive(true);
    }


    public int decorationsCompleted = 0;
    public int totalDecorations = 6;

    public int fishCaught = 0;
    public int totalFish = 6;


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
        FishingMiniGame.Instance.StartTask();
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

        Debug.Log("Current task before starting: " + currentTask);

        currentTask = TaskType.None;

        currentTask = TaskType.Recall;
        Debug.Log("Recall game started");
        RecallMiniGame.Instance.StartTask();
    }

    public void StartPrecisionGame()
    {

      
        if (currentTask != TaskType.None)
        {
        Debug.Log("Another task is already active");
        return;
        }

        currentTask = TaskType.Precision;
        PrecisionMiniGame.Instance.StartTask();
        Debug.Log("Presicion game started");
    }



    public void DesaturateEnvironment()
    {
        Debug.Log("Festival gets worse...Darker...");

    
    }

     public void TaskCompleted()
    {
        festivalSaturationLevel +=0.2f;
        festivalSaturationLevel = Mathf.Clamp01(festivalSaturationLevel); //between 0&1
    }

    public void TaskRejected()
    {
        festivalSaturationLevel -=0.2f;
        festivalSaturationLevel = Mathf.Clamp01(festivalSaturationLevel);
    }

    public void RejectDelivery()
    {
        deliveryTaskRejected = true;
        TaskRejected();
        currentTask = TaskType.None;
        Debug.Log("Delivery task has been rejected.");
    }


    public void RejectDecoration()
    {
        decorationTaskRejected = true;
        TaskRejected();
        currentTask = TaskType.None;
        Debug.Log("Decoration task has been rejected.");
    }

    public void RejectRecall()
    {
        recallTaskRejected = true;
        TaskRejected();
        currentTask = TaskType.None;
        Debug.Log("Recall task has been rejected.");
    }

    public void RejectFishing()
    {
        fishingTaskRejected = true;
        TaskRejected();
        currentTask = TaskType.None;
        Debug.Log("Fishing task has been rejected.");
    }

    public void RejectPrecision()
    {
        precisionTaskRejected = true;
        TaskRejected();
        currentTask = TaskType.None;
        Debug.Log("Precision task has been rejected.");
    }

   
}
