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
        Debug.Log("NetId:" + netId);
        GameObject.Find("TurnController").GetComponent<TurnController>().SkipTurn(netId);
    }
}
