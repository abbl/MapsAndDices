using UnityEngine;
using System.Collections;

public class GameCordinator : MonoBehaviour {
    private Player[] players;
    private Player playerTurn;
	
    // Use this for initialization
	void Start () {
        PlayerPrefs.SetFloat("numberOfPlayers", 4);
        SpawnPlayersOnMap();
	}
	
    private void SpawnPlayersOnMap()
    {
        playerTurn = new Player();
        playerTurn.playerColor = new Color(1, 0, 1, 1);
    }

	// Update is called once per frame
	void Update () {
	
	}

    public void receivePlayerHexagonClick(Hexagon hexagon)
    {
        if (!playerTurn.doesPlayerOwnThisHexagon(hexagon.gameObject))
        {
            playerTurn.AddHexagon(hexagon.gameObject);
        }
        else
        {
            playerTurn.RemoveHexagon(hexagon.gameObject);
        }
    }
}
