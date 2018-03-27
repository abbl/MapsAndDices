using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    private HexagonMapGenerator map;
    private TurnController turnController;
    private ArrayList players;
    public int playersAmount;

    /*
     * Things done only once when game scene is loaded.
     */

    void Start()
    {
        InitializeFields();
        GenerateMap();
        SpawnPlayers();
    }

    private void InitializeFields()
    {
        map = GameObject.Find("HexagonMap").GetComponent<HexagonMapGenerator>();
        turnController = GetComponent<TurnController>();
        players = new ArrayList();
    }

    private void GenerateMap()
    {
        map.GenerateWorld();
    }

    private void SpawnPlayers()
    {
        GameObject playersGameObject = GameObject.Find("Players");
        for(int i = 0; i < playersAmount; i++)
        {
            GameObject player = Instantiate(Resources.Load("PlayerPrefab/DefaultPlayer"), playersGameObject.transform, false) as GameObject;
            player.name = "Player " + i;
            Player playerComp = player.GetComponent<Player>();
            playerComp.MovePlayer(RandomPlayerPosition());
            players.Add(playerComp);
        }
    }

    private Hexagon RandomPlayerPosition()
    {
        return map.GetHexagon(new Vector2(Random.Range(1, map.width), Random.Range(1, map.height)));
    }

    /*
    * Things that are used during gameplay
    */

    void Update()
    {

    }

    public Player GetPlayerTurn()
    {
        return turnController.GetPlayerTurn();
    }

    public ArrayList GetPlayers()
    {
        return players;
    }
}
