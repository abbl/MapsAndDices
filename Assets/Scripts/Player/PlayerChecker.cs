using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerChecker : NetworkBehaviour {
    [SyncVar]
    public Color playerColor;
    [SyncVar]
    public Vector2 fixedPosition;

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        Cmd_RandomPlayerColor();
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

}
