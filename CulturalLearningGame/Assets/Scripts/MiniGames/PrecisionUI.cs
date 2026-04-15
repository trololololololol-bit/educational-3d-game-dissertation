using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Security.Cryptography;

public class PrecisionUI : MonoBehaviour
{
    public GameObject bgPanel;
    public GameObject textPanel;
    private Color originalColor;
    public GameObject attemptPanel;
    public TextMeshProUGUI textTXT;
    public TextMeshProUGUI attemptTXT;
    public TextMeshProUGUI livesTXT;
    public Image darioImage;
    public TextMeshProUGUI textTxt;
    public TextMeshProUGUI timerTxt;
    public TextMeshProUGUI attemptsTxt;
    public static PrecisionUI Instance;
    public Image foodDisplay;
    public Sprite[] cookBurn;
    public Sprite[] rawIngredients;
    public Sprite[] choppedIngredients;

    void Awake()
    {
        Instance = this;
        originalColor = foodDisplay.color;
    }

    public void Start()
    {
        HideUI();
        
    }
    public void StartGameUI()
    {
        if(GameManager.Instance.currentTask == GameManager.TaskType.Precision)
        {
            ShowUI();
            StageOne();
            TaskUI.Instance.TaskPanelFalse();
        } 
        
    }
    public void ShowUI()
    {
    

    bgPanel.SetActive(true);
    textPanel.SetActive(true);
    attemptPanel.SetActive(true);

    }
    public void HideUI()
    {
        bgPanel.SetActive(false);
        textPanel.SetActive(false);
        attemptPanel.SetActive(false);
    
    }
    public void StageOne()
    {
        //prep
        textTxt.text = "Dario: This is the prep stage. Using your SPACEBAR, please help me chop these ingredients: Garlic, Herbs, Cheese...";
        
        
    }
    public void StageTwo()
    {
        //cook
        textTxt.text = "Dario: This is cooking stage. When the BLUE diamond reaches the safezone, flip the food. Try not to burn the food...";
        
    }

     public void BurnWarn()
    {
        //cook
        textTxt.text = "Dario: Careful! It's Burning!";
        
    }

     public void BurnedText()
    {
        //cook
        textTxt.text = "Dario: Oh no! It burned. Why don't you start over...";
        
    }
    public void StageThree()
    {
        //serve 
        textTxt.text = "Dario: This is serving stage. Top the palenta cake with some tomato, basil, and fresh mozerella. Make the food look as presentable as possible...";
        attemptsTxt.text = "Chops: /5";
    }

    public void UpdateProgress(int current, int max)
    {
        attemptsTxt.text = "Pieces collected:"+current + "/" + max;
        timerTxt.text = "";
        
    }

    public void FlashFail()
    {
        StopAllCoroutines();
        StartCoroutine(FlashRed());
    }
    
    IEnumerator FlashRed()
    {
        foodDisplay.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        foodDisplay.color = originalColor;
    }

    public void ShowChopped(int IngredientIndex)
    {
        foodDisplay.sprite = choppedIngredients[IngredientIndex];
    }

    public void ShowRawUnchopped(int IngredientIndex)
    {
        foodDisplay.sprite = rawIngredients[IngredientIndex];
    }

     public void UpdateLives(int currentLives)
    {
        livesTXT.text = "Lives:" + currentLives;
    }

    public void UpdateTimer(float timeLeft)
    {
        timerTxt.text = "Time Left:" + Mathf.CeilToInt(timeLeft);
    } 

    public void UpdateFlips(int current, int max)
    {
        attemptsTxt.text = "Flips:"+current + "/" + max;
    }

    public void ShowCooked(int IngredientIndex)
    {
        foodDisplay.sprite = cookBurn[IngredientIndex];
    }

    public void OntoStageThree()
    {
        attemptsTxt.text = "";
        timerTxt.text= "";
        livesTXT.text = "";
        textTxt.text = "Dario: Good Job. Now lets plate the food.";

    }

      public void EndingTask()
    {
        attemptsTxt.text = "";
        timerTxt.text= "";
        livesTXT.text = "";
        textTxt.text = "Dario: Well done.";

    }

     public void TryAgain()
    {
        attemptsTxt.text = "";
        timerTxt.text= "";
        livesTXT.text = "";
        textTxt.text = "Dario: Lets try that again.";

    }

    public void Blank()
    {
        attemptsTxt.text = "";
        timerTxt.text= "";
        livesTXT.text = "";
        textTxt.text = "Dario: Welcome to my kitchen.";

    }

    public void UpdateStageThreeUI(int currentLives, int curr, int max)
    {
        livesTXT.text = "Lives:" + currentLives;
        attemptsTxt.text = "Plates:"+ curr + "/" + max;
        timerTxt.text= "";
        
    }

}
