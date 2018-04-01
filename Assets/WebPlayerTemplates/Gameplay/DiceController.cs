using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
class DiceController : NetworkBehaviour
{
    private static GameObject rollResultWindow;
    private static Image rollResultPanel;
    private static Text rollResultText;
    public float fadeOutDivisor;
    [SyncVar]
    public int minRoll;
    [SyncVar]
    public int maxRoll;
    public SyncListInt dices = new SyncListInt();

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

    [ClientRpc]
    public void RpcRollDice()
    {
        if (isServer)
        {
            for (int i = 0; i < dices.Length; i++)
            {
                dices[i] = Dice.Roll(minRoll, maxRoll);
            }
        }
        RpcDisplayRollResult();
    }

    [ClientRpc]
    private void RpcDisplayRollResult()
    {
        RpcRestoreComponentsDefaultAlphaColor();
        rollResultText.text = RpcCreateRollResultText(dices);
        rollResultWindow.SetActive(true);
    }

    private string RpcCreateRollResultText(int[] dices)
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
            RpcFadeOutRollWindow();
        }
    }

    [ClientRpc]
    private void RpcFadeOutRollWindow()
    {
        if(rollResultPanel.color.a > 0)
        {
            RpcChangeComponentsAlphaColor(Time.deltaTime / fadeOutDivisor);
        }
        else
        {
            rollResultWindow.SetActive(false);
        }
    }

    [ClientRpc]
    private void RpcChangeComponentsAlphaColor(float value)
    {
        rollResultPanel.color = new Color(rollResultPanel.color.r, rollResultPanel.color.g, rollResultPanel.color.b, rollResultPanel.color.a - value);
        rollResultText.color = new Color(rollResultText.color.r, rollResultText.color.g, rollResultText.color.b, rollResultText.color.a - value);
    }

    [ClientRpc]
    private void RpcRestoreComponentsDefaultAlphaColor()
    {
        rollResultPanel.color = new Color(rollResultPanel.color.r, rollResultPanel.color.g, rollResultPanel.color.b, 1f);
        rollResultText.color = new Color(rollResultText.color.r, rollResultText.color.g, rollResultText.color.b, 1f);
    }
}
