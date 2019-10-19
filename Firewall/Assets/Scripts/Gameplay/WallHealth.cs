using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class WallHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 10;
    [SerializeField]
    private GameObject hitEffect;
    [SerializeField]
    private GameObject destroyEffect;
    private PhotonView photonView;
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        photonView = GetComponent<PhotonView>();
    }

    private void OnCollisionEnter(Collision other) {
        Debug.Log(gameObject + " hit by " + other.gameObject);
        if(other.gameObject.tag == "malware") {
            GameObject effect = Instantiate(
                hitEffect, 
                other.gameObject.GetComponent<Rigidbody>().position,
                other.gameObject.GetComponent<Rigidbody>().rotation
            );

            if(photonView.IsMine) {
                photonView.RPC("RPC_HandleWallCollision", RpcTarget.AllBuffered);
            }
            
            Destroy(other.gameObject);
            Destroy(effect, 1f);
        }
    }

    private void Update() {
        if(health <= 0) {
            GameObject effect = Instantiate(
                destroyEffect, 
                gameObject.transform.position,
                gameObject.transform.rotation
            );
            Destroy(effect, 1f);
            Destroy(gameObject, 1.5f);
        }
    }

    [PunRPC]
    public void RPC_HandleWallCollision() {
        health--;
    }

    [PunRPC]
    public void RPC_DestroyOnContact(GameObject target) {
        PhotonNetwork.Destroy(target);
    }
}
