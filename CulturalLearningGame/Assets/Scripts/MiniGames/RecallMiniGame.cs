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




    [Header("Audio Setup")]
    [SerializeField] private float duration = 0.2f;


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



