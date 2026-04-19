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
    public int cookGoal = 30;
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
    private int plateProgress = 0;
    private int plateGoal = 10;
 

    private float stageTimer = 0f;

    private float stageDuration = 30f;
    private float zoneChangeTimer = 0f;

    bool zoneActive = false;
    float zoneTimer = 0f;
    float zoneInterval = 2f;
    float zoneDuration = 1f;


    public AudioSource audioSource;
    public AudioClip chopSound;
    public AudioClip successDing;
    public AudioClip failSound;
    public AudioClip sizzle;
    public AudioClip plate;
    public AudioClip grate;

    void Start()
    {
        pointerTransform = GetComponent<RectTransform>();
        targetPos = pointB.position;
        
        
        
    }
    void OnEnable()
    {
        stage = 1;
        currentIngredient = 0;
        PrecisionUI.Instance.Blank();
        StartIngredient();
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
            PrecisionUI.Instance.UpdateFlips(cookProgress, cookGoal);

            if(stageTimer>=stageDuration)
            {
                CookingComplete();
            }
        }else if(stage ==3){
            zoneTimer += Time.deltaTime;

            if(!zoneActive && zoneTimer >= zoneInterval)
                {
                    ActivateZone();
                }

            if(zoneActive && zoneTimer >= zoneDuration)
                {
                    DeactivateZone();
                }
            PlateInputHandler();
            ThreePointerHandler();
        }else
        {
            MovePointer();
        }

         if(canClick && Keyboard.current.spaceKey.wasPressedThisFrame) {
            CheckSuccess();
            PlaySound(chopSound);
        }
    }

    // STAGE 1 ---------------------------------------------------------------

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
                PlaySound(failSound);
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
        PlaySound(successDing);
        Invoke("NextIngredient", 2f);
        canClick =  false;
        
    }

    void NextIngredient()
    {
        currentIngredient ++;
        if (currentIngredient >= totalIngredients)
        {
            Debug.Log("Stage 1 completed");
            Invoke("NextStage",2f);
            
            return;
        }
        Invoke("StartIngredient",2f);
    }

    void RestartIngredient()
    {
        Debug.Log("Restarting Ingredient");
        currentIngredient =0;
        currentLives =3;
        PrecisionUI.Instance.TryAgain();
        Invoke("StartIngredient",2f);
        
    }



    // STAGE 2 ---------------------------------------------------------------

    void StartCookingStage()
    {
        currentLives = 3;
        
        stageTimer =  0f;
        stageDuration =  60f;
        moveSpeed =200f;
        zoneMoveSpeed = 250f;
        cookProgress = 0;
        zoneTargetPos = zonePointB.anchoredPosition;
        PrecisionUI.Instance.StageTwo();
        PrecisionUI.Instance.UpdateFlips(cookProgress,cookGoal);
         PrecisionUI.Instance.ShowCooked(0);
         PlaySound(sizzle);
    }


    void CookingComplete()
    {
        Debug.Log("Cooking was successful");
        
        PrecisionUI.Instance.ShowCooked(0);
        PrecisionUI.Instance.OntoStageThree();
        Invoke("NextStage", 3f);
    }

    void CookingInputHandler()
    {
        if(!Keyboard.current.spaceKey.wasPressedThisFrame)return;
        if (RectTransformUtility.RectangleContainsScreenPoint(
        safeZone, pointerTransform.position, null))
    {
        Debug.Log("Cook success tick");
        cookProgress++;
        zoneMoveSpeed +=15f;
        
        PrecisionUI.Instance.UpdateFlips(cookProgress, cookGoal);
        if(cookProgress >= cookGoal)
            {
                CookingComplete();
            }

    }
    else
    {
        currentLives--;
        PlaySound(failSound);
        PrecisionUI.Instance.FlashFail();
        PrecisionUI.Instance.UpdateLives(currentLives);
        if(currentLives <=2) {
                 PrecisionUI.Instance.ShowCooked(1);
                 PrecisionUI.Instance.BurnWarn();
// gain life possibility
            }

        if (currentLives <= 0 || cookProgress > cookGoal)
        {
            
            PrecisionUI.Instance.ShowCooked(2);
            PrecisionUI.Instance.BurnedText();
            Invoke("ResetStage", 3f);
    
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




    
    // STAGE 3 ---------------------------------------------------------------

    void StartPlatingStage()
    {
        moveSpeed = 300f;
        PrecisionUI.Instance.StageThree();
        currentLives = 3;
        plateProgress = 0;
        plateGoal = 10;
        zoneActive = false;
        zoneTimer = 0f;
        PrecisionUI.Instance.UpdateStageThreeUI(currentLives, plateProgress, plateGoal);
       
        
    }

    void ActivateZone()
    {
        zoneActive = true;
        zoneTimer = 0f;
        safeZone.gameObject.SetActive(true);
        PlaySound(plate);
        

        float minSpot =zonePointA.anchoredPosition.x;
        float maxSpot =zonePointB.anchoredPosition.x;
        float randomSpot = Random.Range(minSpot, maxSpot);
        safeZone.anchoredPosition = new Vector2(randomSpot, safeZone.anchoredPosition.y);

        // rand width
        float randWidth = Random.Range(80f,200f);
        safeZone.sizeDelta = new Vector2(randWidth,safeZone.sizeDelta.y);
    }

    
    void DeactivateZone()
    {
       
        zoneActive = false;
        zoneTimer = 0f;
        safeZone.gameObject.SetActive(false);
        zoneInterval = Random.Range(0.5f, 3f);
    }

    void ThreePointerHandler()
    {
        //moves pointer to target
        moveSpeed = 350f;
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

    void PlateInputHandler()
    {
        if(!Keyboard.current.spaceKey.wasPressedThisFrame) return;
        if(!zoneActive) return;
        
        
            if(zoneActive && RectTransformUtility.RectangleContainsScreenPoint(
             safeZone, pointerTransform.position, null))
            {
                plateProgress ++;
                PlaySound(successDing);
                PrecisionUI.Instance.UpdateStageThreeUI(currentLives,plateProgress, plateGoal);

                if(plateProgress >=plateGoal)
                {
                    PrecisionUI.Instance.EndingTask();
                    PlaySound(successDing);
                    NextStage();
                } 
                DeactivateZone();
            } else
            {
                currentLives --;
                PrecisionUI.Instance.FlashFail();
                PrecisionUI.Instance.UpdateLives(currentLives);
                PlaySound(failSound);

                if(currentLives <= 0 )
                {
                    PrecisionUI.Instance.TryAgain();
                    Invoke("ResetStage",3f);
                }
            }
        }
    




















    //---------------------------------------------------------------


    void NextStage()
    {
        
        stage ++;
        Debug.Log("stage" + stage);

       
        if(stage == 2)
        {
            
            StartCookingStage();
        } else if (stage == 3)
        {
            
            StartPlatingStage();
        } else
        {
            Invoke("FinishGame",3f);
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
            

        } else if(stage ==3)
        {
            StartPlatingStage();
            PrecisionUI.Instance.StageThree();
        }

    }

   
    void FinishGame()
    {

        PrecisionMiniGame.Instance.EndTask();
        PrecisionMiniGame.Instance.CompleteTask();
        Debug.Log("Dish created");

    }

    void PlaySound(AudioClip clip)
    {
    audioSource.pitch = Random.Range(0.95f, 1.05f);
    audioSource.PlayOneShot(clip);
    }



}
