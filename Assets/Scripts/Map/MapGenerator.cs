using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MapGenerator : NetworkBehaviour {
    private Vector2 hexagonSize;
    private ArrayList hexagonsArray;
    public GameObject[] hexagons;
    public float hexagonOffset;
    public int rowsNumber;
    public int columnsNumber;
    
	void Start () {
        hexagonsArray = new ArrayList();

        if (isServer)
        {
            InitializeFields();
            Generate();
        }
	}

    [Server]
    private void InitializeFields()
    {
        hexagonSize = hexagons[0].GetComponent<Hexagon>().GetHexagonSize();
    }

    [Server]
    public void Generate()
    {
        for (int row = 1; row < rowsNumber + 1; row++) //y
        {
            for (int column = 1; column < columnsNumber + 1; column++) //x
            {
                int hexagonIndex = Random.Range(0, hexagons.Length);
                hexagonsArray.Add(CreateHexagon(row, column, hexagonIndex));
            }
        }
    }

    [Server]
    private Hexagon CreateHexagon(int row, int column, int hexagonIndex)
    {
        GameObject hexagonObject = Instantiate(hexagons[hexagonIndex], CalculateHexagonPosition(row, column), Quaternion.identity) as GameObject;
        hexagonObject.transform.parent = gameObject.transform;
        NetworkServer.Spawn(hexagonObject);
        Hexagon hexagon = hexagonObject.GetComponent<Hexagon>();
        hexagon.SetFixedPosition(new Vector2(column, row));
        return hexagon;
    }

    [Server]
    private Vector2 CalculateHexagonPosition(int row, int column)
    {
        Vector2 positionVector = Vector2.zero;

        float offset = 0;
        if (column % 2 == 0)
        {
            offset = (hexagonSize.y / 2);
        }
        positionVector.x = (column * (hexagonSize.x * 0.75f));
        positionVector.y = ((row * hexagonSize.y) + offset);
        return positionVector;
    }

    public Hexagon GetHexagon(Vector2 fixedPosition)
    {
        foreach(Hexagon hexagon in hexagonsArray)
        {
            if (hexagon.IsFixedPositionEqual(fixedPosition))
                return hexagon;
        }
        return null;
    }
}
