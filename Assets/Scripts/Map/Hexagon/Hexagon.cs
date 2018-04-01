using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Hexagon : NetworkBehaviour {
    [SyncVar]
    private Vector2 fixedPositon;
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

    public bool IsFixedPositionEqual(Vector2 otherPosition)
    {
        return fixedPositon == otherPosition;
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    private SpriteRenderer GetSpriteRenderer()
    {
        return GetComponent<SpriteRenderer>();
    }
}
