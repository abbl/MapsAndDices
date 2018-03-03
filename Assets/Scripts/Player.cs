using UnityEngine;
using System.Collections;

public class Player{
    public Color playerColor { get; set; }
    public string playerName { get; set; }
    private ArrayList playerHexagons;
    private static int index = 1;

	public Player()
    {
        playerHexagons = new ArrayList();
        playerName = "Player" + index;
        index++;
    }

    public void AddHexagon(GameObject hexagon)
    {
        if (hexagon.CompareTag("Hexagon"))
        {
            Hexagon hexagonComponent = hexagon.GetComponent<Hexagon>();
            hexagonComponent.SetColor(playerColor);
            playerHexagons.Add(hexagon);
        }
    }

    public void RemoveHexagon(GameObject hexagon)
    {
        if (hexagon.CompareTag("Hexagon"))
        {
            Hexagon hexagonComponent = hexagon.GetComponent<Hexagon>();
            hexagonComponent.RestoreDefaultColor();
            playerHexagons.Remove(hexagon);
        }
    }

    public bool doesPlayerOwnThisHexagon(GameObject hexagon)
    {
        return playerHexagons.Contains(hexagon);
    }

    public bool doesPlayerOwnThisHexagon(Vector2 fixedPosition)
    {
        foreach(GameObject gameObject in playerHexagons)
        {
            Hexagon hexagon = gameObject.GetComponent<Hexagon>();
            if (hexagon.isPositionEqual(fixedPosition))
            {
                return true;
            }
        }
        return false;
    }
}
