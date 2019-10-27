using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FirewallRules : MonoBehaviour
{
    [SerializeField]
    private TerminalManager terminalManager;
    [SerializeField]
    private GameObject firewall;
    public List<string> rules;

    void Start()
    {
        terminalManager.register(gameObject);
    }

    public void enableFirewall() {
        firewall.SetActive(true);
    }

    public void disableFirewall() {
        firewall.SetActive(false);
    }
}
