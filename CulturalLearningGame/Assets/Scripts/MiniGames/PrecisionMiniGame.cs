using UnityEngine;

public class PrecisionMiniGame : MonoBehaviour
{
    public bool taskActive = false;
    public static PrecisionMiniGame Instance;
    public PointerController pointer;
    void Awake()
    {
        Instance = this;
    }

    public void StartTask()
    {
        taskActive = true;
        Debug.Log("Precision mini-game started!");
        PrecisionUI.Instance.ShowUI();
        PrecisionUI.Instance.StartGameUI();

    }

    public void EndTask()
    {
        taskActive = false;
        Debug.Log("Precision mini-game ended!");
    }

     public void CompleteTask()
    {
        GameManager.Instance.precisionTaskComplete = true;
        GameManager.Instance.currentTask = GameManager.TaskType.None;
        Debug.Log("Precision Game complete!");
        EndTask();
    }

}
