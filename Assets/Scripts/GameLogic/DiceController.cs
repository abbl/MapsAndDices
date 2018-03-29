using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DiceController : NetworkBehaviour {
    private GameObject rollResultWindow;
    public GameObject rollResultPrefab;

    [SyncVar]
    public int minRoll;
    [SyncVar]
    public int maxRoll;
    [SyncVar]
    public int diceAmount;
    [SyncVar]
    public int fadeOutDivisor;

    void Start () {
        InitializeRollResultWindow();
	}
	
    private void InitializeRollResultWindow()
    {
        rollResultWindow = Instantiate(rollResultPrefab, GameObject.Find("Canvas").transform);
        rollResultWindow.SetActive(false);
    }

    [Server]
    public void Roll()
    {
        int rollResult = 0;

        for(int i = 0; i < diceAmount; i++)
        {
            rollResult += Random.Range(minRoll, maxRoll);
        }
        Rpc_DisplayRollResult(rollResult);
    }

    [ClientRpc]
    public void Rpc_DisplayRollResult(int rollResult)
    {
        rollResultWindow.GetComponentInChildren<Text>().text = "Roll result: " + rollResult;
        ChangeRollWindowComponentsOpacity(1f);
        rollResultWindow.SetActive(true);
    }

    void Update()
    {
        if (rollResultWindow.activeSelf)
        {
            FadeOutRollResultWindow();
        }
    }

    private void FadeOutRollResultWindow()
    {
        if (rollResultWindow.GetComponent<Image>().color.a > 0)
        {
            ChangeRollWindowComponentsOpacity(-(Time.deltaTime / fadeOutDivisor));
        }
        else
        {
            rollResultWindow.SetActive(false);
        }
    }

    private void ChangeRollWindowComponentsOpacity(float value)
    {
        rollResultWindow.GetComponent<Image>().color = rollResultWindow.GetComponent<Image>().color + new Color(0f, 0f, 0f, value);
        rollResultWindow.GetComponentInChildren<Text>().color = rollResultWindow.GetComponentInChildren<Text>().color + new Color(0f, 0f, 0f, value);
    }
}
