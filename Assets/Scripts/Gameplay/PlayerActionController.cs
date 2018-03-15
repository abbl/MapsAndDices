using UnityEngine;
using System.Collections;

public class PlayerActionController : MonoBehaviour {
    private GameController gameController;
	
    // Use this for initialization
	void Start () {
        InitializeFields();
	}
	
    private void InitializeFields()
    {
        gameController = gameObject.GetComponent<GameController>();
    }

	// Update is called once per frame
	void Update () {
	
	}

    public void ReceivePlayerMove(Hexagon hexagon)
    {
        Vector2 hexagonPosition = hexagon.GetFixedPosition();
        Vector2 playerPosition = gameController.GetPlayerTurn().GetCurrentPosition();
        float distanceDifferenceX = playerPosition.x - hexagonPosition.x;
        float distanceDifferenceY = playerPosition.y - hexagonPosition.y;

        //Simple checking if given hexagon is close to player.
        if (distanceDifferenceX <= 1 && distanceDifferenceX >= -1 &&//x
            distanceDifferenceY <= 1 && distanceDifferenceY >= -1)
        {
            if(distanceDifferenceX != 0 || distanceDifferenceY != 0)
            {
                if (playerPosition.x % 2 == 0) //When x is even player is able to do 2 additional moves that he shouldn't be able to do in a game.
                {
                    if (distanceDifferenceX != 1 || distanceDifferenceY != 1) //removing #1 case when player shouldn't be able to move
                    {
                        if (distanceDifferenceX != -1 || distanceDifferenceY != 1) //removing #2 case when player shouldn't be able to move
                        {
                            gameController.GetPlayerTurn().MovePlayer(hexagon);
                        }
                    }
                }
                else //The same thing happens when x is odd so as you can guess I removed those two cases too.
                {
                    if (distanceDifferenceX != -1 || distanceDifferenceY != -1) //removing #1 case when player shouldn't be able to move
                    {
                        if (distanceDifferenceX != 1 || distanceDifferenceY != -1) //removing #2 case when player shouldn't be able to move
                        {
                            gameController.GetPlayerTurn().MovePlayer(hexagon);
                        }
                    }
                }
            }
        }
    }

    private bool IsPlayerAbleToMoveTowardsHex(Vector2 hexPosition)
    {
        return false;
    }
}
