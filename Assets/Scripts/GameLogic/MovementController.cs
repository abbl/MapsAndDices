using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MovementController : TurnChangeListener {
    [SyncVar]
    private bool isPlayerAbleToMove;

    private void Start()
    {
        isPlayerAbleToMove = true;
    }

    [Server]
    public void ReceiveMoveRequest(Vector2 fixedPosition, NetworkInstanceId playerNetId)
    {
        if (isPlayerAbleToMove)
        {

        }
    }

    [Server]
    public override void NextTurn()
    {
        isPlayerAbleToMove = true;
    }
}
