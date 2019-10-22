using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class WallManager : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    private Dictionary<int, int> wallHealths = new Dictionary<int, int>();
    
    public int maxWallHealth = 10;

    public int getHealth(int id) {
        return wallHealths[id];
    }
    public void register(int id, int maxHealth) {
        Debug.Log("Registered wall #" + id);
        wallHealths.Add(id,maxHealth);
    } 

    public void takeDamage(int id, int amount) {
        if(amount <= 0) {
            return;
        }

        wallHealths[id] -= amount;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if(stream.IsWriting) {
            Debug.Log("LOCAL CLIENT STREAM");
            stream.SendNext(wallHealths);
        } 
        else {
            Debug.Log("REMOTE CLIENT STREAM");
            this.wallHealths = (Dictionary<int, int>)stream.ReceiveNext();
        }
    }
}
