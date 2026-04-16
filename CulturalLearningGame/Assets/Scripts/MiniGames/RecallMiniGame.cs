using UnityEngine;
using System.Collections.Generic;
using UnityEditor;


public class RecallMiniGame : MonoBehaviour
{
    private int totalRounds = 6;
    private int round = 0;

    private bool canClick = false;

    public List<int> sequence = new List<int>();
    private int currInput;


    public void StartGame()
    {
        round =1; 
        sequence.Clear();
        AddToSequence();
    }

    void AddToSequence()
    {
       // StartCoroutine(PlaySequence());
    }
}
