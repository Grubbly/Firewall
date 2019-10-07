using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using System.IO;

public class PhotonPlayer : MonoBehaviour
{
    private PhotonView photonView;
    public GameObject avatar;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        int spawnPointNum = Random.Range(0, GameSetup.gameSetup.spawnPoints.Length);

        // Instantiate Avatar
        if(photonView.IsMine){
            avatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonAvatar"),
            GameSetup.gameSetup.spawnPoints[spawnPointNum].position,
            GameSetup.gameSetup.spawnPoints[spawnPointNum].rotation,
            0);
        }
    }
}
