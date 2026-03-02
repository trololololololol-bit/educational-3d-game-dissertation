using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DialogueEditor;


public class NPCInteractable : MonoBehaviour {

    [Header("DialogueEditor")]
    public NPCConversation normalConversation;
    public NPCConversation sadConversation;
    public NPCConversation rejectedConversation;
    public NPCConversation completedConversation;
    


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

    public void Interact()
    {
        switch (npcRole)
        {
            case NPCRole.FestivalDialogue:
                StartConversation();
                break;

            case NPCRole.RecallMiniGame:
                StartConversation();
                Debug.Log("Start recall mini game");
                break;

            case NPCRole.PrecisionMiniGame:
                StartConversation();
                Debug.Log("Start precision mini game");
                break;

            case NPCRole.DeliveryTask:
                StartConversation();
                break;

            case NPCRole.DecorationTask:
                StartConversation();
                break;

            case NPCRole.FishingTask:
                StartConversation();
                break;
        }
    }

        void StartConversation()
    {
        NPCConversation chosenConversation = normalConversation;
        // global
        if (GameManager.Instance.festivalSaturationLevel < 0.4f && sadConversation != null)
        {
        chosenConversation = sadConversation;
        }
        

        // role specific

        switch (npcRole)
        {
        case NPCRole.FishingTask:

            if (GameManager.Instance.fishingTaskComplete && completedConversation != null)
                chosenConversation = completedConversation;

            else if (GameManager.Instance.fishingTaskRejected && rejectedConversation != null)
                chosenConversation = rejectedConversation;

            break;

        case NPCRole.DecorationTask:

            if (GameManager.Instance.decorationTaskComplete && completedConversation != null)
                chosenConversation = completedConversation;
            else if (GameManager.Instance.decorationTaskRejected && rejectedConversation != null)
                chosenConversation = rejectedConversation;
            break;

        case NPCRole.DeliveryTask:

            if (GameManager.Instance.deliveryTaskComplete && completedConversation != null)
                chosenConversation = completedConversation;
            else if (GameManager.Instance.deliveryTaskRejected && rejectedConversation != null)
                chosenConversation = rejectedConversation;

            break;
        }



        // start convo

        
            ConversationManager.Instance.StartConversation(chosenConversation);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            ConversationManager.OnConversationEnded += LockCursorAgain;
        
    }

    private void LockCursorAgain()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        ConversationManager.OnConversationEnded -= LockCursorAgain;
    }

   
}




    