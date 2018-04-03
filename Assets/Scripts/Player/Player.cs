using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
    private GameObject playerChecker;
    private MovementController movementController;
    public GameObject playerCheckerPrefab;
    [SyncVar]
    public Vector2 fixedPosition;

    void Start () {
        if (isServer)
            movementController = GameObject.Find("MovementController").GetComponent<MovementController>();
        if (isLocalPlayer)
            Cmd_SpawnPlayerChecker();
	}

    [Command]
    private void Cmd_SpawnPlayerChecker()
    {
        playerChecker = Instantiate(playerCheckerPrefab);
        NetworkServer.SpawnWithClientAuthority(playerChecker, connectionToClient);
        Target_SendCheckerReferenceToPlayer(connectionToClient, playerChecker);
        movementController.MovePlayerOnSpawn(RandomPlayerPosition(), netId, connectionToClient);
        Target_FocusCameraOnChecker(connectionToClient);
    }

    [TargetRpc]
    private void Target_SendCheckerReferenceToPlayer(NetworkConnection connection, GameObject playerCheckerOnServer)
    {
        if(playerChecker == null)
        {
            playerChecker = playerCheckerOnServer;
        }
    }

    [TargetRpc]
    private void Target_FocusCameraOnChecker(NetworkConnection networkConnection)
    {
        CameraTools.FocusCameraOnGameObject(Camera.main, playerChecker);
    }

    [Server]
    private Vector2 RandomPlayerPosition()
    {
        MapGenerator mapGenerator = GameObject.Find("MapGenerator").GetComponent<MapGenerator>();
        return new Vector2(Random.Range(1, mapGenerator.columnsNumber), Random.Range(1, mapGenerator.rowsNumber));
    }

    public void SkipTurn()
    {
        if (isLocalPlayer)
            Cmd_SkipTurn(netId);
    }

    [Command]
    private void Cmd_SkipTurn(NetworkInstanceId netId)
    {
        GameObject.Find("TurnController").GetComponent<TurnController>().SkipTurn(netId);
    }
    
    public void MakeMove(Vector2 fixedPosition)
    {
        if (isLocalPlayer)
            Cmd_MakeMove(fixedPosition, netId);
    }

    [Command]
    private void Cmd_MakeMove(Vector2 fixedPosition, NetworkInstanceId localPlayerNetId)
    {
        movementController.ReceiveMoveRequest(fixedPosition, localPlayerNetId, connectionToClient);
    }

    [Server]
    public void SetPlayerFixedPosition(Vector2 fixedPosition, Vector2 worldPosition, NetworkConnection clientConnection)
    {
        this.fixedPosition = fixedPosition;
        Target_UpdateCheckerPosition(clientConnection, worldPosition);
    }

    [TargetRpc]
    private void Target_UpdateCheckerPosition(NetworkConnection networkConnection, Vector2 worldPosition)
    {
        SetCheckerPosition(worldPosition);
    }

    public Vector2 GetPlayerFixedPosition()
    {
        return fixedPosition;
    }

    public void SetCheckerPosition(Vector2 position)
    {
        playerChecker.GetComponent<PlayerChecker>().UpdatePlayerPosition(position);
    }
}
