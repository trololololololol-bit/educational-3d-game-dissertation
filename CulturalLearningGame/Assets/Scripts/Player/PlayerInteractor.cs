using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.eKey.wasPressedThisFrame) {
        float interactRange = 3f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach(Collider collider in colliderArray)
            {
                if (collider.GetComponentInParent<NPCInteractable>() is NPCInteractable npcInteraction)
                {
                    npcInteraction.Interact();
                }
            }
        }

        if (Keyboard.current.fKey.wasPressedThisFrame)
    {
    TryDeliver();
    }
        
    }

    public NPCInteractable GetInteractableObject()
    {
        
        float interactRange = 3f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach(Collider collider in colliderArray)
            {
                if (collider.GetComponentInParent<NPCInteractable>() is NPCInteractable npcInteraction)
                {
                    return npcInteraction;
                }
            } return null;
    }

void TryDeliver()
{
    if (DeliveryMiniGame.Instance.cheeseRemaining <= 0)
        return;

    float interactRange = 3f;
    Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);

    foreach(Collider collider in colliderArray)
    {
        if (collider.GetComponentInParent<NPCInteractable>() != null && !collider.GetComponentInParent<NPCInteractable>().hasReceivedCheese)
        {
            collider.GetComponentInParent<NPCInteractable>().hasReceivedCheese = true; 
            DeliveryMiniGame.Instance.DeliverCheese();
            collider.GetComponentInParent<NPCInteractable>().StartThankYouDialogue();
            break;
        }
    }
}





}
