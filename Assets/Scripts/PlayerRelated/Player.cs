using UnityEngine;
using System.Collections;

public class Player{
    private Hexagon currentHex;
    private Color playerColor;

    public Player()
    {
        RandomPlayerColor();
    }

    private void RandomPlayerColor()
    {
        playerColor = new Color(Random.Range(0.0f, 1.0f), 0.5f, 0.5f);
    }

    public void MovePlayer(Hexagon newHex)
    {
        if (currentHex != null)
            ReturnPreviousHexToDefaultColor();
        currentHex = newHex;
        ChangeHexColorToPlayerColor();
    }

    private void ChangeHexColorToPlayerColor()
    {
        currentHex.SetColor(playerColor);
    }

    private void ReturnPreviousHexToDefaultColor()
    {
        currentHex.RestoreDefaultColor();
    }

    public Vector2 GetCurrentPosition()
    {
        return currentHex.fixedPosition;
    }
}
