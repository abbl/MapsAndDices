using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TurnController : MonoBehaviour {
    private ArrayList players;
    private Player turnNow;
    private Timer timer;
    private Text timerUI;
    private Text playerUI;
    public int turnTime;
    
	// Use this for initialization
	void Start () {
        InitializeTimer();
        players = GetComponent<GameController>().GetPlayers();
        timerUI = GameObject.Find("TurnTimePanel").GetComponentInChildren<Text>();
        playerUI = GameObject.Find("PlayerTurnPanel").GetComponentInChildren<Text>();
    }
	
    private void InitializeTimer()
    {
        GameObject timerObject = new GameObject("Timer");
        timerObject.AddComponent<Timer>();
        timer = timerObject.GetComponent<Timer>();
    }

	// Update is called once per frame
	void Update () {
        ManageTurns();
        UpdateTimerUI();
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
        MoveCameraToPlayer();
        timer.StartTimer(turnTime);
        UpdatePlayerTurnUI();
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
        GetComponent<PlayerActionController>().MakePlayerAbleToMove();
        GetComponent<DayCycleController>().NextTurn();
        MoveCameraToPlayer();
        ManagePlayerLanterns();
        UpdatePlayerTurnUI();
    }

    private void MoveCameraToPlayer()
    {
        Camera.main.transform.position = new Vector3(turnNow.GetPlayerTransform().position.x, turnNow.GetPlayerTransform().position.y, Camera.main.transform.position.z);
    }

    private void ManagePlayerLanterns()
    {
        string cycleNow = GetComponent<DayCycleController>().GetActiveCycleName();
        switch (cycleNow)
        {
            case "Evening":
                SwitchPlayerLanterns(true);
                break;
            case "Morning":
                SwitchPlayerLanterns(false);
                break;
        }
    }

    private void SwitchPlayerLanterns(bool status)
    {
        foreach(Player player in players)
        {
            if (status)
                player.TurnOnPlayerLantern();
            else
                player.TurnOffPlayerLantern();
        }
    }

    private void UpdateTimerUI()
    {
        timerUI.text = timer.GetTimeLeft().ToString();
    }

    private void UpdatePlayerTurnUI()
    {
        playerUI.text = "Player";
        playerUI.color = turnNow.GetPlayerColor();
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

    public void EndPlayerTurn()
    {
        NextTurn();
    }
}
