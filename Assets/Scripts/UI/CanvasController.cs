using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CanvasController : NetworkBehaviour {
    private bool isConnected = false;
    private Canvas canvas;

	// Use this for initialization
	void Start () {
        HideCanvasBeforeGameStarts();
	}
	
    private void HideCanvasBeforeGameStarts()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        canvas.enabled = false;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        isConnected = true;
    }

    void Update()
    {
        if (!isConnected)
        {

        }    
    }
}
