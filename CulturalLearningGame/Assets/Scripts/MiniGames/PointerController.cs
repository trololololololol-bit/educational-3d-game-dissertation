using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PointerController : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public RectTransform zonePointA;
    public RectTransform zonePointB;
    public RectTransform safeZone;
    public float zoneSpeed =100f;
    public float zoneDirection = 1;
    public float moveSpeed = 200f;
    public float zoneMoveSpeed = 200f;
    public int cookGoal = 20;
    public int cookProgress = 0;

    private float direction = 1f;
    private RectTransform pointerTransform;
    private Vector3 targetPos; 
    private Vector2 zoneTargetPos; 
    private float zonedirection = 1f;
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
        
    }

    void Update()
    {
        
        if(stage==2)
        {
            stageTimer += Time.deltaTime;
            PointerMovementHandler();
            RandomZoneHandler();
            CookingInputHandler();
            float timeLeft = stageDuration - stageTimer;
            PrecisionUI.Instance.UpdateTimer(timeLeft);
            PrecisionUI.Instance.UpdateLives(currentLives);
            PrecisionUI.Instance.UpdateProgress(cookProgress, cookGoal);

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
            moveSpeed +=180f;
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
        stageDuration =  60f;
        moveSpeed =200f;
        zoneMoveSpeed = 250f;
        zoneTargetPos = zonePointB.anchoredPosition;
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
        if(!Keyboard.current.spaceKey.wasPressedThisFrame)return;
        if (RectTransformUtility.RectangleContainsScreenPoint(
        safeZone, pointerTransform.position, null))
    {
        Debug.Log("Cook success tick");
        cookProgress++;
        PrecisionUI.Instance.UpdateProgress(cookProgress, cookGoal);
        if(cookProgress >= cookGoal)
            {
                CookingComplete();
            }

    }
    else
    {
        currentLives--;
        PrecisionUI.Instance.UpdateLives(currentLives);

        if (currentLives <= 0 || stageTimer <= stageDuration && cookProgress > cookGoal)
        {
            ResetStage();
            return;
        }
    }
    }

    void RandomZoneHandler()
    {
         //moves safzone to target
        safeZone.anchoredPosition = Vector2.MoveTowards(safeZone.anchoredPosition, zoneTargetPos, zoneMoveSpeed * Time.deltaTime);
        //change dir if meets one of points 
        // if dist between pointer and point a is less than 0.1f, change dir
        if(Vector2.Distance(safeZone.anchoredPosition, zonePointA.anchoredPosition)<0.1f)
        {
            zoneTargetPos = zonePointB.anchoredPosition;
            zonedirection = 1f;
        } else if(Vector2.Distance(safeZone.anchoredPosition, zonePointB.anchoredPosition)<0.1f)
        {
            zoneTargetPos = zonePointA.anchoredPosition;
            zonedirection = -1f;
        }
    }
    void PointerMovementHandler()
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
            StartCookingStage();
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
