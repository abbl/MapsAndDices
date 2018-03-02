using UnityEngine;
using System.Collections;

public class GameCordinator : MonoBehaviour {
    private HexagonMapGenerator hexagonMapGenerator;
    private Player[] players;
    private Player playerTurn;
    private Timer timer;
    public float turnTime;

    // Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("numberOfPlayers", 4);
        timer = GameObject.Find("TurnTimer").GetComponent<Timer>();
        hexagonMapGenerator = GameObject.Find("HexagonMap").GetComponent<HexagonMapGenerator>();
        SpawnPlayersOnMap();
	}
	
    private void SpawnPlayersOnMap()
    {
        players = new Player[PlayerPrefs.GetInt("numberOfPlayers")];
        for(int i = 0; i < PlayerPrefs.GetInt("numberOfPlayers"); i++)
        {
            players[i] = new Player();
            players[i].playerColor = RandomPlayerColor();
            RandomPlayerPosition(players[i]);
        }
    }

    private Color RandomPlayerColor()
    {
        return new Color(RandomColor(), RandomColor(), RandomColor(), 1);
    }

    private float RandomColor()
    {
        return Random.Range(0.0f, 1.0f);
    }

    private void RandomPlayerPosition(Player player)
    {
        Vector2 randomFixedPosition = new Vector2(Random.Range(1, PlayerPrefs.GetInt("mapColumns")), Random.Range(1, PlayerPrefs.GetInt("mapRows")));
        Hexagon hexagon = hexagonMapGenerator.GetHexagon(randomFixedPosition);
        if (hexagon != null)
        {
            player.AddHexagon(hexagon.gameObject);
        }
    }

	// Update is called once per frame
	void Update () {
        ControlPlayerTurn();
	}

    private void ControlPlayerTurn()
    {
        if(playerTurn == null)
        {
            playerTurn = players[0];
            timer.StartTimer(turnTime);
        }
        if (timer.isCountingDone())
            NextTurn();
    }

    private void NextTurn()
    {
        int playerIndex = GetPlayerIndexFromArray(playerTurn);

        if (playerIndex != -1)
        {
            Debug.Log("Index now:" + playerIndex + "Next index:" + (playerIndex + 1) + "Length" + players.Length);
            if(playerIndex + 1 > players.Length - 1)
            {
                playerTurn = players[0];
                playerIndex = 0;
            }
            else
            {
                playerTurn = players[playerIndex += 1];
            }
            timer.StartTimer(turnTime);
            Debug.Log("Player Turn changed to:" + playerIndex);
        }
    }

    private int GetPlayerIndexFromArray(Player player)
    {
        for(int i = 0; i < players.Length; i++)
        {
            if(players[i] == player)
            {
                return i;
            }
        }
        return -1;
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
        NextTurn();
    }
}
