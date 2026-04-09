using UnityEngine;

public class DecoratingSpot : MonoBehaviour
{

    public GameObject undecoratedLampost;
    public GameObject decoratedLampostPrefab;

    public AudioSource audioSource;
    public AudioClip ruffleSound;
    public AudioClip popSound;

    private bool isDecorated = false;
    private GameObject decoratedInst;



    public void Decorate()
    {
        if (isDecorated) return;

        if (audioSource != null && ruffleSound != null)
            audioSource.PlayOneShot(ruffleSound);

        // sound delay
        Invoke(nameof(ActivateDecorated), 0.3f);
    }

    void ActivateDecorated()
    {
        if (audioSource != null && popSound != null)
            audioSource.PlayOneShot(popSound);

        if (undecoratedLampost != null)
            undecoratedLampost.SetActive(false);

        if (decoratedLampostPrefab != null)
        {
            decoratedInst = Instantiate(decoratedLampostPrefab, undecoratedLampost.transform.position,
            undecoratedLampost.transform.rotation,
            undecoratedLampost.transform.parent);
           
        }

        isDecorated = true;
        GameManager.Instance.decorationsCompleted++;

        if(GameManager.Instance.decorationsCompleted >= GameManager.Instance.totalDecorations)
        {
            DecoratingMiniGame.Instance.CompleteTask();
        }


    }

    public bool IsDecorated() => isDecorated;


}
