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
        setPlayerDetails();
        CreatePlayer();
    }

    private void setPlayerDetails() {
        string playerName = PlayerInfo.playerInfo.username + "_Grubbling_" + PhotonNetwork.PlayerList.Length.ToString();
        PhotonNetwork.NickName = playerName;
        Debug.Log(playerName + " joined room");
    }

    // Instantiate player controller
    private void CreatePlayer() {
        Debug.Log("Creating player");
        int spawnPointNum = Random.Range(0, spawnPoints.Length);
        PhotonNetwork.Instantiate(
            Path.Combine("PhotonPrefabs", "PhotonNetworkPlayer"), 
            Vector3.zero, 
            Quaternion.identity
        );
    }
}
