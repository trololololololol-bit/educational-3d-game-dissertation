using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using TMPro;
using UnityEditor.ShaderGraph;

public class FishingSpot : MonoBehaviour
{
   public GameObject unfishedSpot;
    public GameObject fishedSpot;

    public AudioSource audioSource;
    public AudioClip waterSound;
    public AudioClip catchSound;

    private bool isFished = false;
    private GameObject fishedInst;
    private bool canCatch = false; //playercancatch
    private bool fishRecieved = false; //player actually caught
    public GameObject panel;
    public TextMeshProUGUI interactFishTxt;

    public UnityEngine.UI.Image panelImage;
    public Color readyColor = Color.blue;
    public Color caughtColor = Color.green;
    public Color failedColor = Color.red;



    public void Fish()
    {
        if (isFished) return;
        StartCoroutine(FishingSequence());
        panel.SetActive(false);


    }

    IEnumerator FishingSequence()
    {
        panel.SetActive(false);
        //play water effect
        if (audioSource != null && waterSound != null)
            audioSource.PlayOneShot(waterSound);

        //rand bite time
        float randWait = Random.Range(1.3f,4f);
        yield return new WaitForSeconds(randWait);

        panel.SetActive(true);
        panelImage.color = readyColor;
        interactFishTxt.text = "FISH NOW";
       
        Debug.Log("Press now");

        float catchLimit = 0.8f;
        float timer = 0f;

        canCatch = true;
        fishRecieved = false;

        while (timer < catchLimit)
        {
            timer+=Time.deltaTime;

            if(fishRecieved)
            {
                SuccessfulCatch();
                yield break;
            } yield return null;
        } FailedCatch();

    
    }
    public void RegisterInput()
    {
        if(canCatch)
        {
            fishRecieved = true;
        }
    }

    void SuccessfulCatch()
    {
        canCatch = false;
        Debug.Log("Caught");
        panelImage.color = caughtColor;
         interactFishTxt.text = "CAUGHT";
        ActivateFishedSpot();
        FishingMiniGame.Instance.Clear();
    }

    void FailedCatch()
    {
        canCatch = false;
        panelImage.color = failedColor;
        interactFishTxt.text = "FAILED - TRY AGAIN";
        Debug.Log("Failed Catch, try again");
        FishingMiniGame.Instance.Clear();
    }

    void ActivateFishedSpot()
    {
        //catch sound
        if (audioSource != null && catchSound != null)
            audioSource.PlayOneShot(catchSound);

        if (unfishedSpot != null)
            unfishedSpot.SetActive(false);

       

        isFished = true;
        GameManager.Instance.fishCaught++;

        if(GameManager.Instance.fishCaught >= GameManager.Instance.totalFish)
        {
            panel.SetActive(false);
            FishingMiniGame.Instance.CompleteTask();
        }


    }

    public bool IsFished() => isFished;

}
