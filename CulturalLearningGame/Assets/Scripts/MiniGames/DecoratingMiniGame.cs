using System.Xml.Serialization;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class DecoratingMiniGame : MonoBehaviour
{
   public bool taskActive = false;
   public Transform playerTransform;
   
   public static DecoratingMiniGame Instance;
   
      void Awake()
    {
        Instance = this;
    }


    void Update()
    {
        if (!taskActive) return;

        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            TryDecorate();
        }
    }

    public void StartTask()
    {
        taskActive = true;
        Debug.Log("Decoration mini-game started!");
    }

    public void EndTask()
    {
        taskActive = false;
        Debug.Log("Decoration mini-game ended!");
    }
    void TryDecorate()
    {
        float interactRange = 3f;
        Collider[] hits = Physics.OverlapSphere(playerTransform.position, interactRange);

        foreach (Collider hit in hits)
        {
            
            if (hit.GetComponentInParent<DecoratingSpot>() != null && !hit.GetComponentInParent<DecoratingSpot>().IsDecorated())
            {
                hit.GetComponentInParent<DecoratingSpot>().Decorate();
                
            
               break;
                
        }}}
    

    public void CompleteTask()
    {
        GameManager.Instance.decorationTaskComplete = true;
        GameManager.Instance.currentTask = GameManager.TaskType.None;
        Debug.Log("Decorations complete!");
        EndTask();
    }
}

