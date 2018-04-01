using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
    public GameObject playerCheckerPrefab;

    // Use this for initialization
	void Start () {
        if (!isLocalPlayer)
            return;
        Cmd_SpawnPlayerChecker();
	}

    [Command]
    private void Cmd_SpawnPlayerChecker()
    {
        SpawnPlayer();
    }

    [Server]
    private void SpawnPlayer()
    {
        GameObject playerChecker = Instantiate(playerCheckerPrefab);
        NetworkServer.SpawnWithClientAuthority(playerChecker, connectionToClient);
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
    
    /// <summary>
    /// This method sends a request to server to move local player to certain hex.
    /// </summary>
    /// <param name="fixedPosition">Fixed position of hexagon</param>
    public void MakeMove(Vector2 fixedPosition)
    {
        if (isLocalPlayer)
            Cmd_MakeMove(fixedPosition);
    }

    [Command]
    private void Cmd_MakeMove(Vector2 fixedPosition)
    {
        
    }
}
