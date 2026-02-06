using UnityEngine;


public class Tile : MonoBehaviour
{
    private RecallMiniGame recallManager;
    private SpriteRenderer spriteRenderer; // alternate colour of tile to light it up
    private int tileId; // state of which tile
    private Color colour;




    // initialise
    public void Init(RecallMiniGame recallManager, int tileId, Color colour)
    {
        // map past variables to local ones
        this.recallManager = recallManager;
        this.tileId = tileId;
        this.colour = colour;
        spriteRenderer = GetComponent<SpriteRenderer>();


        // so the tile starts off
        TurnOff();


    }


    public void TurnOff()
    {
        // darken original colour to inidicate that it is off
        spriteRenderer.color = colour * 0.3f;
    }


    // public void so can be called from recall manager
    public void TurnOn()
    {
        spriteRenderer.color = colour;
    }


    private void OnMouseDown()
    {
        // calls function on teh recall manager
        // turn light on, play audio, checks if it is correct or not


        recallManager.PlayLightAndTone(tileId);
    }


}
