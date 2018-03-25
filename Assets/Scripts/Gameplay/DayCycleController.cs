using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DayCycleController : MonoBehaviour {
    private int cycleCount;
    private int cycleLenght; //How many turns need to pass in order to change to next cycle
    private int dayCycleIndex;
    private Light mainLight;
    public GameObject[] dayCycleArray;

	// Use this for initialization
	void Start () {
        mainLight = GameObject.Find("Directional light").GetComponent<Light>();
        cycleLenght = gameObject.GetComponent<GameController>().GetPlayers().Count;
        cycleCount = 1;
        dayCycleIndex = 0;
        SetFirstDayCycle();
	}

    private void SetFirstDayCycle()
    {
        mainLight.intensity = dayCycleArray[0].GetComponent<DayCycle>().lightIntensity;
        UpdateGUIText();
    }

    public void NextTurn()
    {
        if(++cycleCount > cycleLenght)
        {
            cycleCount = 0;
            NextCycle();
        }
    }

    private void NextCycle()
    {
        dayCycleIndex++;
        if(dayCycleIndex > dayCycleArray.Length - 1)
        {
            dayCycleIndex = 0;
        }
        mainLight.intensity = dayCycleArray[dayCycleIndex].GetComponent<DayCycle>().lightIntensity;
        UpdateGUIText();
    }

    private void UpdateGUIText()
    {
        GameObject.Find("DayCyclePanel").GetComponentInChildren<Text>().text = dayCycleArray[dayCycleIndex].name;
    }
}
