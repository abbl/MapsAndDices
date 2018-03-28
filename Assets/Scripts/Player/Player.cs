using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
    [SyncVar]
    public string playerName;
    [SyncVar]
    public Color playerColor;
    [SyncVar]
    public Vector2 playerPosition;

	// Use this for initialization
	void Start () {
        Cmd_RandomPlayerColor();
        UpdatePlayerColor();
        Cmd_SpawnOnRandomCords(0, 15);
        UpdatePlayerPosition();
	}

    [Command]
    private void Cmd_RandomPlayerColor()
    {
        playerColor = new Color(RandomSomeNumber(), RandomSomeNumber(), RandomSomeNumber(), 1f);  
    }

    private float RandomSomeNumber()
    {
        return Random.Range(0f, 1f);
    }
    
    private void UpdatePlayerColor()
    {
        GetSpriteRenderer().color = playerColor;
    }

    [Command]
    private void Cmd_SpawnOnRandomCords(int min, int max)
    {
        playerPosition = new Vector2(Random.Range(min, max), Random.Range(min, max));
    }

    private void UpdatePlayerPosition()
    {
        gameObject.transform.position = playerPosition;
    }

    private SpriteRenderer GetSpriteRenderer()
    {
        return GetComponent<SpriteRenderer>();
    }

	// Update is called once per frame
	void Update () {
		
	}
}
