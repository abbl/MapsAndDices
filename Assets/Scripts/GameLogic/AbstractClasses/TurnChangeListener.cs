using UnityEngine;
using UnityEngine.Networking;


public abstract class TurnChangeListener : NetworkBehaviour
{
    void Start()
    {
        AddToRelayObject();
    }

    private void AddToRelayObject()
    {
        GameObject.Find("TurnController").GetComponent<TurnController>().AddChangeListener(this);
    }

    abstract public void NextTurn();
}
