using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnView : MonoBehaviour {
    private static TurnController turnController;
    private int previousTimerValue = -1;
    private int previousPlayerIndex = -1;

    void Start()
    {
        turnController = GetComponent<TurnController>();
    }

    public void SkipTurn()
    {
        LocalDataManager.GetLocalPlayerGameObject().GetComponent<Player>().SkipTurn();
    }
    
    void Update()
    {
        HasTimerValueChanged();
        HasPlayerTurnChanged();
    }

    private void HasTimerValueChanged()
    {
        if(previousTimerValue != turnController.GetTurnTimerCount())
        {
            previousTimerValue = turnController.GetTurnTimerCount();
            UpdateTurnTimerDisplay();
        }
    }

    private void UpdateTurnTimerDisplay()
    {
        GameObject.Find("TurnTimePanel").GetComponentInChildren<Text>().text = previousTimerValue.ToString();
    }

    private void HasPlayerTurnChanged()
    {
        if(previousPlayerIndex != turnController.GetPlayerIndex())
        {
            previousPlayerIndex = turnController.GetPlayerIndex();
            UpdatePlayerTurnDisplay();
        }
    }

    private void UpdatePlayerTurnDisplay()
    {
        GameObject.Find("PlayerTurnPanel").GetComponentInChildren<Text>().text = previousPlayerIndex.ToString();
    }
}
