using UnityEngine;

public class NPCInteractable : MonoBehaviour {

    private NPCDialogue dialogue;

    private void Awake()
    {
        dialogue = GetComponentInChildren<NPCDialogue>();
    }


    public enum NPCRole
    {
        FestivalDialogue,
        RecallMiniGame,
        PrecisionMiniGame,
        DeliveryTask,
        DecorationTask,
        FishingTask
    }

    [Header("NPC")]
    public NPCRole npcRole;

    [TextArea]
    public string dialogueText;

    public void Interact()
    {
        
        switch (npcRole)
        {
            
            case NPCRole.FestivalDialogue:
                OpenDialogue();
                break;

            case NPCRole.RecallMiniGame:
                StartRecallMiniGame();
                break;

            case NPCRole.PrecisionMiniGame:
                StartPrecisionMiniGame();
                break;

            case NPCRole.DeliveryTask:
                OpenDeliveryTask();
                break;

            case NPCRole.DecorationTask:
                OpenDecorationTask();
                break;

            case NPCRole.FishingTask:
                OpenFishingTask();
                break;
        }
    }

    void OpenDialogue()
    {
        Debug.Log("Dialogue NPC says: " + dialogueText);
    }

    void StartRecallMiniGame()
    {
        Debug.Log("Dario starts recall mini-game"+ dialogueText);
    }

    void StartPrecisionMiniGame()
    {
        Debug.Log("Dino starts precision mini-game"+ dialogueText);
    }

    void OpenDeliveryTask()
    {
        Debug.Log("NPC offers delivery task - accept or decline"+ dialogueText);
    }

    void OpenDecorationTask()
    {
        Debug.Log("NPC offers decoration task- accept or decline"+ dialogueText);
    }
   
    void OpenFishingTask()
    {
        Debug.Log("NPC offers fishing task - accept or decline"+ dialogueText);
    }
}
