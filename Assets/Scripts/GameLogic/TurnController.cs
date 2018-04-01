using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TurnController : NetworkBehaviour {
    private static ArrayList turnChangeListeners = new ArrayList();
    private NetworkedTimer turnTimer;
    private int timerLastValue;

    public int turnTime;
    [SyncVar]
    public int playerTurnIndex = -1;

    
    void Start()
    {
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
            IsTurnDone();
        }
    }

    [Server]
    private void IsTurnDone()
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
        if(LocalDataManager.IsNetIdEqual(LocalDataManager.GetPlayersGameObjects()[playerTurnIndex], playerNetId))
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

    public static void AddChangeListener(TurnChangeListener listener)
    {
        turnChangeListeners.Add(listener);
    }

    public int GetTurnTimerCount()
    {
        if(turnTimer != null)
            return turnTimer.GetTimeLeft();
        return -1;
    }

    public int GetPlayerIndex()
    {
        return playerTurnIndex;
    }

    public bool DoesThisNetIdMatchCurrentTurn(NetworkInstanceId playerNetId)
    {
        return LocalDataManager.IsNetIdEqual(LocalDataManager.GetPlayersGameObjects()[playerTurnIndex], playerNetId);
    }
}
