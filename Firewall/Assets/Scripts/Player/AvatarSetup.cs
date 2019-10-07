using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using UnityEngine.UI;

public class AvatarSetup : MonoBehaviour
{
    private PhotonView photonView;
    [SerializeField]
    private Text nametag;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        if(photonView.IsMine) {
            photonView.RPC("RPC_AddNametag", RpcTarget.AllBuffered, PlayerInfo.playerInfo.username);
        }
    }

    [PunRPC]
    void RPC_AddNametag(string username)
    {
        nametag.text = username;
    }
}
