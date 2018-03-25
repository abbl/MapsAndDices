using UnityEngine;
using UnityEngine.UI;

class DiceController : MonoBehaviour
{
    private static GameObject rollResultWindow;
    private static Image rollResultPanel;
    private static Text rollResultText;
    public float fadeOutDivisor;
    public int minRoll;
    public int maxRoll;

    void Start()
    {
        PrepareRollResultWindow();
    }

    private void PrepareRollResultWindow()
    {
        rollResultWindow = Instantiate(Resources.Load("UI/RollResultPanel"), GameObject.Find("Canvas").transform, false) as GameObject;
        rollResultPanel = rollResultWindow.GetComponent<Image>();
        rollResultText = rollResultWindow.GetComponentInChildren<Text>();
        rollResultWindow.SetActive(false);
    }

    public void RollDice()
    {
        int[] dices = new int[2];

        for (int i = 0; i < dices.Length; i++)
        {
            dices[i] = Dice.Roll(minRoll, maxRoll);
        }
        DisplayRollResult(dices);
    }

    private void DisplayRollResult(int[] dices)
    {
        RestoreComponentsDefaultAlphaColor();
        rollResultText.text = CreateRollResultText(dices);
        rollResultWindow.SetActive(true);
    }

    private string CreateRollResultText(int[] dices)
    {
        string defaultText = "Roll result: ";

        for (int i = 0; i < dices.Length; i++)
        {
            if(i % 2 != 0)
            {
                defaultText += " & ";
            }
            defaultText += dices[i];
        }
        return defaultText;
    }

    void Update()
    {
        if (rollResultWindow.activeSelf)
        {
            FadeOutRollWindow();
        }
    }

    private void FadeOutRollWindow()
    {
        if(rollResultPanel.color.a > 0)
        {
            ChangeComponentsAlphaColor(Time.deltaTime / fadeOutDivisor);
        }
        else
        {
            rollResultWindow.SetActive(false);
        }
    }

    private void ChangeComponentsAlphaColor(float value)
    {
        rollResultPanel.color = new Color(rollResultPanel.color.r, rollResultPanel.color.g, rollResultPanel.color.b, rollResultPanel.color.a - value);
        rollResultText.color = new Color(rollResultText.color.r, rollResultText.color.g, rollResultText.color.b, rollResultText.color.a - value);
    }

    private void RestoreComponentsDefaultAlphaColor()
    {
        rollResultPanel.color = new Color(rollResultPanel.color.r, rollResultPanel.color.g, rollResultPanel.color.b, 1f);
        rollResultText.color = new Color(rollResultText.color.r, rollResultText.color.g, rollResultText.color.b, 1f);
    }
}
