using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using System.IO;

public class GameSetup : MonoBehaviour
{
    public static GameSetup gameSetup;
    public Transform[] spawnPoints;

    private void OnEnable() {
        if(GameSetup.gameSetup == null) {
            GameSetup.gameSetup = this;
        }    
    }

    void Start()
    {
        CreatePlayer();
    }

    private void CreatePlayer() {
        Debug.Log("Creating player");
        int spawnPointNum = Random.Range(0, spawnPoints.Length);
        PhotonNetwork.Instantiate(
            Path.Combine("PhotonPrefabs", "PhotonPlayer"), 
            spawnPoints[spawnPointNum].position, 
            spawnPoints[spawnPointNum].rotation
        );
    }
}
