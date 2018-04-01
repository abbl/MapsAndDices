using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HexagonController : MonoBehaviour {
    private static GameObject hexagonCard;
    private Hexagon hexagon;
    
    private void Start()
    {
        hexagon = GetComponent<Hexagon>();
    }

    public void DisplayHexagonCard()
    {
        CreateHexagonCard();
    }

    private void CreateHexagonCard()
    {
        hexagonCard = Instantiate(Resources.Load("Cards/DefaultHexCard"), GameObject.Find("Canvas").transform) as GameObject;
        Image hexImage = (Image)hexagonCard.transform.Find("HexImage").gameObject.GetComponent<Image>();
        Text hexName = (Text)hexagonCard.transform.Find("HexName").gameObject.GetComponent<Text>();
        Text hexDesc = (Text)hexagonCard.transform.Find("HexDesc").gameObject.GetComponent<Text>();
        hexImage.sprite = hexagon.hexagonCardSprite;
        hexName.text = hexagon.hexagonName;
        hexDesc.text = hexagon.hexagonDesc;
    }

    public void HideHexagonCard()
    {
        if (hexagonCard != null)
            Destroy(hexagonCard);
    }

    public bool IsHexagonDisplayed()
    {
        return hexagonCard != null;
    }

}
