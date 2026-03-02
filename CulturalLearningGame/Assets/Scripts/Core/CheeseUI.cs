using UnityEngine;

public class CheeseUI : MonoBehaviour
{
    public GameObject[] cheeseIcons; 

    void Update()
    {
        int remaining = DeliveryMiniGame.Instance.cheeseRemaining;

        for(int i = 0; i < cheeseIcons.Length; i++)
        {
            cheeseIcons[i].SetActive(i < remaining);
        }
    }
}
