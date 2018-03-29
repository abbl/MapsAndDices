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

    private void SpawnPlayer()
    {
        if (!isServer)
            return;
        GameObject playerChecker = Instantiate(playerCheckerPrefab);
        NetworkServer.SpawnWithClientAuthority(playerChecker, connectionToClient);
    }
}
