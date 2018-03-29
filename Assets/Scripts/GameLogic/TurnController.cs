using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TurnController : NetworkBehaviour {
    private SyncList<TurnChangeListener> turnChangeListeners;
    [SyncVar]
    public int playerTurnIndex;

    void Start()
    {

    }

    void Update()
    {
        if(isServer)
            isTurnDone();
    }

    [Server]
    private void isTurnDone()
    {

    }

    [Server]
    public void SkipTurn(NetworkInstanceId playerNetId)
    {
        if(LocalDataManager.isNetIdEqual(LocalDataManager.GetPlayersGameObjects()[playerTurnIndex], playerNetId))
        {
            Debug.Log("Valid player requested to end his turn.");
        }
        else
        {
            Debug.Log("Not valid player requested to end his turn");
        }
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
}
