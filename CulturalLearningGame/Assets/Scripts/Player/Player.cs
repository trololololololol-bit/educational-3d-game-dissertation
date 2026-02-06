using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour


{


    [SerializeField] private float moveSpeed = 8f;
    private bool isWalking;


    private void Update() { // runs code on every single frame
        Vector2 inputVector = new Vector2(0,0);

        if (Keyboard.current.wKey.isPressed){
            Debug.Log("Pressing W");
            inputVector.y = +1;
        }
        if (Keyboard.current.aKey.isPressed){
            Debug.Log("Pressing A");
            inputVector.x = -1;
        }
        if (Keyboard.current.sKey.isPressed){
            Debug.Log("Pressing S");
            inputVector.y = -1;
        }
        if (Keyboard.current.dKey.isPressed){
            Debug.Log("Pressing D");
            inputVector.x = +1;
        }
        // to normalise input vector so that amount of keys pressed wont have an effect
        inputVector = inputVector.normalized;


        // add input vector to transformed position
        // transofrm.position is vector 3 so convert input vector
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float playerSize = .7f;
        float playerHeight = 2f;
        float moveDistance = moveSpeed * Time.deltaTime;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerSize, moveDir, moveDistance);

        if (canMove) {
        transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;
        
        // (s)lerp smooth movement by interpolating between values
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
       



        Debug.Log(inputVector);
        }
       
    
    


    public bool IsWalking() {
        return isWalking;
    }
}


