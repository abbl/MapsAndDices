using UnityEngine;
using System.Collections;

public class HexagonMapGenerator : MonoBehaviour {
    public GameObject sampleHexagon;
    private ArrayList hexagons;

	// Use this for initialization
	void Start () {
        hexagons = new ArrayList();
        PlayerPrefs.SetInt("mapRows", 32);
        PlayerPrefs.SetInt("mapColumns", 32);
        GenerateWorld(PlayerPrefs.GetInt("mapRows"), PlayerPrefs.GetInt("mapColumns"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void GenerateWorld(int rows, int colums)
    {
        for(int row = 1; row < rows; row++)
        {
            for(int column = 1; column < colums; column++)
            {
                createHexagon(column, row);
            }
        }
    }

    private GameObject createHexagon(int x, int y)
    {
        GameObject gameObject = Instantiate(sampleHexagon);
        gameObject.transform.parent = GameObject.Find("HexagonMap").transform;
        SetFixedPosition(gameObject, x, y);
        Hexagon hexagon = gameObject.GetComponent<Hexagon>();
        hexagon.fixedPosition = new Vector2(x, y);
        hexagons.Add(hexagon);
        return null;
    }

    private void SetFixedPosition(GameObject hexagon, int column, int row)
    {
        Vector2 positionVector = hexagon.transform.position;

        float offset = 0;
        if(column % 2 == 0)
        {
            offset = (getHexagonHeight() / 2);
        }
        positionVector.x = column * (getHexagonWidth() / 1.25f);
        positionVector.y = (row * getHexagonHeight() * 1.05f) + offset;
        hexagon.transform.position = positionVector;
    }

    private float getHexagonWidth()
    {
        return sampleHexagon.GetComponent<Renderer>().bounds.size.x;
    }

    private float getHexagonHeight()
    {
        return sampleHexagon.GetComponent<Renderer>().bounds.size.y;
    }

    public Hexagon GetHexagon(Vector2 fixedPosition)
    {
        foreach(Hexagon hexagon in hexagons){
            if (hexagon.isPositionEqual(fixedPosition))
                return hexagon;
        }
        return null;
    }
}
