using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MovementController : TurnChangeListener {
    private TurnController turnController;
    [SyncVar]
    private bool isPlayerAbleToMove;

    private void Start()
    {
        if (isServer)
        {
            turnController = GameObject.Find("TurnController").GetComponent<TurnController>();
            isPlayerAbleToMove = true;
        }
    }

    [Server]
    public void ReceiveMoveRequest(Vector2 fixedPosition, NetworkInstanceId playerNetId, NetworkConnection networkConnection)
    {
        if (turnController.DoesThisNetIdMatchCurrentTurn(playerNetId))
        {
            if (isPlayerAbleToMove)
            {
                GameObject playerGameObject = LocalDataManager.GetPlayerGameObject(playerNetId);
                Hexagon hexagon = GameObject.Find("MapGenerator").GetComponent<MapGenerator>().GetHexagon(fixedPosition);

                if (playerGameObject != null && hexagon != null)
                {
                    if (CheckIfPlayerCanMoveToCertainHex(playerGameObject, hexagon))
                    {
                        isPlayerAbleToMove = false;
                        playerGameObject.GetComponent<Player>().SetPlayerFixedPosition(fixedPosition, hexagon.GetPosition(), networkConnection);
                    }
                }
                else
                {
                    Debug.Log("Can't move player cause object with such netId does not exist or hexagon is equal to null", playerGameObject);
                }
            }
        }
    }

    public void MovePlayerOnSpawn(Vector2 fixedPosition, NetworkInstanceId playerNetId, NetworkConnection networkConnection)
    {
        Hexagon hexagon = GameObject.Find("MapGenerator").GetComponent<MapGenerator>().GetHexagon(fixedPosition);
        GameObject playerGameObject = LocalDataManager.GetPlayerGameObject(playerNetId);

        if (hexagon != null)
        {
            playerGameObject.GetComponent<Player>().SetPlayerFixedPosition(fixedPosition, hexagon.GetPosition(), networkConnection);
        }
        else
        {
            Debug.Log("Can't spawn player on given hexagon because such hex doesn't exist.", hexagon);
        }
    }

    [Server]
    private bool CheckIfPlayerCanMoveToCertainHex(GameObject player, Hexagon hexagon)
    {
        Vector2 hexagonPosition = hexagon.GetFixedPosition();
        Vector2 playerPosition = player.GetComponent<Player>().GetPlayerFixedPosition();

        float distanceDifferenceX = playerPosition.x - hexagonPosition.x;
        float distanceDifferenceY = playerPosition.y - hexagonPosition.y;

        if (distanceDifferenceX <= 1 && distanceDifferenceX >= -1 &&//x
                distanceDifferenceY <= 1 && distanceDifferenceY >= -1)
        {
            if (distanceDifferenceX != 0 || distanceDifferenceY != 0)
            {
                if (playerPosition.x % 2 == 0) //When x is even player is able to do 2 additional moves that he shouldn't be able to do in a game.
                {
                    if (distanceDifferenceX != 1 || distanceDifferenceY != 1) //removing #1 case when player shouldn't be able to move
                    {
                        if (distanceDifferenceX != -1 || distanceDifferenceY != 1) //removing #2 case when player shouldn't be able to move
                        {
                            return true;
                        }
                    }
                }
                else //The same thing happens when x is odd so as you can guess I removed those two cases too.
                {
                    if (distanceDifferenceX != -1 || distanceDifferenceY != -1) //removing #1 case when player shouldn't be able to move
                    {
                        if (distanceDifferenceX != 1 || distanceDifferenceY != -1) //removing #2 case when player shouldn't be able to move
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    [Server]
    public override void NextTurn()
    {
        isPlayerAbleToMove = true;
    }
}
