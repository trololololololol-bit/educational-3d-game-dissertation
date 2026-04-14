using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PrecisionUI : MonoBehaviour
{
    public GameObject bgPanel;
    public GameObject textPanel;
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
    public Sprite[] rawIngredients;
    public Sprite[] choppedIngredients;

    void Awake()
    {
        Instance = this;
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
    public void StageThree()
    {
        //serve 
        textTxt.text = "Dario: This is serving stage. Make the food look as presentable as possible...";
        attemptsTxt.text = "Chops: /5";
    }

    public void UpdateProgress(int current, int max)
    {
        attemptsTxt.text = "Pieces collected:"+current + "/" + max;
        
    }

    public void FlashFail()
    {
        Debug.Log("flashred");
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

}
