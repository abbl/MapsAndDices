using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerChecker : NetworkBehaviour {
    public float zOffsetFromMap;
    [SyncVar]
    public Color playerColor;

    void Start()
    {
        if (isServer)
        {
            RandomPlayerColorOnServer();
        }
    }

    [Command]
    private void Cmd_RandomPlayerColor()
    {
        RandomPlayerColorOnServer();
    }

    [Server]
    private void RandomPlayerColorOnServer()
    {
        playerColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
    }

    void Update()
    {
        if (isClient)
        {
            if (GetSpriteRenderer().color != playerColor)
            {
                GetSpriteRenderer().color = playerColor;
            }
        }
    }

    private SpriteRenderer GetSpriteRenderer()
    {
        return GetComponent<SpriteRenderer>();
    }

    public void UpdatePlayerPosition(Vector2 position)
    {
        transform.position = new Vector3(position.x, position.y, zOffsetFromMap);
    }
}
