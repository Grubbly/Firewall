using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firewall : MonoBehaviour
{
    public bool toggleWall = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void enable() {
        gameObject.SetActive(true);
    }

    public void disable() {
        gameObject.SetActive(false);
    }

    private void Update() {
        if(toggleWall) {
            enable();
        }
        else if(!toggleWall) {
            disable();
        }
    }
}
