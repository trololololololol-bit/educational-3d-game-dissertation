using UnityEngine;

public class DeliveryMiniGame : MonoBehaviour
{
    public static DeliveryMiniGame Instance;
    public GameObject cheeseIcons; 
    public int cheeseRemaining;
    public int startingCheese = 4;

    public AudioSource deliveryAudio;

    void Awake()
    {
        Instance = this;
    }

    public void StartTask()
    {
        Debug.Log("Delivery system running");

        cheeseRemaining = startingCheese;
        cheeseIcons.SetActive(true);
    }

    public void DeliverCheese()
    {
        if (cheeseRemaining <= 0) return;

        cheeseRemaining--;
        if (deliveryAudio != null)
        deliveryAudio.Play();


        if (cheeseRemaining <= 0)
        {
            CompleteTask();
        }
    }

    void CompleteTask()
    {
        Debug.Log("Delivery complete!");

        GameManager.Instance.deliveryTaskComplete = true;
        GameManager.Instance.currentTask = GameManager.TaskType.None;
        cheeseIcons.SetActive(false);

    
}
}
