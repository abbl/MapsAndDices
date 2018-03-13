using UnityEngine;
using System.Collections;

public class Hexagon : MonoBehaviour {
    private static PlayerActionController playerActionController;
    private Color defaultColor;
    public Vector2 fixedPosition;

	void Start () {
        playerActionController = GameObject.Find("Controllers").GetComponent<PlayerActionController>();
        StoreDefaultMaterial();
    }
	
    private void StoreDefaultMaterial()
    {
        defaultColor = new Color(1f, 1f, 1f, 1f);
    }

    void OnMouseDown()
    {
        playerActionController.ReceivePlayerMove(this);
    }

    public void SetColor(Color color)
    {
        GetSpriteRenderer().color = color;
    }

    public void RestoreDefaultColor()
    {
        GetSpriteRenderer().color = defaultColor;
    }

    public bool isPositionEqual(Vector2 fixedPosition)
    {
        if (this.fixedPosition.x == fixedPosition.x && this.fixedPosition.y == fixedPosition.y)
            return true;
        return false;
    }
    
    private SpriteRenderer GetSpriteRenderer()
    {
        return gameObject.GetComponent<SpriteRenderer>();
    }

    public void SetFixedPosition(Vector2 fixedPosition)
    {
        this.fixedPosition = fixedPosition;
    }

    public Vector2 GetFixedPosition()
    {
        return fixedPosition;
    }
}
