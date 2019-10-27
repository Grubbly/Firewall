using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TerminalManager : MonoBehaviour
{
    public Dictionary<int, Dictionary<string, bool>> firewalls = new Dictionary<int, Dictionary<string, bool>>();
    public void register(int id, List<string> rules) {
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

        return validRule;
    }


}
