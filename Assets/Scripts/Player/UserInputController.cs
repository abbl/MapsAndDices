using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInputController : MonoBehaviour {
    public void SkipTurn()
    {
        LocalDataManager.GetLocalPlayerGameObject().GetComponent<Player>().SkipTurn();
    }
}
