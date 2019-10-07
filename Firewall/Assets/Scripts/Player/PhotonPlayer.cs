using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using System.IO;
using Photon.Pun.Demo.PunBasics;

public class PhotonPlayer : MonoBehaviour
{
    private PhotonView photonView;
    public GameObject avatar;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        InstantiateAvatar();
    }

    void InstantiateAvatar() {
        int spawnPointNum = Random.Range(0, GameSetup.gameSetup.spawnPoints.Length);

        // Instantiate Avatar
        if(photonView.IsMine){
            avatar = PhotonNetwork.Instantiate(
            Path.Combine("PhotonPrefabs", "PhotonAvatar"),
            GameSetup.gameSetup.spawnPoints[spawnPointNum].position,
            GameSetup.gameSetup.spawnPoints[spawnPointNum].rotation,
            0);
            InstantiateCamera();
        }
    }

    void InstantiateCamera() {
        CameraWork _cameraWork = avatar.GetComponent<CameraWork>();

        if (_cameraWork != null)
        {
            if (photonView.IsMine)
            {
                _cameraWork.OnStartFollowing();
            }
        }
        else
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab.", this);
        }
    }
}
