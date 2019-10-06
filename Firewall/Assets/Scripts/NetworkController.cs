using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class NetworkController : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start() {
        // Open up a connection to Photon server host
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() {
        Debug.Log("Application connected to " + PhotonNetwork.CloudRegion + " Photon server!");
    }

    // Update is called once per frame
    void Update() {
        
    }
}
