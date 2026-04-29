using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DialogueEditor;
using UnityEngine.SceneManagement;


public class NPCInteractable : MonoBehaviour {

    [Header("DialogueEditor")]
    public NPCConversation normalConversation;
    public NPCConversation sadConversation;
    public NPCConversation rejectedConversation;
    public NPCConversation completedConversation;
    
    public NPCConversation activeConversation; 
    public NPCConversation busyConversation; 
    public NPCConversation thankYouDialogue;

    [Header("Delivery")]
    public bool hasReceivedCheese = false;


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
                Recall();
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

    bool IsNPCTaskActive()
    {
      

        switch (npcRole)
        {
            case NPCRole.DeliveryTask:
                return GameManager.Instance.currentTask == GameManager.TaskType.Delivery;

            case NPCRole.FishingTask:
                return GameManager.Instance.currentTask == GameManager.TaskType.Fishing;

            case NPCRole.DecorationTask:
                return GameManager.Instance.currentTask == GameManager.TaskType.Decoration;

            case NPCRole.RecallMiniGame:
                return GameManager.Instance.currentTask == GameManager.TaskType.Recall;

            case NPCRole.PrecisionMiniGame:
                return GameManager.Instance.currentTask == GameManager.TaskType.Precision;
        }

    return false;
    }



        void StartConversation()
        {
     
        NPCConversation chosenConversation = normalConversation;
        

        // role specific

        if (GameManager.Instance.allTasksComplete() && completedConversation != null)
            {
            chosenConversation = completedConversation;
            } else {
        

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


        case NPCRole.RecallMiniGame:
            if (GameManager.Instance.recallTaskComplete && completedConversation != null)
                chosenConversation = completedConversation;
            else if (GameManager.Instance.recallTaskRejected && rejectedConversation != null)
                chosenConversation = rejectedConversation;
            break;

        case NPCRole.PrecisionMiniGame:
            if (GameManager.Instance.precisionTaskComplete && completedConversation != null)
                chosenConversation = completedConversation;
            else if (GameManager.Instance.precisionTaskRejected && rejectedConversation != null)
                chosenConversation = rejectedConversation;
            break;
        }}

        //task state
          if (GameManager.Instance.currentTask != GameManager.TaskType.None)
        {
             if (IsNPCTaskActive())
            {
            if (activeConversation != null)
                chosenConversation = activeConversation; // if it is teh npc's own task
             }
            else
            {
            
            if (busyConversation != null)
                chosenConversation = busyConversation; // if its another npc either busy or hungry
        }}

        // global
        if(chosenConversation == normalConversation) {
        if (GameManager.Instance.festivalSaturationLevel < 0.4f && sadConversation != null)
            {
            chosenConversation = sadConversation;
            }
        }

       
        
        // start convo
        
    
            ConversationManager.Instance.StartConversation(chosenConversation);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            ConversationManager.OnConversationEnded += LockCursorAgain;
            }

             

    public void StartThankYouDialogue()
{
    if (thankYouDialogue != null)
    {
        ConversationManager.Instance.StartConversation(thankYouDialogue);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        ConversationManager.OnConversationEnded += LockCursorAgain;
    }
}

    private void LockCursorAgain()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        ConversationManager.OnConversationEnded -= LockCursorAgain;
    }

    public void Recall()
    {
            ConversationManager.OnConversationEnded -= StartRecall;
            StartConversation();
            ConversationManager.OnConversationEnded += StartRecall;
            
    }

    public void StartRecall()
    {
        ConversationManager.OnConversationEnded -= StartRecall;
        GameManager.Instance.StartRecallGame();
    }

    public void FinalConversation()
    {
        if (completedConversation != null)
    {
        ConversationManager.Instance.StartConversation(completedConversation);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        ConversationManager.OnConversationEnded += OnFinalConvoEnded;
    }
    }

    public void LoadFinalScene()
    {
        SceneManager.LoadScene("FinalScene");
    }

    public void OnFinalConvoEnded()
    {
        ConversationManager.OnConversationEnded -= OnFinalConvoEnded;
        LoadFinalScene();
    }

   

   
}




    