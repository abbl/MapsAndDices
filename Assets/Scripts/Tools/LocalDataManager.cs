using UnityEngine;
using UnityEngine.Networking;

public class LocalDataManager
{
    public static GameObject[] GetPlayersGameObjects()
    {
        return GameObject.FindGameObjectsWithTag("PlayerConnectionObject");
    }

    public static Player[] GetAllPlayers()
    {
        GameObject[] gameObjectsWithTag = GameObject.FindGameObjectsWithTag("PlayerConnectionObject");
        Player[] players = new Player[gameObjectsWithTag.Length];

        for (int i = 0; i < players.Length; i++)
        {
            players[i] = gameObjectsWithTag[i].GetComponent<Player>();
        }
        return players;
    }

    public static GameObject GetLocalPlayerGameObject()
    {
        foreach(GameObject playerGameObject in GetPlayersGameObjects())
        {
            if (playerGameObject.GetComponent<NetworkIdentity>().isLocalPlayer)
                return playerGameObject;
        }
        return null;
    }

    public static bool isNetIdEqual(GameObject gameObject, NetworkInstanceId id)
    {
        return gameObject.GetComponent<NetworkIdentity>().netId == id;
    }
}
