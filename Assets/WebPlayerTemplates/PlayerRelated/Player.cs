using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour{
    private Hexagon currentHex;
    private Color playerColor;

    void Start()
    {
        RandomPlayerColor();
    }

    private void RandomPlayerColor()
    {
        playerColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        GetComponent<SpriteRenderer>().color = playerColor;
    }

    public void MovePlayer(Hexagon newHex)
    {
        currentHex = newHex;
        gameObject.transform.position = new Vector3(currentHex.transform.position.x, currentHex.transform.position.y, currentHex.transform.position.z - 2);
    }

    private void ReturnPreviousHexToDefaultColor()
    {
        currentHex.RestoreDefaultColor();
    }

    public Vector2 GetCurrentPosition()
    {
        return currentHex.fixedPosition;
    }

    public Color GetPlayerColor()
    {
        return playerColor;
    }

    public Transform GetPlayerTransform()
    {
        return currentHex.transform;
    }

    public void TurnOnPlayerLantern()
    {
        GetComponentInChildren<Light>().enabled = true;
    }

    public void TurnOffPlayerLantern()
    {
        GetComponentInChildren<Light>().enabled = false;
    }
}
