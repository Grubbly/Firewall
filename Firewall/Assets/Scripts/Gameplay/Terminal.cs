using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Terminal : MonoBehaviourPun, IPunOwnershipCallbacks
{
    [SerializeField]
    GameObject console;
    public bool activeElsewhere = false;

    private void Awake() {
        PhotonNetwork.AddCallbackTarget(this);
        console.SetActive(false);
    }

    private void OnDestroy() {
        PhotonNetwork.RemoveCallbackTarget(this);

        if(console) {
            console.SetActive(false);
        }
    }

    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer) {
        if(targetView != base.photonView) {
            return;
        }
        if(activeElsewhere) {
            Debug.Log("Console controlled by other player -- REJECTING REQUEST");
            return;
        }

        Debug.Log("TargetView: " + targetView + " Player: " + requestingPlayer + " is requesting ownership");

        base.photonView.TransferOwnership(requestingPlayer);
    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner) {
        if(targetView != base.photonView) {
            return;
        }

        if(photonView.IsMine) {
            photonView.RPC("RPC_SetConsoleState", RpcTarget.AllBuffered, true);
            console.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject + " entered terminal");

        PhotonView view = other.GetComponent<PhotonView>();

        if(view) {
            if(view.IsMine) {
                photonView.RequestOwnership();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(photonView.IsMine && photonView.Owner.Equals(other.GetComponent<PhotonView>().Owner)) {
            Debug.Log(other.gameObject.GetComponent<PhotonView>().Owner.NickName + " is shutting down console");
            console.SetActive(false);
            photonView.RPC("RPC_SetConsoleState", RpcTarget.AllBuffered, false);
        }
    }

    [PunRPC]
    public void RPC_SetConsoleState(bool state) {
        activeElsewhere = state;
    }
}
