using UnityEngine;
using System.Collections;

public class GameCordinator : MonoBehaviour {
    private HexagonMapGenerator map;
    private ArrayList players;
    public int playersAmount;

    void Start()
    {
        InitializeFields();
        GenerateMap();
        SpawnPlayers();
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
        return map.GetHexagon(new Vector2(Random.Range(1, map.width), Random.Range(1, map.height)));
    }
}
