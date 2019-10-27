using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FirewallRuleInput : MonoBehaviour
{
    [SerializeField]
    private TerminalManager terminalManager;
    [SerializeField]
    private TextMeshProUGUI inputField;
    public int terminalID;

    public void sendInput() {
        terminalManager.sendInput(terminalID, inputField.text);
    }
}
