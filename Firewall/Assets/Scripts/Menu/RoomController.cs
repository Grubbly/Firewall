using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class RoomController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int multiplayerSceneIndex;

    public override void OnEnable() {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable() {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnJoinedRoom() {
        string playerName = "Grubbling" + PhotonNetwork.PlayerList.Length.ToString();
        PhotonNetwork.NickName = playerName;
        Debug.Log(playerName + " joined room");
        StartGame();
    }

    private void StartGame() {
        if(PhotonNetwork.IsMasterClient) {
            Debug.Log("Starting game!");
            PhotonNetwork.LoadLevel(multiplayerSceneIndex);
        }
    }
}
