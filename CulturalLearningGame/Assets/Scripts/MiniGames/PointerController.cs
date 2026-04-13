using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PointerController : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public RectTransform safeZone;
    public float zoneSpeed =100f;
    public float zoneDirection = 1;
    public float moveSpeed = 200f;

    private float direction = 1f;
    private RectTransform pointerTransform;
    private Vector3 targetPos; 
    private int stage = 1;
    private int perRound = 0;
    private int maxPerRound = 5;
    private int totalIngredients = 3;
    private int currentIngredient = 0;
    private int maxLives = 3;
    private int currentLives = 3;
    public bool canClick =  true;

    private float stageTimer = 0f;

    private float stageDuration = 30f;
    private float zoneChangeTimer = 0f;

    void Start()
    {
        pointerTransform = GetComponent<RectTransform>();
        targetPos = pointB.position;
    }
    void OnEnable()
    {
        stage = 1;
        currentIngredient = 0;
        StartIngredient();
    }

    void StartIngredient()
    {
        canClick = true;
        perRound = 0;
        currentLives = maxLives;
        moveSpeed = 200f;

        PrecisionUI.Instance.ShowRawUnchopped(currentIngredient);
        Debug.Log("Ingredient" + (currentIngredient +1));
        PrecisionUI.Instance.StageOne();
        PrecisionUI.Instance.UpdateProgress(perRound, maxPerRound);
    }

    void Update()
    {
        
        if(stage==2)
        {
            stageTimer += Time.deltaTime;
            //PointerMovementHandler();
            
            RandomZoneHandler();
            CookingInputHandler();
           

            if(stageTimer>=stageDuration)
            {
                CookingComplete();
            }
        }else
        {
            MovePointer();
        }

         if(canClick && Keyboard.current.spaceKey.wasPressedThisFrame) {
            CheckSuccess();
        }
    }

    void MovePointer()
    {
         //moves pointer to target
        pointerTransform.position = Vector3.MoveTowards(pointerTransform.position, targetPos, moveSpeed * Time.deltaTime);
        //change dir if meets one of points 
        // if dist between pointer and point a is less than 0.1f, change dir
        if(Vector3.Distance(pointerTransform.position, pointA.position)<0.1f)
        {
            targetPos = pointB.position;
            direction = 1f;
        } else if(Vector3.Distance(pointerTransform.position, pointB.position)<0.1f)
        {
            targetPos = pointA.position;
            direction = -1f;
        }

    }

    void CheckSuccess()
    {
        if(RectTransformUtility.RectangleContainsScreenPoint(safeZone,pointerTransform.position, null))
        {
            Debug.Log("Success!");
            perRound ++;
            moveSpeed +=60f;
            PrecisionUI.Instance.UpdateProgress(perRound, maxPerRound);
            PrecisionUI.Instance.UpdateLives(currentLives);

            if(perRound >= maxPerRound)
            {
                IngredientComplete();
            } 
        }else
            {
                currentLives --;
                PrecisionUI.Instance.UpdateLives(currentLives);
                PrecisionUI.Instance.FlashFail();

                if(currentLives <= 0)
            {
                RestartIngredient();
            }
            }
    }


    void IngredientComplete()
    {
        Debug.Log("Ingredient Complete");
        PrecisionUI.Instance.ShowChopped(currentIngredient);
        Invoke("NextIngredient", 2f);
        canClick =  false;
        
    }

    void NextIngredient()
    {
        currentIngredient ++;
        if (currentIngredient >= totalIngredients)
        {
            Debug.Log("Stage 1 completed");
            NextStage();
            return;
        }
        StartIngredient();
    }

    void RestartIngredient()
    {
        Debug.Log("Restarting Ingredient");
        StartIngredient();
    }

    void StartCookingStage()
    {
        currentLives = 3;
        
        stageTimer =  0f;
        stageDuration =  30f;
        moveSpeed = 250f;
        PrecisionUI.Instance.StageTwo();
    }


    void CookingComplete()
    {
        Debug.Log("Cooking was successful");
        //PrecisionUI.Instance.ShowCooked();
        NextStage();
    }

    void CookingInputHandler()
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(
        safeZone, pointerTransform.position, null))
    {
        Debug.Log("Cook success tick");
    }
    else
    {
        currentLives--;
        PrecisionUI.Instance.UpdateLives(currentLives);

        if (currentLives <= 0)
        {
            ResetStage();
        }
    }
    }

    void RandomZoneHandler()
    {
        safeZone.anchoredPosition += new Vector2(zoneSpeed * zoneDirection * Time.deltaTime, 0);

    if (safeZone.anchoredPosition.x > 200)
        zoneDirection = -1;

    if (safeZone.anchoredPosition.x < -200)
        zoneDirection = 1;
    }
    void PointerMovementHandler()
    {
        
    }

    void CookingVisual()
    {
        if(currentLives > 1)
        {
            //PrecisionUI.Instance.ShowCooked();
        } else
        {
            //PrecisionUI.Instance.ShowBurned();
        }
    }



    void NextStage()
    {
        stage ++;
      
        Debug.Log("stage" + stage);

        if(stage == 2)
        {
            StartCookingStage();
        } else if (stage == 3)
        {
            PrecisionUI.Instance.StageThree();
            //StartPlatingStage();
        } else
        {
            FinishGame();
        }
    }

    void ResetStage()
    {
        Debug.Log("Restarting Stage");
        

        if(stage ==1 ) 
        {
         PrecisionUI.Instance.StageOne();
        } else if(stage == 2)
        {
            PrecisionUI.Instance.StageTwo();
        } else if(stage ==3)
        {
            PrecisionUI.Instance.StageThree();
        }

    }

   
    void FinishGame()
    {
        Debug.Log("Dish created");
    }



}
