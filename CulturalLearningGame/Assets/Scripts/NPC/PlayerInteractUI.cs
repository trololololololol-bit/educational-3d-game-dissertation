using System.ComponentModel;
using UnityEngine;

public class PlayerInteractUI : MonoBehaviour
{
 [SerializeField] private GameObject containerGameObject;
 [SerializeField] private PlayerInteractor playerInteract;

    private void Update()
    {
        if(playerInteract.GetInteractableObject() != null)
        {
            Show();
        } else
        {
            Hide();
        }
    }

    private void Show()
    {
        containerGameObject.SetActive(true);
    }

 private void Hide()
    {
        containerGameObject.SetActive(false);
    }
}
