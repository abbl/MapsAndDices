using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TurnController : NetworkBehaviour {
    private ArrayList turnChangeListeners;
    private NetworkedTimer turnTimer;
    private int timerLastValue;

    public int turnTime;
    [SyncVar]
    public int playerTurnIndex = -1;

    
    void Start()
    {
        turnChangeListeners = new ArrayList();
        turnTimer = GameObject.Find("TurnNetworkedTimer").GetComponent<NetworkedTimer>();

        if (isServer)
        {            
            EndTurn();
        }
    }

    void Update()
    {
        if (isServer)
        {
            isTurnDone();
        }
    }

    [Server]
    private void isTurnDone()
    {
        if(turnTimer != null)
            if (turnTimer.IsCountingDone())
            {
                EndTurn();
            }
    }

    [Server]
    public void SkipTurn(NetworkInstanceId playerNetId)
    {
        if(LocalDataManager.isNetIdEqual(LocalDataManager.GetPlayersGameObjects()[playerTurnIndex], playerNetId))
        {
            EndTurn();
        }
        else
        {
            Debug.Log("Not valid player requested to end his turn");
        }
    }

    [Server]
    private void EndTurn()
    {
        int playersInGame = LocalDataManager.GetAllPlayers().Length - 1; //Changing starting index to 0;

        if (++playerTurnIndex > playersInGame)
        {
            playerTurnIndex = 0;
        }
        turnTimer.StartTimer(turnTime);
        NotifyListenersAboutNextTurn();
    }

    private void NotifyListenersAboutNextTurn()
    {
        foreach(TurnChangeListener listener in turnChangeListeners)
        {
            listener.NextTurn();
        }
    }

    public void AddChangeListener(TurnChangeListener listener)
    {
        turnChangeListeners.Add(listener);
    }

    public int GetTurnTimerCount()
    {
        return turnTimer.GetTimeLeft();
    }

    public int GetPlayerIndex()
    {
        return playerTurnIndex;
    }
}
