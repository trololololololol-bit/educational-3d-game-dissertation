using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class RecallMiniGame : MonoBehaviour
{
    [Header("Game Setup")]
    [SerializeField] private int numRows = 3;
    [SerializeField] private int numCols = 3;
    private int numTiles;
    private Tile[]  tile;


    [Header("Game Objects")]
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform gameArea;

    void Awake()
    {
    Instance = this;
    }

    public static RecallMiniGame Instance;
    private int[] sequence;
    private int playerIndex = 0;
    private bool playerTurn = false;




    [Header("Audio Setup")]
    [SerializeField] private float duration = 0.2f;


   

    public void StartTask()
{
    Debug.Log("Recall system running");
    StartCoroutine(PlaySequence());
}

    void Start()
    {
        numTiles = numRows * numCols;
        tile = new Tile[numTiles];


        // Create grid of tiles
        for (int row = 0; row < numRows; row++)
        {
            for(int col = 0; col < numCols; col++)
            {
                int index = (row * numCols) + col;




                // instantiate tile prefab
                tile[index] = Instantiate(tilePrefab, gameArea);
                tile[index].Init(this, index, Color.HSVToRGB((float)index / numTiles, 0.8f, 0.9f));


                // position tiles in centre of game area
                float rowStart = (numRows / 2.0f) - 0.5f;
                float colStart = (-numCols / 2.0f) + 0.5f;
                tile[index].transform.localPosition = new Vector3(colStart + col, rowStart - row, 0f);
            }
        }


        // scale tiles to fit space


        float scale = 6f / numRows;
        gameArea.localScale = Vector3.one * scale;
    }

    private IEnumerator PlaySequence()
{
    playerTurn = false;
    playerIndex = 0;

    sequence = new int[3];

    for (int i = 0; i < sequence.Length; i++)
    {
        sequence[i] = Random.Range(0, numTiles);
    }

    yield return new WaitForSeconds(0.5f);

    foreach (int index in sequence)
    {
        PlayLightAndTone(index);
        yield return new WaitForSeconds(0.6f);
    }

    Debug.Log("Player turn");
    playerTurn = true;
}

    public void PlayerSelect(int index)
{
    if (!playerTurn) return;

    if (index == sequence[playerIndex])
    {
        Debug.Log("Correct");
        playerIndex++;

        if (playerIndex >= sequence.Length)
        {
            Debug.Log("Sequence complete!");
            playerTurn = false;
        }
    }
    else
    {
        Debug.Log("Wrong!");
        playerTurn = false;
    }
}


    private IEnumerator FlashTile(int index)
    {
        tile[index].TurnOn();
        yield return new WaitForSeconds(duration);
        tile[index].TurnOff();
    }
 

    public void PlayLightAndTone(int index)
    {
        StartCoroutine(FlashTile(index));
    }
  
 

    
}



