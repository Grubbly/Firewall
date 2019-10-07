using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class LobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject startButton;
    [SerializeField]
    private GameObject cancelButton;
    [SerializeField]
    private int roomSize;

    public override void OnConnectedToMaster() {
        PhotonNetwork.AutomaticallySyncScene = true;
        startButton.SetActive(true);
    }

    public void StartButtonClick() {
        startButton.SetActive(false);
        cancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Joining room with other players...");
    }

    public override void OnJoinRandomFailed(short returnCode, string message) {
        Debug.Log("Failed to join random room");
        Debug.Log("Return code: " + returnCode + " with message: " + message);
        Debug.Log("Potentially no rooms available... \nCreating room...");
        CreateRoom();
    }

    void CreateRoom() {
        Debug.Log("Creating room...");
        int roomNumber = Random.Range(0,10000);
        RoomOptions roomOptions = new RoomOptions() {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = (byte)roomSize
        };
        PhotonNetwork.CreateRoom("Creating room named: Room" + roomNumber, roomOptions);
    }

    public override void OnCreateRoomFailed(short returnCode, string message) {
        Debug.Log("Room creation failed! A room with this number probably already exists.");
        Debug.Log("Automatically trying again");
        CreateRoom();
    }

    public void CancelButtonClick() {
        startButton.SetActive(true);
        cancelButton.SetActive(false);
        PhotonNetwork.LeaveRoom();
    }
}
