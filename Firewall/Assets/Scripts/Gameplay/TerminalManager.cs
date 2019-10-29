using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using Photon.Pun;

public class TerminalManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public Dictionary<int, GameObject> terminals = new Dictionary<int, GameObject>();
    public Dictionary<int, Dictionary<string, bool>> firewalls = new Dictionary<int, Dictionary<string, bool>>();

    private PhotonView view;

    private void Start() {
        view = GetComponent<PhotonView>();
    }
    public void register(GameObject terminalGO) {
        List<string> rules = terminalGO.GetComponent<FirewallRules>().rules;
        int id = terminalGO.GetComponent<PhotonView>().ViewID;
        terminals.Add(id, terminalGO);

        Dictionary<string, bool> terminal = new Dictionary<string, bool>();
        foreach(string rule in rules) {
            Debug.Log("Added " + rule + " to terminal " + id);
            terminal.Add(rule, false);
        }
        firewalls.Add(id, terminal);
    }

    private bool isValidRule(Dictionary<string, bool> terminal, string rule) {
        return terminal.Keys.Any(
            key => 
                key.Equals(rule, System.StringComparison.CurrentCultureIgnoreCase)  
        );
    }

    private bool allRulesSet(int id) {
        Dictionary<string, bool> terminal = firewalls[id];
        foreach(string rule in terminal.Keys) {
            if(!terminal[rule]) {
                return false;
            }
        }
        
        Debug.Log("All firewall rules for terminal " + id + " are set!!");
        return true;
    }

    private void determineFirewallState(int id) {
        view.RPC("RPC_UpdateFirewallRules", RpcTarget.OthersBuffered, firewalls);
        Debug.Log("Terminal " + view.ViewID + " is updating firewall state ");
        view.RPC("RPC_DetermineFirewallState", RpcTarget.AllBuffered, id);
    }

    public void disableRandomFirewallRule(int id) {
        Dictionary<string, bool> terminal = firewalls[id];
        int ruleIndex = Random.Range(0, terminal.Count);
        string key = terminal.Keys.ElementAt(ruleIndex);

        Debug.Log("Disabling random rule: " + key + " on terminal " + id);
        terminal[key] = false;
        determineFirewallState(id);
    }

    public bool sendInput(int id, string rule) {
        string standardizedRuleStr = rule.Substring(0,rule.Length-1);

        Debug.Log("Terminal " + id + " sent " + gameObject.name + " received text input: " + standardizedRuleStr);
        bool validRule = isValidRule(firewalls[id], standardizedRuleStr);

        if(validRule && !firewalls[id][standardizedRuleStr]) {
            Debug.Log(standardizedRuleStr + " ACCEPTED!");
            firewalls[id][standardizedRuleStr] = true;
        }
        else if(validRule) {
            Debug.Log("Rule: " + standardizedRuleStr + " already set!");
        }
        else {
            Debug.Log(standardizedRuleStr + " INVALID");
        }

        determineFirewallState(id);

        return validRule;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if(stream.IsWriting) {
            Debug.Log("TERMINAL LOCAL CLIENT STREAM");
        } 
        else {
            Debug.Log("TERMINAL REMOTE CLIENT STREAM");
        }
    }

    [PunRPC]
    public void RPC_UpdateFirewallRules(Dictionary<int,Dictionary<string,bool>> rules) {
        Debug.Log("Firewall rules updated remotely");
        this.firewalls = rules;
    }

    [PunRPC]
    public void RPC_DetermineFirewallState(int id) {
        GameObject terminalGO = terminals[id];
        FirewallRules fwRules = terminalGO.GetComponent<FirewallRules>();
        if(allRulesSet(id)) {
            fwRules.enableFirewall();
        }
        else {
            fwRules.disableFirewall();
        }
    }
}
