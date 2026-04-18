using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.Collections;




public class RecallMiniGame : MonoBehaviour
{
    public RecallUI ui;
    private int totalRounds = 7;
    private int round = 0;
    public MonoBehaviour playerController;

    private bool canClick = false;
    public string[] ingredients;

    public List<int> sequence = new List<int>();
    private int currInput;
    public bool taskActive = false;
    public static RecallMiniGame Instance;
    bool isPlaying = false;
    public AudioSource audioSource;
    public AudioClip failSound;
    public AudioClip correctSequence;
    public AudioClip winSound;
    private float speed = 0.9f;
    private float highlightTime = 0.8f;
    private float pauseTime = 0.4f; 

    void Awake()
    {
        Instance = this;
    }

    void PlaySound(AudioClip clip)
    {
    audioSource.pitch = Random.Range(0.95f, 1.05f);
    audioSource.PlayOneShot(clip);
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
        ui.counterText.text = "Rounds: " + round + "/7";
        ui.darioText.text = "Dario: Help me make some bruschetta.";
        highlightTime = 0.8f;
        pauseTime = 0.4f; 
        AddToSequence();
        
        
    }

 

    void AddToSequence()
    {
        if(isPlaying) return; 
        int random = Random.Range(0, ui.buttonCount);
        sequence.Add(random);
        string line = "Dario: ";

        foreach (int i in sequence)
        {
            line += ingredients[i] + "... ";
        }

        ui.darioText.text = line;
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
            yield return new WaitForSeconds(highlightTime);
            ui.ResetButton(index);
            yield return new WaitForSeconds(pauseTime);
        } 
        
        currInput = 0;
        yield return new WaitForSeconds(0.1f);
        canClick = true;
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
            PlaySound(correctSequence);
            Debug.Log("Clicked: " + index);
            
        
        if(currInput >= sequence.Count)
        {
            canClick = false;
            PlaySound(winSound);
            RoundComplete();
        }}
        else
        {
            canClick = false;
            ui.FlashFail();
            PlaySound(failSound);
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
        ui.counterText.text = "Rounds: " + round + "/7";
        highlightTime = highlightTime * speed;
        pauseTime = pauseTime * speed;
        if(round > totalRounds)
        {
            WinGame();
            ui.FlashGood();
        } else
        {
            StartCoroutine(NextRoundTransition());
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
        playerController.enabled = true;
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

    IEnumerator NextRoundTransition()
    {
        yield return new WaitForSeconds(0.5f);
        AddToSequence();
    }




}
