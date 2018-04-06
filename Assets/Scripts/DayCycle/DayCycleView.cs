using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DayCycleView : MonoBehaviour
{
    private Canvas inGameCanvas;
    private GameObject newDayPopOut;
    public GameObject newDayPopOutPrefab;

    private void Awake()
    {
        inGameCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }
    
    public void DisplayNewDayPopOut()
    {
        newDayPopOut = Instantiate(newDayPopOutPrefab, inGameCanvas.transform);
    }

    private void Update()
    {
        if (newDayPopOut != null)
            HideNewDayPopOut();
    }

    private void HideNewDayPopOut()
    {
        Image image = newDayPopOut.GetComponentInChildren<Image>();
        Text text = newDayPopOut.GetComponentInChildren<Text>();

        if(image.color.a > 0)
        {
            image.color = image.color + new Color(0f, 0f, 0f, -(0.25f * Time.deltaTime));
            text.color = text.color + new Color(0f, 0f, 0f, -(0.25f * Time.deltaTime));
        }
        else
        {
            Destroy(newDayPopOut);
        }
    }
}
