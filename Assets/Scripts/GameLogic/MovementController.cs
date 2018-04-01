using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MovementController : TurnChangeListener {
    [Server]
    public void ReceiveMoveRequest(Vector2 fixedPosition, NetworkInstanceId playerNetId)
    {

    }

    public override void NextTurn()
    {
        
    }
}
