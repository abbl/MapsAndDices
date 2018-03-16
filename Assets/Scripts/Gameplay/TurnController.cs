using UnityEngine;
using System.Collections;

public class TurnController : MonoBehaviour {
    private ArrayList players;
    private Player turnNow;
    private Timer timer;
    public int turnTime;
    
	// Use this for initialization
	void Start () {
        InitializeTimer();
        players = GetComponent<GameController>().GetPlayers();
    }
	
    private void InitializeTimer()
    {
        GameObject timerObject = new GameObject();
        timerObject.AddComponent<Timer>();
        timer = timerObject.GetComponent<Timer>();
    }

	// Update is called once per frame
	void Update () {
        ManageTurns();
	}

    private void ManageTurns()
    {
        if(turnNow == null)
            StartFirstTurn();
        if (timer.IsCountingDone())
        {
            NextTurn();
        }
    }

    private void StartFirstTurn()
    {
        turnNow = (Player)players[0];
        timer.StartTimer(turnTime);
    }

    private void NextTurn()
    {
        int playerIndex = GetPlayerIndex(turnNow);

        if (playerIndex + 1 > players.Count - 1)
        {
            turnNow = (Player)players[0];
            playerIndex = 0;
        }
        else
        {
            turnNow = (Player)players[playerIndex += 1];
        }
        timer.StartTimer(turnTime);
        GetComponent<PlayerActionController>().NextTurn();
        Debug.Log("Now {" + playerIndex + "} turn");
    }

    private int GetPlayerIndex(Player player)
    {
        for(int i = 0; i < players.Count; i++)
        {
            if(((Player) players[i]).Equals(player))
            {
                return i;
            }
        }
        return -1;
    }

    public Player GetPlayerTurn()
    {
        return turnNow;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(1760, 740, 256, 256), "Time Left: " + timer.GetTimeLeft());
        if(GUI.Button(new Rect(1760, 760, 128, 128), "Next Turn"))
        {
            NextTurn();
        }
    }
}
