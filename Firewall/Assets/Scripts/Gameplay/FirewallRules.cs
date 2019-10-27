using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FirewallRules : MonoBehaviour
{
    [SerializeField]
    private TerminalManager terminalManager;
    public List<string> rules;

    void Start()
    {
        PhotonView view = GetComponent<PhotonView>();
        terminalManager.register(view.ViewID, rules);
    }
}
