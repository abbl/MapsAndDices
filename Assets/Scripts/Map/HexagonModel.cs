using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HexagonModel : NetworkBehaviour {
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
}
