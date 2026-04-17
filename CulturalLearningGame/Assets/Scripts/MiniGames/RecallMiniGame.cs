using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.Collections;




public class RecallMiniGame : MonoBehaviour
{
    public RecallUI ui;
    private int totalRounds = 6;
    private int round = 0;
    public MonoBehaviour playerController;

    private bool canClick = false;
    public string[] ingredients;

    public List<int> sequence = new List<int>();
    private int currInput;
    public bool taskActive = false;
    public static RecallMiniGame Instance;
    bool isPlaying = false;

    void Awake()
    {
        Instance = this;
    }
    

    public void StartTask()
    {
        ui.ShowUI();
        taskActive = true;
        Debug.Log("Recall mini-game started!");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StartGame();
        
    }

    public void StartGame()
    {
       
        round =1; 
        sequence.Clear();
        ui.darioText.text = "Dario: Help me make some bruschetta.";
        AddToSequence();
        
        
    }

 

    void AddToSequence()
    {
        int random = Random.Range(0, ui.buttonCount);
        sequence.Add(random);
        ui.darioText.text = "Dario: Okay. Add some " + ingredients[random];
        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        if (isPlaying) yield break;
        isPlaying = true;
        canClick = false;
        yield return new WaitForSeconds(0.5f);
        foreach(int index in new List<int>(sequence)) {
            ui.HighlightButton(index);
            yield return new WaitForSeconds(0.8f);
            ui.ResetButton(index);
            yield return new WaitForSeconds(0.3f);
        } 
        canClick = true;
        currInput = 0;
        isPlaying = false;
    }

    public void PlayerInput(int index)
    {
        if(!canClick) return;
        ui.HighlightButton(index);
        StartCoroutine(ResetAfter(index));
        if(index == sequence[currInput])
        {
            currInput++;
            Debug.Log("Clicked: " + index);
        
        if(currInput >= sequence.Count)
        {
            canClick = false;
            RoundComplete();
        }}
        else
        {
            canClick = false;
            ui.FlashFail();
            Fail();
        }

    }

    IEnumerator ResetAfter(int index)
    {
        yield return new WaitForSeconds(0.2f);
        ui.ResetButton(index);
    }

    public void RoundComplete()
    {
        round ++;
        if(round > totalRounds)
        {
            WinGame();
            ui.FlashGood();
        } else
        {
            Invoke("AddToSequence", 0.5f);
        }
    }

    public void Fail()
    {
        canClick = false;
        ui.FlashFail();
        ui.FailText();
        Invoke("StartGame",3f);
    }

    public void WinGame()
    {
        canClick = false;
        ui.WinText();
        Invoke("CompleteTask",3f);
        
    }

    public void EndTask()
    {
        taskActive = false;
        Debug.Log("Recall mini-game ended!");
        playerController.enabled = false;
        ui.HideUI();
        
    }

     public void CompleteTask()
    {
        GameManager.Instance.recallTaskComplete = true;
        GameManager.Instance.currentTask = GameManager.TaskType.None;
        Debug.Log("Recall Game complete!");
        TaskUI.Instance.TaskPanelFalse();
        EndTask();
    }

    void ListIngredient(int index)
{
    ui.darioText.text = "Dario: Remember " + ingredients[index];
}


}
