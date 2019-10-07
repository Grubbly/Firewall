using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using System.IO;

public class GameSetup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CreatePlayer();
    }

    private void CreatePlayer() {
        Debug.Log("Creating player");
        PhotonNetwork.Instantiate(
            Path.Combine("PhotonPrefabs", "PhotonPlayer"), 
            Vector3.zero, 
            Quaternion.identity);
    }
}
