using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hexagon : MonoBehaviour {
    private static PlayerActionController playerActionController;
    private Color defaultColor;
    public Vector2 fixedPosition;

    private GameObject hexagonCard;
    public Sprite hexagonImage;
    public string hexagonName;
    public string hexagonDesc;

	void Start () {
        playerActionController = GameObject.Find("Controllers").GetComponent<PlayerActionController>();
        StoreDefaultMaterial();
    }
	
    private void StoreDefaultMaterial()
    {
        defaultColor = new Color(1f, 1f, 1f, 1f);
    }

    void Update()
    {
        CheckIfHexWasClicked();
    }

    private void CheckIfHexWasClicked()
    {
        if (Input.GetMouseButtonDown(0)) //Left mouse button
        {
            if (isHexagonHit())
            {
                playerActionController.ReceivePlayerMove(this);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if(hexagonCard != null)
            {
                if (isHexagonHit())
                {
                    HideHexagonCard();
                }
            }
            else
            {
                if (isHexagonHit())
                {
                    DisplayHexagonCard();
                }
            }
        }
    }

    private bool isHexagonHit()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if(hit.collider != null && hit.transform == gameObject.transform)
        {
            return true;
        }
        return false;
    }

    void OnMouseExit()
    {
        if(hexagonCard != null)
        {
            HideHexagonCard();
        }
    }

    private void DisplayHexagonCard()
    {
        CreateHexagonCard();
    }

    private void CreateHexagonCard()
    {
        hexagonCard = Instantiate(Resources.Load("Cards/DefaultHexCard"), GameObject.Find("Canvas").transform, false) as GameObject;
        Image hexImage = (Image)hexagonCard.transform.FindChild("HexImage").gameObject.GetComponent<Image>();
        Text hexName = (Text) hexagonCard.transform.FindChild("HexName").gameObject.GetComponent<Text>();
        Text hexDesc = (Text)hexagonCard.transform.FindChild("HexDesc").gameObject.GetComponent<Text>();
        hexImage.sprite = hexagonImage;
        hexName.text = hexagonName;
        hexDesc.text = hexagonDesc;
    }

    private void HideHexagonCard()
    {
        Destroy(hexagonCard);
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
