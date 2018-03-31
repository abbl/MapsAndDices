using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonView : MonoBehaviour {
    private HexagonController hexagonController;

	// Use this for initialization
	void Start () {
        hexagonController = GetComponent<HexagonController>();	
	}
	
	// Update is called once per frame
	void Update () {
        isHexagonLeftClicked();
        isHexagonRightClicked();
    }

    /// <summary>
    /// In case of left clicking player will move to certain hex
    /// </summary>
    private void isHexagonLeftClicked()
    {

    }

    /// <summary>
    /// In case of right clicking a hexagon description card will appear.
    /// </summary>
    private void isHexagonRightClicked()
    {
        hexagonController.DisplayHexagonCard();
    }
}
