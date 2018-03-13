using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    private HexagonMapGenerator map;
    private ArrayList players;
    private Player turn;
    public int playersAmount;

    /*
     * Things done only once when game scene is loaded.
     */

    void Start()
    {
        InitializeFields();
        GenerateMap();
        SpawnPlayers();
        turn = (Player) players[0];
    }

    private void InitializeFields()
    {
        map = GameObject.Find("HexagonMap").GetComponent<HexagonMapGenerator>();
        players = new ArrayList();
    }

    private void GenerateMap()
    {
        map.GenerateWorld();
    }

    private void SpawnPlayers()
    {
        for(int i = 0; i < playersAmount; i++)
        {
            Player player = new Player();
            player.MovePlayer(RandomPlayerPosition());
            players.Add(player);
        }
    }

    private Hexagon RandomPlayerPosition()
    {
        return map.GetHexagon(new Vector2(5, 6)); //new Vector2(Random.Range(1, map.width), Random.Range(1, map.height))
    }

    /*
    * Things that are used during gameplay
    */

    void Update()
    {

    }

    public Player GetPlayerTurn()
    {
        return turn;
    }
}
