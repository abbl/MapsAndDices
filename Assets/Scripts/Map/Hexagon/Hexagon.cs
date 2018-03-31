using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Hexagon : NetworkBehaviour {
    [SyncVar]
    private Vector2 fixedPositon = Vector2.zero;
    public Sprite hexagonCardSprite;
    public string hexagonName;
    public string hexagonDesc;

    [Server]
    public void SetFixedPosition(Vector2 position)
    {
        this.fixedPositon = position;
    }

    public Vector2 GetFixedPosition()
    {
        return fixedPositon;
    }

    public Vector2 GetHexagonSize()
    {
        return GetSpriteRenderer().bounds.size;
    }

    private SpriteRenderer GetSpriteRenderer()
    {
        return GetComponent<SpriteRenderer>();
    }
}
