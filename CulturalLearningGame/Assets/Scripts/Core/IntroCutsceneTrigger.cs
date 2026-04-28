using UnityEngine;

public class IntroCutsceneTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public IntroCutscene slideshow;
   private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.name);
        if(other.CompareTag("Player") && !hasPlayed)
        {
            hasPlayed = true;
            slideshow.StartSlideshow();
        }
    }


}
