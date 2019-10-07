using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using System.IO;

public class PhotonPlayer : MonoBehaviour
{
    private PhotonView photonView;
    public GameObject player;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        player = gameObject;
    }
}
