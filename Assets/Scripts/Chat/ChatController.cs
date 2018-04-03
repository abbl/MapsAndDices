using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChatController : NetworkBehaviour {
    private GameObject chatContent;

	// Use this for initialization
	void Start () {
        chatContent = GameObject.Find("Chat").transform.Find("Scroll View").transform.Find("Viewport").transform.Find("Content").gameObject;
	}

    public void SendMessage()
    {

    }
}
