using UnityEngine;
using System.Collections;

public class HexagonMapGenerator : MonoBehaviour {
    private ArrayList hexagons;
    public GameObject sampleHexagon;
    public int width;
    public int height;

	void Start () {
        hexagons = new ArrayList();
	}
	
    public void GenerateWorld()
    {
        for(int row = 1; row < height + 1; row++) //y
        {
            for(int column = 1; column < width + 1; column++) //x
            {
                CreateHexagon(column, row);
            }
        }
    }

    private void CreateHexagon(int x, int y)
    {
        GameObject hexagonObject = Instantiate(sampleHexagon, GetFixedPosition(x, y), Quaternion.identity) as GameObject;
        hexagonObject.transform.parent = gameObject.transform;
        Hexagon hexagon = hexagonObject.GetComponent<Hexagon>();
        hexagon.fixedPosition = new Vector2(x, y);
        hexagons.Add(hexagon);
    }

    private Vector2 GetFixedPosition(int column, int row)
    {
        Vector2 positionVector = Vector2.zero;

        float offset = 0;
        if(column % 2 == 0)
        {
            offset = (getHexagonHeight() / 2);
        }
        positionVector.x = column * (getHexagonWidth() / 1.25f);
        positionVector.y = (row * getHexagonHeight() * 1.05f) + offset;

        return positionVector;
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
