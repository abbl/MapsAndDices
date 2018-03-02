using UnityEngine;
using System.Collections;

public class Player{
    public Color playerColor { get; set; }
    private ArrayList playerHexagons;

	public Player()
    {
        playerHexagons = new ArrayList();
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
}
