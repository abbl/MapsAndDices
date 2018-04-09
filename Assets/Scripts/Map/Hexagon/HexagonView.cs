using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HexagonController))]
public class HexagonView : MonoBehaviour {
    private HexagonController hexagonController;
    private Hexagon hexagon;

	// Use this for initialization
	void Start () {
        hexagonController = GetComponent<HexagonController>();
        hexagon = GetComponent<Hexagon>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
        {
            if (IsHit())
            {
                IsHexagonLeftClicked();
                IsHexagonRightClicked();
            }
        }
    }

    /// <summary>
    /// In case of left clicking player will move to certain hex
    /// </summary>
    private void IsHexagonLeftClicked()
    {
        if (Input.GetButtonDown("MouseLeftButton"))
        {
            LocalDataManager.GetLocalPlayerGameObject().GetComponent<Player>().MakeMove(hexagon.GetFixedPosition());
        }
    }

    /// <summary>
    /// In case of right clicking a hexagon description card will appear.
    /// </summary>
    private void IsHexagonRightClicked()
    {
        if (Input.GetButtonDown("MouseRightButton"))
        {
            if (!hexagonController.IsHexagonDisplayed())
            {
                hexagonController.DisplayHexagonCard();
                return;
            }
            hexagonController.HideHexagonCard();
        }
    }

    /// <summary>
    /// Checks if hexagon was clicked by player.
    /// </summary>
    private bool IsHit()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null && hit.transform == gameObject.transform)
        {
            return true;
        }
        return false;
    }
}
