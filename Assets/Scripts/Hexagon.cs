﻿using UnityEngine;
using System.Collections;

public class Hexagon : MonoBehaviour {
    private static GameCordinator gameCordinator;
    private Material defaultMaterial;
    public Vector2 fixedPosition;

	void Start () {
        gameCordinator = GameObject.Find("GameCordinator").GetComponent<GameCordinator>();
        StoreDefaultMaterial();
	}
	
    private void StoreDefaultMaterial()
    {
        defaultMaterial = GetComponent<Renderer>().material;
    }

    void OnMouseDown()
    {

    }

    public void SetColor(Color color)
    {
        GetSpriteRenderer().color = color;
    }

    public void RestoreDefaultColor()
    {
        GetComponent<Renderer>().material = defaultMaterial;
    }

    public bool isPositionEqual(Vector2 fixedPosition)
    {
        if (this.fixedPosition.x == fixedPosition.x && this.fixedPosition.y == fixedPosition.y)
            return true;
        return false;
    }
    
    private SpriteRenderer GetSpriteRenderer()
    {
        return gameObject.GetComponent<SpriteRenderer>();
    }

    public void SetFixedPosition(Vector2 fixedPosition)
    {
        this.fixedPosition = fixedPosition;
    }
}
