using UnityEngine;
using UnityEngine.Networking;


public abstract class TurnChangeListener : NetworkBehaviour
{
    public override void OnStartServer()
    {
        AddToRelayObject();
    }

    protected void AddToRelayObject()
    {
        TurnController.AddChangeListener(this);
    }

    abstract public void NextTurn();
}
