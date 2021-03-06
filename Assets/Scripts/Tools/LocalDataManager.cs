﻿using UnityEngine;
using UnityEngine.Networking;

public class LocalDataManager
{
    public static GameObject[] GetPlayersGameObjects()
    {
        return GameObject.FindGameObjectsWithTag("PlayerConnectionObject");
    }

    public static GameObject[] GetPlayersCheckers()
    {
        return GameObject.FindGameObjectsWithTag("PlayerChecker");
    }

    public static GameObject GetPlayerGameObject(NetworkInstanceId netId)
    {
        foreach(GameObject gameObject in GetPlayersGameObjects())
        {
            if (IsNetIdEqual(gameObject, netId))
                return gameObject;
        }
        return null;
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

    public static bool IsNetIdEqual(GameObject gameObject, NetworkInstanceId id)
    {
        return gameObject.GetComponent<NetworkIdentity>().netId == id;
    }

    public static GameObject GetGameObjectByTagWithLocalAuthority(string tag)
    {
        foreach(GameObject gameObject in GameObject.FindGameObjectsWithTag(tag))
        {
            if (gameObject.GetComponent<NetworkIdentity>().hasAuthority)
                return gameObject;
        }
        return null;
    }
}
