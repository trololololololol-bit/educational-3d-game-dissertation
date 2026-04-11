using System.Xml.Serialization;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishingMiniGame : MonoBehaviour
{
   public bool taskActive = false;
   public static FishingMiniGame Instance;
   public Transform playerTransform;
   private FishingSpot current;

      void Awake()
    {
        Instance = this;
    }


    void Update()
    {
        if (!taskActive) return;

        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            if(current != null)
            {
                current.RegisterInput();
            }else {
            TryFish();
            }
        }
    }

    public void StartTask()
    {
        taskActive = true;
        Debug.Log("Fishing mini-game started!");
    }

    public void EndTask()
    {
        taskActive = false;
        Debug.Log("Fishing mini-game ended!");
    }
    void TryFish()
    {
        float interactRange = 3f;
        Collider[] hits = Physics.OverlapSphere(playerTransform.position, interactRange);

        foreach (Collider hit in hits)
        {
            
            if (hit.GetComponentInParent<FishingSpot>() != null && !hit.GetComponentInParent<FishingSpot>().IsFished())
            {
                current = hit.GetComponentInParent<FishingSpot>();
                hit.GetComponentInParent<FishingSpot>().Fish();
               
                
            
               break;
                
        }}}
    
    public void Clear()
    {
        current = null;
    }

    public void CompleteTask()
    {
        GameManager.Instance.fishingTaskComplete = true;
        GameManager.Instance.currentTask = GameManager.TaskType.None;
        Debug.Log("Fishing complete!");
        EndTask();
    }
}

